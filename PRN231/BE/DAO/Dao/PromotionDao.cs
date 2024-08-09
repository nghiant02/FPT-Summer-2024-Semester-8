using BusinessObjects.Context;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace DAO.Dao
{
    public class PromotionDao 
    {
        private readonly JssatsContext _context;

        public PromotionDao()
        {
            _context = new JssatsContext();
        }

        public async Task<IEnumerable<Promotion>> GetPromotions()
        {
            return await _context.Promotions.ToListAsync();
        }
        public async Task<Promotion?> GetPromotionById(string id)
        {
            return await _context.Promotions.FindAsync(id);
        }
        public async Task<int> CreatePromotion(Promotion promotion)
        {
            promotion.PromotionId = Generator.GenerateId();
            await _context.Promotions.AddAsync(promotion);
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> UpdatePromotion(string id, Promotion promotion)
        {
            var existPromotion = await _context.Promotions.FirstOrDefaultAsync(x => x.PromotionId == id);
            if (existPromotion == null) return 0;
            existPromotion.Type = promotion.Type;
            existPromotion.Description = promotion.Description;
            existPromotion.EndDate = promotion.EndDate;
            existPromotion.StartDate = promotion.StartDate;
            existPromotion.ApproveManager = promotion.ApproveManager;
            existPromotion.DiscountRate = promotion.DiscountRate;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePromotion(string id)
        {
            var existPromotion = await _context.Promotions.FirstOrDefaultAsync(x => x.PromotionId == id);
            if (existPromotion == null) return 0;
            _context.Remove(existPromotion);
            return await _context.SaveChangesAsync();
        }
    }
}