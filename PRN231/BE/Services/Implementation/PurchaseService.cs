using BusinessObjects.Dto.BillReqRes;
using BusinessObjects.Dto.BuyBack;
using BusinessObjects.Models;
using Repositories.Interface;
using Services.Interface;
using Tools;

namespace Services.Implementation
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IBillDetailRepository _billDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository, IBillDetailRepository billDetailRepository, IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _purchaseRepository = purchaseRepository;
            _billDetailRepository = billDetailRepository;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }

        public async Task<ProcessBuybackByIdResponse> ProcessBuybackById(string jewelryId)
        {
            var purchase = await _purchaseRepository.GetPurchaseByJewelryIdWithBuyBack0(jewelryId);

            if (purchase == null)
            {
                throw new Exception("Jewelry not found or not eligible for buyback.");
            }

            var jewelry = await _purchaseRepository.GetJewelryById(jewelryId);
            if (jewelry == null)
            {
                throw new Exception("Jewelry not found.");
            }

            var materials = await _purchaseRepository.GetJewelryMaterialByJewelryId(jewelryId);
            jewelry.JewelryMaterials = new List<JewelryMaterial> { materials };

            var totalPrice = CalculateTotalPrice(jewelry.JewelryMaterials, jewelry.LaborCost);

            purchase.IsBuyBack = 1;
            purchase.PurchasePrice = totalPrice;

            var customer = await _customerRepository.GetById(purchase.CustomerId);
            var user = await _userRepository.GetUserById(purchase.UserId);

            var billDetailDto = new BillDetailDto
            {
                Id = Generator.GenerateId(),
                BillId = Generator.GenerateId(),
                CustomerName = customer?.FullName,
                StaffName = user?.FullName,
                TotalAmount = (double)purchase.PurchasePrice,
                TotalDiscount = 0,
                SaleDate = purchase.PurchaseDate,
                Items = new List<BillItemResponse?>
                {
                    new BillItemResponse
                    {
                        JewelryId = purchase.JewelryId,
                        Name = jewelry.Name,
                        JewelryPrice = totalPrice,
                        TotalPrice = totalPrice
                    }
                },
                Promotions = new List<BillPromotionResponse?>(),
                AdditionalDiscount = 0,
                PointsUsed = 0,
                FinalAmount = (double)purchase.PurchasePrice
            };

            await _billDetailRepository.AddBillDetail(billDetailDto);

            purchase.BillId = billDetailDto.BillId;

            await _purchaseRepository.UpdatePurchase(purchase);

            return new ProcessBuybackByIdResponse
            {
                TotalPrice = totalPrice,
                BillId = purchase.BillId
            };
        }

        public async Task<CountProcessBuybackByIdResponse> CountProcessBuybackById(string jewelryId)
        {
            var purchase = await _purchaseRepository.GetPurchaseByJewelryIdWithBuyBack0(jewelryId);

            if (purchase == null)
            {
                throw new Exception("Jewelry not found or not eligible for buyback.");
            }

            var jewelry = await _purchaseRepository.GetJewelryById(jewelryId);
            if (jewelry == null)
            {
                throw new Exception("Jewelry not found.");
            }

            var materials = await _purchaseRepository.GetJewelryMaterialByJewelryId(jewelryId);
            jewelry.JewelryMaterials = new List<JewelryMaterial> { materials };

            var totalPrice = CalculateTotalPrice(jewelry.JewelryMaterials, jewelry.LaborCost);

            return new CountProcessBuybackByIdResponse
            {
                TotalPrice = totalPrice
            };
        }

        public async Task<ProcessBuybackByNameResponse> ProcessBuybackByName(BuybackByNameRequest request)
        {

            var jewelryId = Generator.GenerateId();
            var jewelry = new Jewelry
            {
                JewelryId = jewelryId,
                Name = request.JewelryName,
                JewelryTypeId = request.JewelryTypeId,
                ImageUrl = request.ImageUrl,
                LaborCost = request.LaborCost,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var jewelryMaterialId = Generator.GenerateId();
            var jewelryMaterial = new JewelryMaterial
            {
                JewelryMaterialId = jewelryMaterialId,
                JewelryId = jewelryId,
                GoldPriceId = request.JewelryMaterial.GoldId,
                StonePriceId = request.JewelryMaterial.StoneId,
                GoldQuantity = request.JewelryMaterial.GoldQuantity,
                StoneQuantity = request.JewelryMaterial.StoneQuantity,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var customerId = Generator.GenerateId();
            var addCustomer = new Customer
            {
                CustomerId = customerId,
                FullName = request.Customer.FullName,
                Phone = request.Customer.Phone,
                Address = request.Customer.Address,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            await _purchaseRepository.CreateJewelry(jewelry);
            await _purchaseRepository.CreateJewelryMaterial(jewelryMaterial);
            await _customerRepository.CreateCustomer(addCustomer);

            var materials = await _purchaseRepository.GetJewelryMaterialByJewelryId(jewelryId);
            jewelry.JewelryMaterials = new List<JewelryMaterial> { materials };

            var totalPrice = CalculateTotalPrice(new List<JewelryMaterial> { materials }, request.LaborCost);

            var purchase = new Purchase
            {
                PurchaseId = Generator.GenerateId(),
                CustomerId = addCustomer.CustomerId,
                JewelryId = jewelryId,
                UserId = request.UserId,
                PurchaseDate = DateTimeOffset.UtcNow,
                PurchasePrice = request.HasGuarantee ? totalPrice : totalPrice * 0.3,
                IsBuyBack = request.HasGuarantee ? 2 : 3
            };

            var purchasePrice = purchase.PurchasePrice;

            var customer = await _customerRepository.GetById(purchase.CustomerId);
            var user = await _userRepository.GetUserById(purchase.UserId);

            var billDetailDto = new BillDetailDto
            {
                Id = Generator.GenerateId(),
                BillId = Generator.GenerateId(),
                CustomerName = customer?.FullName,
                StaffName = user?.FullName,
                TotalAmount = (double)purchase.PurchasePrice,
                TotalDiscount = 0,
                SaleDate = purchase.PurchaseDate,
                Items = new List<BillItemResponse?>
        {
            new BillItemResponse
            {
                JewelryId = purchase.JewelryId,
                Name = jewelry.Name,
                JewelryPrice = totalPrice,
                TotalPrice = totalPrice
            }
        },
                Promotions = new List<BillPromotionResponse?>(),
                AdditionalDiscount = 0,
                PointsUsed = 0,
                FinalAmount = (double)purchase.PurchasePrice
            };

            await _billDetailRepository.AddBillDetail(billDetailDto);

            purchase.BillId = billDetailDto.BillId;

            await _purchaseRepository.CreatePurchase(purchase);

            return new ProcessBuybackByNameResponse
            {
                TotalPrice = (float)purchasePrice,
                BillId = purchase.BillId
            };
        }


        public async Task<CountProcessBuybackByNameResponse> CountProcessBuybackByName(CountBuybackByNameRequest request)
        {
            if (request == null)
            {
                throw new Exception("Request is null");
            }

            if (request.JewelryMaterial == null)
            {
                throw new Exception("JewelryMaterial in request is null");
            }

            var goldPriceId = request.JewelryMaterial.GoldId;
            var stonePriceId = request.JewelryMaterial.StoneId;
            var goldQuantity = request.JewelryMaterial.GoldQuantity;
            var stoneQuantity = request.JewelryMaterial.StoneQuantity;

            var goldPrice = await _purchaseRepository.GetGoldPriceById(goldPriceId);
            var stonePrice = await _purchaseRepository.GetStonePriceById(stonePriceId);

            if (goldPrice == null || stonePrice == null)
            {
                throw new Exception("Gold price or stone price not found.");
            }

            var totalPrice = CalculateTotalPriceForName(goldPrice.BuyPrice, goldQuantity, stonePrice.BuyPrice, stoneQuantity, request.LaborCost);

            if (!request.HasGuarantee)
            {
                totalPrice *= 0.3f;
            }

            return new CountProcessBuybackByNameResponse
            {
                TotalPrice = totalPrice
            };
        }

        private static float CalculateTotalPrice(IEnumerable<JewelryMaterial> jewelryMaterials, double? laborCost)
        {
            float totalPrice = 0;
            foreach (var material in jewelryMaterials)
            {
                if (material.GoldPrice != null)
                {
                    totalPrice += material.GoldPrice.BuyPrice * material.GoldQuantity;
                }

                if (material.StonePrice != null)
                {
                    totalPrice += material.StonePrice.BuyPrice * material.StoneQuantity;
                }
            }

            if (laborCost.HasValue)
            {
                totalPrice += (float)laborCost.Value;
            }

            return totalPrice;
        }

        private static float CalculateTotalPriceForName(float goldPrice, float goldQuantity, float stonePrice, float stoneQuantity, double? laborCost)
        {
            float totalPrice = 0;

            totalPrice += goldPrice * goldQuantity;
            totalPrice += stonePrice * stoneQuantity;

            if (laborCost.HasValue)
            {
                totalPrice += (float)laborCost.Value;
            }

            return totalPrice;
        }
    }
}
