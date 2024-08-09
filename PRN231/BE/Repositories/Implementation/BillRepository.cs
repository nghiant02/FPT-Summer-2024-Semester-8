using BusinessObjects.Dto.BillReqRes;
using BusinessObjects.Models;
using DAO;
using Microsoft.EntityFrameworkCore;
using DAO.Dao;
using Repositories.Interface;
using Tools;
using BusinessObjects.Dto.BillReqRes;
using BusinessObjects.Dto.Dashboard;
using MongoDB.Driver;

namespace Repositories.Implementation
{
    public class BillRepository(BillDao billDao, BillJewelryDao billJewelryDao, BillPromotionDao billPromotionDao) : IBillRepository
    {
        public BillDao BillDao { get; } = billDao;
        public BillJewelryDao BillJewelryDao { get; } = billJewelryDao;
        public BillPromotionDao BillPromotionDao { get; } = billPromotionDao;
        private readonly IMongoCollection<BillDetailDto> _collection;


        public async Task<IEnumerable<Bill?>?> Gets()
        {
            return await BillDao.GetBills();
        }

        public async Task<Bill?> GetById(string id)
        {
            return await BillDao.GetBillById(id);
        }
        
        public async Task<BillResponseDto> CreateBill(BillRequestDto billRequestDto)
        {
            double totalAmount = 999;
            double totalDiscount = 0;
            double finalAmount = 0;
            
            // Create bill
            var bill = new Bill
            {
                BillId = Generator.GenerateId(),
                CounterId = billRequestDto.CounterId,
                CustomerId = billRequestDto.CustomerId,
                UserId = billRequestDto.UserId,
                SaleDate = DateTime.Now.ToUniversalTime(),
                TotalAmount = totalAmount,
            };
            var billId = await BillDao.CreateBill(bill);
            // Check if bill is created
            if (billId == null)
            {
                throw new InvalidOperationException("Failed to create the bill.");
            }
            // Add bill items
            foreach (var item in billRequestDto.Jewelries)
            {
                var billJewelry = new BillJewelry
                {
                    BillJewelryId = Generator.GenerateId(),
                    BillId = billId,
                    JewelryId = item.JewelryId,
                };
                await BillJewelryDao.CreateBillJewelry(billJewelry);
            }
            // Add bill promotions
            foreach (var promotion in billRequestDto.Promotions)
            {
                var billPromotion = new BillPromotion
                {
                    BillPromotionId = Generator.GenerateId(),
                    BillId = billId,
                    PromotionId = promotion.PromotionId,
                };
                await BillPromotionDao.CreateBillPromotion(billPromotion);
            }
            // Generate response
            var billResponseDto = new BillResponseDto
            {
                BillId = billId,
                TotalAmount = totalAmount,
                SaleDate = bill.SaleDate,
                Items = billRequestDto.Jewelries.Select(i => new BillItemResponse
                {
                    JewelryId = i.JewelryId,
                    TotalPrice = 0 // Calculate price
                }).ToList(),
                Promotions = billRequestDto.Promotions.Select(p => new BillPromotionResponse
                {
                    PromotionId = p.PromotionId,
                    Discount = 0 // Calculate discount
                }).ToList(),
                AdditionalDiscount = billRequestDto.AdditionalDiscount,
                PointsUsed = 0, // Calculate points used
                FinalAmount = 0 // Calculate final amount
            };
            return billResponseDto;
        }
        public Task<int> Create(Bill entity)
        {
            throw new NotImplementedException();
        }
        public async Task<string> CreateBill(Bill entity)
        {
            return await BillDao.CreateBill(entity);
        }

        public async Task<int> UpdateBill(Bill entity)
        {
            return await BillDao.UpdateBill(entity);
        }

        public async Task<decimal> GetTotalRevenueAllTime()
        {
            var bills = await BillDao.GetBills();
            return (decimal)bills.Sum(b => (decimal?)b.TotalAmount ?? 0);
        }

        public async Task<decimal> GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            var bills = await BillDao.GetBills();
            return (decimal)bills.Where(b => b.SaleDate >= startDate && b.SaleDate <= endDate)
                                 .Sum(b => (decimal?)b.TotalAmount ?? 0);
        }

        public async Task<RevenueByCounterDto> GetRevenueByCounter(string counterId)
        {
            var bills = await BillDao.GetBills();
            var revenue = bills.Where(b => b.CounterId == counterId)
                               .Sum(b => (decimal?)b.TotalAmount ?? 0);
            return new RevenueByCounterDto { CounterId = counterId, TotalRevenue = revenue };
        }

        public async Task<RevenueByEmployeeDto> GetRevenueByEmployee(string userId)
        {
            var bills = await BillDao.GetBills();
            var revenue = bills.Where(b => b.UserId == userId)
                               .Sum(b => (decimal?)b.TotalAmount ?? 0);
            return new RevenueByEmployeeDto { EmployeeId = userId, TotalRevenue = revenue };
        }

        public async Task<RevenueByProductTypeDto> GetRevenueByProductType(string typeId)
        {
            var bills = await BillDao.GetAllBills()
                                     .Include(b => b.BillJewelries)
                                     .ThenInclude(bj => bj.Jewelry)
                                     .ThenInclude(j => j.JewelryMaterials)
                                     .ThenInclude(jm => jm.GoldPrice)
                                     .Include(b => b.BillJewelries)
                                     .ThenInclude(bj => bj.Jewelry)
                                     .ThenInclude(j => j.JewelryMaterials)
                                     .ThenInclude(jm => jm.StonePrice)
                                     .ToListAsync();

            var billJewelries = bills.SelectMany(b => b.BillJewelries)
                                     .Where(j => j.Jewelry != null && j.Jewelry.JewelryTypeId == typeId)
                                     .ToList();

            if (!billJewelries.Any())
            {
                Console.WriteLine("No billJewelries found for the given typeId.");
            }

            decimal totalRevenue = 0;

            foreach (var billJewelry in billJewelries)
            {
                var jewelry = billJewelry.Jewelry;
                var jewelryPrice = CalculateJewelryPrice(jewelry);

                // Log details for debugging
                Console.WriteLine($"Jewelry ID: {jewelry.JewelryId}, Type ID: {jewelry.JewelryTypeId}, Calculated Price: {jewelryPrice}");

                totalRevenue += jewelryPrice;
            }

            return new RevenueByProductTypeDto { JewelryTypeId = typeId, TotalRevenue = totalRevenue };
        }

        private static decimal CalculateJewelryPrice(Jewelry jewelry)
        {
            if (jewelry == null || jewelry.JewelryMaterials == null)
                return 0;

            decimal totalPrice = 0;

            foreach (var material in jewelry.JewelryMaterials)
            {
                if (material.GoldPrice != null)
                {
                    totalPrice += (decimal)material.GoldPrice.SellPrice * (decimal)material.GoldQuantity;
                }
                if (material.StonePrice != null)
                {
                    totalPrice += (decimal)material.StonePrice.SellPrice * (decimal)material.StoneQuantity;
                }
            }

            if (jewelry.LaborCost.HasValue)
            {
                totalPrice += (decimal)jewelry.LaborCost.Value;
            }

            return totalPrice;
        }
    }
}
