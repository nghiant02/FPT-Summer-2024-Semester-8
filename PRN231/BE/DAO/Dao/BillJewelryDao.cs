using BusinessObjects.Context;
using BusinessObjects.Dto.Dashboard;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace DAO.Dao
{
    public class BillJewelryDao
    {
        private readonly JssatsContext _context;
        public BillJewelryDao()
        {
            _context = new JssatsContext();
        }
        public async Task<IEnumerable<BillJewelry?>?> GetBillJewelries()
        {
            return await _context.BillJewelries.ToListAsync();
        }
        public async Task<BillJewelry?> GetBillJewelryById(string id)
        {
            return await _context.BillJewelries.FindAsync(id);
        }
        public async Task<int> CreateBillJewelry(BillJewelry billJewelry)
        {
            billJewelry.BillJewelryId = Generator.GenerateId();
            _context.BillJewelries.Add(billJewelry);
            return await _context.SaveChangesAsync();
        }
        public async Task<BillJewelry?> GetBillJewelryByBillId(string billId)
        {
            return await _context.BillJewelries.FirstOrDefaultAsync(b => b.BillId == billId);
        }
        public async Task<IEnumerable<BestSellingProductDto>> GetBestSellingProducts()
        {
            return await _context.BillJewelries
                .GroupBy(bj => new { bj.JewelryId, bj.Jewelry.Name })
                .Select(g => new BestSellingProductDto
                {
                    JewelryId = g.Key.JewelryId,
                    JewelryName = g.Key.Name,
                    PurchaseTime = g.Count()
                })
                .OrderByDescending(x => x.PurchaseTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<BestSellingProductTypeDto>> GetBestSellingProductTypes()
        {
            return await _context.BillJewelries
                .GroupBy(bj => new { bj.Jewelry.JewelryTypeId, bj.Jewelry.JewelryType.Name })
                .Select(g => new BestSellingProductTypeDto
                {
                    JewelryTypeId = g.Key.JewelryTypeId,
                    JewelryTypeName = g.Key.Name,
                    PurchaseTime = g.Count()
                })
                .OrderByDescending(x => x.PurchaseTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductRevenueDto>> GetTotalRevenueByProducts()
        {
            return await _context.BillJewelries
                .GroupBy(bj => new { bj.JewelryId, bj.Jewelry.Name })
                .Select(g => new ProductRevenueDto
                {
                    JewelryName = g.Key.Name,
                    JewelryRevenue = g.Sum(bj => bj.Jewelry.JewelryMaterials
                        .Sum(jm => (decimal)jm.GoldPrice.SellPrice * (decimal)jm.GoldQuantity + (decimal)jm.StonePrice.SellPrice * (decimal)jm.StoneQuantity))
                })
                .OrderByDescending(x => x.JewelryRevenue)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductTypeRevenueDto>> GetTotalRevenueByProductTypes()
        {
            return await _context.BillJewelries
                .GroupBy(bj => new { bj.Jewelry.JewelryTypeId, bj.Jewelry.JewelryType.Name })
                .Select(g => new ProductTypeRevenueDto
                {
                    JewelryTypeName = g.Key.Name,
                    JewelryTypeRevenue = g.Sum(bj => bj.Jewelry.JewelryMaterials
                        .Sum(jm => (decimal)jm.GoldPrice.SellPrice * (decimal)jm.GoldQuantity + (decimal)jm.StonePrice.SellPrice * (decimal)jm.StoneQuantity))
                })
                .OrderByDescending(x => x.JewelryTypeRevenue)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalRevenueByMonth(int month, int year)
        {
            var startDate = new DateTime(year, month, 1).ToUniversalTime();
            var endDate = startDate.AddMonths(1).ToUniversalTime();

            return await _context.BillJewelries
                .Where(bj => bj.Bill.SaleDate >= startDate && bj.Bill.SaleDate < endDate)
                .SumAsync(bj => bj.Jewelry.JewelryMaterials
                    .Sum(jm => (decimal)jm.GoldPrice.SellPrice * (decimal)jm.GoldQuantity + (decimal)jm.StonePrice.SellPrice * (decimal)jm.StoneQuantity));
        }
    }
}
