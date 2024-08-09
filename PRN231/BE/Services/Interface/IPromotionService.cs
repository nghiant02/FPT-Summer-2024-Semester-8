using BusinessObjects.Dto;
using BusinessObjects.Models;

namespace Services.Interface
{
    public interface IPromotionService
    {
        public Task<int> CreatePromotion(string id, PromotionDto promotion);
        public Task<IEnumerable<Promotion?>?> GetPromotions();
        public Task<Promotion?> GetPromotionById(string id);
        public Task<int> UpdatePromotion(string id, PromotionDto promotion);
        public Task<int> DeletePromotion(string id);
    }
}
