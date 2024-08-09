using BusinessObjects.Dto.Jewelry;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;
using DAO.Dao;
using Repositories.Interface;

namespace Repositories.Implementation
{
    public class JewelryRepository(
        JewelryDao jewelryDao,
        JewelryTypeDao jewelryTypeDao,
        GoldPriceDao goldPriceDao,
        GemPriceDao gemPriceDao,
        JewelryMaterialDao jewelryMaterialDao) : IJewelryRepository
    {
        private JewelryDao JewelryDao { get; } = jewelryDao;
        private JewelryTypeDao JewelryTypeDao { get; } = jewelryTypeDao;
        private GoldPriceDao GoldPriceDao { get; } = goldPriceDao;
        private GemPriceDao GemPriceDao { get; } = gemPriceDao;
        private JewelryMaterialDao JewelryMaterialDao { get; } = jewelryMaterialDao;

        public async Task<int> Create(Jewelry entity)
        {
            entity.IsSold = false;
            entity.CreatedAt = DateTimeOffset.UtcNow.ToUniversalTime();
            return await JewelryDao.CreateJewelry(entity);
        }

        public async Task<int> Delete(string id)
        {
            return await JewelryDao.DeleteJewelry(id);
        }

        public async Task<(int, int, IEnumerable<JewelryResponseDto>)> GetsJewelryPaging(int pageNumber, int pageSize,
            string? name, string? typeId)
        {
            // Retrieve paginated jewelries from DAO
            var jewelries = await JewelryDao.GetJewelries(pageNumber, pageSize);
            if (jewelries.Item3 == null || !jewelries.Item3.Any())
            {
                return default;
            }

            // Apply filters on the retrieved paginated jewelries
            var filteredJewelries = jewelries.Item3.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                filteredJewelries =
                    filteredJewelries.Where(j => j.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                filteredJewelries = filteredJewelries.Where(j => j.JewelryTypeId == typeId);
            }

            var jewelryResponseDtos = new List<JewelryResponseDto>();

            foreach (var jewelry in filteredJewelries)
            {
                var jewelryType = await JewelryTypeDao.GetJewelryTypeById(jewelry.JewelryTypeId);
                var jewelryMaterials = await JewelryMaterialDao.GetJewelryMaterialByJewelry(jewelry.JewelryId);
                var jewelryMaterialList = new List<JewelryMaterial> { jewelryMaterials };

                foreach (var jewelryMaterial in jewelryMaterialList)
                {
                    var goldType = await GoldPriceDao.GetGoldPriceById(jewelryMaterial.GoldPriceId);
                    var stoneType = await GemPriceDao.GetStonePriceById(jewelryMaterial.StonePriceId);

                    jewelryMaterial.GoldPrice = goldType;
                    jewelryMaterial.StonePrice = stoneType;
                }

                jewelry.JewelryType = jewelryType;
                jewelry.JewelryMaterials = jewelryMaterialList;

                var totalPrice = jewelry.JewelryMaterials.Sum(jm => CalculateTotalPrice(jm, jewelry.LaborCost));

                var jewelryResponseDto = new JewelryResponseDto
                {
                    JewelryId = jewelry.JewelryId,
                    Name = jewelry.Name,
                    JewelryTypeId = jewelryType?.JewelryTypeId,
                    Type = jewelryType?.Name,
                    ImageUrl = jewelry.ImageUrl,
                    Barcode = jewelry.Barcode,
                    LaborCost = jewelry.LaborCost,
                    Materials = jewelry.JewelryMaterials.Select(jm => new Materials
                    {
                        Gold = new GoldResponseDto
                        {
                            GoldId = jm.GoldPriceId,
                            GoldType = jm.GoldPrice?.Type,
                            GoldQuantity = jm.GoldQuantity,
                            GoldPrice = jm.GoldPrice?.SellPrice ?? 0
                        },
                        Gem = new GemResponseDto
                        {
                            GemId = jm.StonePriceId,
                            Gem = jm.StonePrice?.Type,
                            GemQuantity = jm.StoneQuantity,
                            GemPrice = jm.StonePrice?.SellPrice ?? 0
                        }
                    }).ToList(),
                    TotalPrice = totalPrice
                };

                jewelryResponseDtos.Add(jewelryResponseDto);
            }

            // Return the total number of items, the page size, and the filtered jewelry list
            return (jewelries.Item1, jewelries.Item2, jewelryResponseDtos);
        }


        public async Task<(int, int, IEnumerable<JewelryResponseDto>)> GetsJewelryPagingByType(string jewelryTypeId,
            int pageNumber, int pageSize)
        {
            var jewelries = await JewelryDao.GetJewelriesByType(jewelryTypeId, pageNumber, pageSize);
            if (jewelries.Item3 == null || !jewelries.Item3.Any())
            {
                return default;
            }

            var jewelryResponseDtos = new List<JewelryResponseDto>();

            foreach (var jewelry in jewelries.Item3)
            {
                var jewelryType = await JewelryTypeDao.GetJewelryTypeById(jewelry.JewelryTypeId);
                var jewelryMaterials = await JewelryMaterialDao.GetJewelryMaterialByJewelry(jewelry.JewelryId);
                var jewelryMaterialList = new List<JewelryMaterial> { jewelryMaterials };
                foreach (var jewelryMaterial in jewelryMaterialList)
                {
                    var goldType = await GoldPriceDao.GetGoldPriceById(jewelryMaterial.GoldPriceId);
                    var stoneType = await GemPriceDao.GetStonePriceById(jewelryMaterial.StonePriceId);

                    jewelryMaterial.GoldPrice = goldType;
                    jewelryMaterial.StonePrice = stoneType;
                }

                jewelry.JewelryType = jewelryType;
                jewelry.JewelryMaterials = jewelryMaterialList;

                var totalPrice = jewelry.JewelryMaterials.Sum(jm => CalculateTotalPrice(jm, jewelry.LaborCost));

                var jewelryResponseDto = new JewelryResponseDto
                {
                    JewelryId = jewelry.JewelryId,
                    JewelryTypeId = jewelryType?.JewelryTypeId,
                    Name = jewelry.Name,
                    Type = jewelryType?.Name,
                    ImageUrl = jewelry.ImageUrl,
                    Barcode = jewelry.Barcode,
                    LaborCost = jewelry.LaborCost,
                    Materials = jewelry.JewelryMaterials.Select(jm => new Materials
                    {
                        Gold = new GoldResponseDto
                        {
                            GoldId = jm.GoldPriceId,
                            GoldType = jm.GoldPrice?.Type,
                            GoldQuantity = jm.GoldQuantity,
                            GoldPrice = jm.GoldPrice?.SellPrice ?? 0
                        },
                        Gem = new GemResponseDto
                        {
                            GemId = jm.StonePriceId,
                            Gem = jm.StonePrice?.Type,
                            GemQuantity = jm.StoneQuantity,
                            GemPrice = jm.StonePrice?.SellPrice ?? 0
                        }
                    }).ToList(),
                    TotalPrice = totalPrice
                };

                jewelryResponseDtos.Add(jewelryResponseDto);
            }

            return (jewelries.Item1, jewelries.Item2, jewelryResponseDtos);
        }

        public async Task<Jewelry?> GetJewelryById(string id)
        {
            return await JewelryDao.GetJewelryById(id);
        }

        public Task<IEnumerable<JewelryResponseDto>?> Gets()
        {
            throw new NotImplementedException();
        }

        public async Task<JewelryResponseDto?> GetById(string id)
        {
            var jewelry = await JewelryDao.GetJewelryById(id);
            var jewelryType = await JewelryTypeDao.GetJewelryTypeById(jewelry.JewelryTypeId);
            var jewelryMaterial = await JewelryMaterialDao.GetJewelryMaterialByJewelry(id);
            var goldType = await GoldPriceDao.GetGoldPriceById(jewelryMaterial.GoldPriceId);
            var stoneType = await GemPriceDao.GetStonePriceById(jewelryMaterial.StonePriceId);

            jewelryMaterial.GoldPrice = goldType;
            jewelryMaterial.StonePrice = stoneType;
            jewelry.JewelryType = jewelryType;
            jewelry.JewelryMaterials = new List<JewelryMaterial> { jewelryMaterial };

            var totalPrice = CalculateTotalPrice(jewelryMaterial, jewelry.LaborCost);

            var jewelryResponseDto = new JewelryResponseDto
            {
                JewelryId = jewelry.JewelryId,
                Name = jewelry.Name,
                ImageUrl = jewelry.ImageUrl,
                JewelryTypeId = jewelryType?.JewelryTypeId,
                Type = jewelryType?.Name,
                Barcode = jewelry.Barcode,
                JewelryPrice = CalculateJewelryPrice(jewelryMaterial),
                LaborCost = jewelry.LaborCost,
                IsSold = (bool)jewelry.IsSold!,
                Materials = jewelry.JewelryMaterials.Select(jm => new Materials
                {
                    Gold = new GoldResponseDto
                    {
                        GoldId = jm.GoldPriceId,
                        GoldType = jm.GoldPrice?.Type,
                        GoldQuantity = jm.GoldQuantity,
                        GoldPrice = jm.GoldPrice?.SellPrice ?? 0
                    },
                    Gem = new GemResponseDto
                    {
                        GemId = jm.StonePriceId,
                        Gem = jm.StonePrice?.Type,
                        GemQuantity = jm.StoneQuantity,
                        GemPrice = jm.StonePrice?.SellPrice ?? 0
                    }
                }).ToList(),
                TotalPrice = totalPrice
            };

            return jewelryResponseDto;
        }

        public async Task<int> Update(string id, Jewelry entity)
        {
            return await JewelryDao.UpdateJewelry(id, entity);
        }

        private static float CalculateTotalPrice(JewelryMaterial jewelryMaterial, double? laborCost)
        {
            float totalPrice = 0;
            if (jewelryMaterial.GoldPrice != null)
            {
                totalPrice += jewelryMaterial.GoldPrice.BuyPrice * jewelryMaterial.GoldQuantity;
            }

            if (jewelryMaterial.StonePrice != null)
            {
                totalPrice += jewelryMaterial.StonePrice.BuyPrice * jewelryMaterial.StoneQuantity;
            }

            totalPrice += (float)laborCost;
            return totalPrice;
        }

        private static float CalculateJewelryPrice(JewelryMaterial jewelryMaterial)
        {
            float totalPrice = 0;
            if (jewelryMaterial.GoldPrice != null)
            {
                totalPrice += jewelryMaterial.GoldPrice.BuyPrice * jewelryMaterial.GoldQuantity;
            }

            if (jewelryMaterial.StonePrice != null)
            {
                totalPrice += jewelryMaterial.StonePrice.BuyPrice * jewelryMaterial.StoneQuantity;
            }

            return totalPrice;
        }

        public async Task<int> GetSoldJewelryCount()
        {
            return await JewelryDao.GetTotalSellJewelry();
        }

        public async Task<int> UpdateJewelryWithMaterial(string id, JewelryRequestDto jewelryRequestDto)
        {
            var jewelry = await JewelryDao.GetJewelryById(id);
            if (jewelry == null)
            {
                return 0;
            }

            var jewelryType = await JewelryTypeDao.GetJewelryTypeById(jewelryRequestDto.JewelryTypeId);
            if (jewelryType == null)
            {
                return 0;
            }

            var jewelryMaterial = await JewelryMaterialDao.GetJewelryMaterialByJewelry(id);
            if (jewelryMaterial == null)
            {
                return 0;
            }

            var goldPrice = await GoldPriceDao.GetGoldPriceById(jewelryRequestDto.JewelryMaterial.GoldId);
            if (goldPrice == null)
            {
                return 0;
            }

            var gemPrice = await GemPriceDao.GetStonePriceById(jewelryRequestDto.JewelryMaterial.GemId);
            if (gemPrice == null)
            {
                return 0;
            }

            jewelry.Name = jewelryRequestDto.Name;
            jewelry.ImageUrl = jewelryRequestDto.ImageUrl;
            jewelry.JewelryTypeId = jewelryRequestDto.JewelryTypeId;
            jewelry.Barcode = jewelryRequestDto.Barcode;
            jewelry.LaborCost = jewelryRequestDto.LaborCost;

            jewelryMaterial.GoldPriceId = jewelryRequestDto.JewelryMaterial.GoldId;
            jewelryMaterial.GoldQuantity = jewelryRequestDto.JewelryMaterial.GoldQuantity;
            jewelryMaterial.StonePriceId = jewelryRequestDto.JewelryMaterial.GemId;
            jewelryMaterial.StoneQuantity = jewelryRequestDto.JewelryMaterial.GemQuantity;

            await JewelryDao.UpdateJewelry(id, jewelry);
            await JewelryMaterialDao.UpdateJewelryMaterial(jewelryMaterial);

            return 1;
        }

        public async Task<bool> DisableJewelry(string id)
        {
            return await jewelryDao.DisableJewelry(id);
        }
    }
}