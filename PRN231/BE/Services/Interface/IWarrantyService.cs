using BusinessObjects.Dto;
using BusinessObjects.Models;

namespace Services.Interface
{
    public interface IWarrantyService
    {
        public Task<int> CreateWarranty(WarrantyDto warranty);
        public Task<IEnumerable<Warranty?>?> GetWarranties();
        public Task<Warranty?> GetWarrantyById(string id);
        public Task<int> UpdateWarranty(string id ,WarrantyDto warranty);
        public Task<int> DeleteWarranty(string id);
    }
}
