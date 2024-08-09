using BusinessObjects.Dto.Jewelry;
using BusinessObjects.Dto.Other;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;
using Repositories.Interface;
using Services.Interface;
using Tools;

namespace Services.Implementation
{
    public class JewelryService(
        IJewelryRepository jewelryRepository,
        IJewelryMaterialRepository jewelryMaterialRepository) : IJewelryService
    {
        private IJewelryRepository JewelryRepository { get; } = jewelryRepository;
        private IJewelryMaterialRepository JewelryMaterialRepository { get; } = jewelryMaterialRepository;

        public async Task<PagingResponse> GetJewelries(int pageNumber, int pageSize, string? name, string? typeId)
        {
            var jewelries = await JewelryRepository.GetsJewelryPaging(pageNumber, pageSize, name, typeId);
            var jewelryPaging = new PagingResponse
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecord = jewelries.Item1,
                TotalPage = jewelries.Item2,
                Data = jewelries.Item3
            };
            return jewelryPaging;
        }

        public async Task<JewelryResponseDto?> GetJewelryById(string id)
        {
            var jewelryResponseDto = await JewelryRepository.GetById(id);
            return jewelryResponseDto;
        }

        public async Task<PagingResponse?> GetJewelryByType(string jewelryTypeId, int pageNumBer, int pageSize)
        {
            var jewelries = await JewelryRepository.GetsJewelryPagingByType(jewelryTypeId, pageNumBer, pageSize);
            var jewelryPaging = new PagingResponse
            {
                PageNumber = pageNumBer,
                PageSize = pageSize,
                TotalRecord = jewelries.Item1,
                TotalPage = jewelries.Item2,
                Data = jewelries.Item3
            };
            return jewelryPaging;
        }

        public async Task<int> CreateJewelry(JewelryRequestDto jewelryRequestDto)
        {
            // Create Jewelry first before creating JewelryMaterial
            var jewelry = new Jewelry
            {
                JewelryId = Generator.GenerateId(),
                JewelryTypeId = jewelryRequestDto.JewelryTypeId,
                ImageUrl = jewelryRequestDto.ImageUrl,
                Name = jewelryRequestDto.Name,
                Barcode = jewelryRequestDto.Barcode,
                LaborCost = jewelryRequestDto.LaborCost,
                IsSold = false
            };
            try
            {
                await JewelryRepository.Create(jewelry);
            }
            catch (Exception e)
            {
                throw new CustomException.InvalidDataException("Failed to create Jewelry.");
            }

            // Create JewelryMaterial
            var jewelryMaterial = new JewelryMaterial
            {
                JewelryMaterialId = Generator.GenerateId(),
                JewelryId = jewelry.JewelryId,
                GoldPriceId = jewelryRequestDto.JewelryMaterial.GoldId,
                StonePriceId = jewelryRequestDto.JewelryMaterial.GemId,
                GoldQuantity = jewelryRequestDto.JewelryMaterial.GoldQuantity,
                StoneQuantity = jewelryRequestDto.JewelryMaterial.GemQuantity
            };
            try
            {
                await JewelryMaterialRepository.Create(jewelryMaterial);
                return 1;
            }
            catch (Exception e)
            {
                await JewelryRepository.Delete(jewelry.JewelryId);
                throw new CustomException.InvalidDataException("Failed to create JewelryMaterial.");
            }
        }

        public Task<int> DeleteJewelry(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateJewelry(string id, JewelryRequestDto jewelry)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetSoldJewelryCount()
        {
            return await JewelryRepository.GetSoldJewelryCount();
        }

        public async Task<int> UpdateJewelryWithMaterial(string id, JewelryRequestDto jewelryRequestDto)
        {
            return await JewelryRepository.UpdateJewelryWithMaterial(id, jewelryRequestDto);
        }
        public async Task<bool> DisableJewelry(string id)
        {
            return await JewelryRepository.DisableJewelry(id);
        }
    }
}