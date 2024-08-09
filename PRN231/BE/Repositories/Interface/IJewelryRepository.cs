using BusinessObjects.Dto.Jewelry;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;
using Repositories.Interface.GenericRepository;

namespace Repositories.Interface;

public interface IJewelryRepository : IReadRepository<JewelryResponseDto>, ICreateRepository<Jewelry>,
    IUpdateRepository<Jewelry>, IDeleteRepository<Jewelry>
{
    Task<(int, int, IEnumerable<JewelryResponseDto>)> GetsJewelryPaging(int pageNumber, int pageSize,
        string? name, string? typeId);

    Task<(int, int, IEnumerable<JewelryResponseDto>)> GetsJewelryPagingByType(string jewelryTypeId, int pageNumber,
        int pageSize);
    Task<Jewelry?> GetJewelryById(string id);

    Task<int> GetSoldJewelryCount();
    public Task<int> UpdateJewelryWithMaterial(string id, JewelryRequestDto jewelryRequestDto);
    public Task<bool> DisableJewelry(string id);

}