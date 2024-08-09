using BusinessObjects.Context;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace DAO.Dao
{
    public class PurchaseDao
    {
        private readonly JssatsContext _context;

        public PurchaseDao()
        {
            _context = new JssatsContext();
        }

        public async Task<Purchase?> GetPurchaseByJewelryIdWithBuyBack0(string jewelryId)
        {
            return await _context.Purchases
                .Include(p => p.Jewelry)
                .ThenInclude(j => j.JewelryMaterials)
                .ThenInclude(jm => jm.GoldPrice)
                .Include(p => p.Jewelry)
                .ThenInclude(j => j.JewelryMaterials)
                .ThenInclude(jm => jm.StonePrice)
                .FirstOrDefaultAsync(p => p.JewelryId == jewelryId && p.IsBuyBack == 0);
        }

        public async Task<Purchase?> GetPurchaseByJewelryId(string jewelryId)
        {
            return await _context.Purchases
                .Include(p => p.Jewelry)
                .ThenInclude(j => j.JewelryMaterials)
                .ThenInclude(jm => jm.GoldPrice)
                .Include(p => p.Jewelry)
                .ThenInclude(j => j.JewelryMaterials)
                .ThenInclude(jm => jm.StonePrice)
                .FirstOrDefaultAsync(p => p.JewelryId == jewelryId);
        }

        public async Task<Jewelry?> GetJewelryById(string jewelryId)
        {
            return await _context.Jewelries
                .Include(j => j.JewelryMaterials)
                .ThenInclude(jm => jm.GoldPrice)
                .Include(j => j.JewelryMaterials)
                .ThenInclude(jm => jm.StonePrice)
                .FirstOrDefaultAsync(j => j.JewelryId == jewelryId);
        }

        public async Task<JewelryMaterial?> GetJewelryMaterialByJewelryId(string jewelryId)
        {
            return await _context.JewelryMaterials
                .Include(jm => jm.GoldPrice)
                .Include(jm => jm.StonePrice)
                .FirstOrDefaultAsync(jm => jm.JewelryId == jewelryId);
        }

        public async Task<Gold?> GetGoldPriceById(string goldPriceId)
        {
            return await _context.Golds.FirstOrDefaultAsync(gp => gp.GoldId == goldPriceId);
        }

        public async Task<Gem?> GetStonePriceById(string stonePriceId)
        {
            return await _context.Gems.FirstOrDefaultAsync(sp => sp.GemId == stonePriceId);
        }

        public async Task CreateJewelry(Jewelry jewelry)
        {
            await _context.Jewelries.AddAsync(jewelry);
            await _context.SaveChangesAsync();
        }

        public async Task CreateJewelryMaterial(JewelryMaterial jewelryMaterial)
        {
            _context.JewelryMaterials.Add(jewelryMaterial);
            await _context.SaveChangesAsync();
        }

        public async Task CreatePurchase(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
        }

        public async Task<Purchase?> UpdatePurchase(string id, Purchase purchase)
        {
            var existingPurchase = await _context.Purchases.FindAsync(id);
            if (existingPurchase == null)
            {
                throw new ArgumentException("Purchase not found");
            }
            _context.Entry(existingPurchase).CurrentValues.SetValues(purchase);
            await _context.SaveChangesAsync();
            return existingPurchase;
        }
    }
}
