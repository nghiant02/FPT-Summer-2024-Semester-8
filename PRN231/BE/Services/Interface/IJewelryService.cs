using BusinessObjects.Dto.Jewelry;
using BusinessObjects.Dto.Other;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;

namespace Services.Interface
{
    public interface IJewelryService
    {
        public Task<PagingResponse> GetJewelries(int pageNumber, int pageSize, string? name, string? typeId);
        public Task<JewelryResponseDto?> GetJewelryById(string id);
        public Task<PagingResponse?> GetJewelryByType(string jewelryTypeId, int pageNumBer, int pageSize);
        public Task<int> CreateJewelry(JewelryRequestDto jewelryRequestDto);
        public Task<int> UpdateJewelry(string id, JewelryRequestDto jewelryRequestDto);
        public Task<int> DeleteJewelry(string id);
        public Task<int> GetSoldJewelryCount();
        public Task<int> UpdateJewelryWithMaterial(string id, JewelryRequestDto jewelryRequestDto);
        public Task<bool> DisableJewelry(string id);
    }
}
