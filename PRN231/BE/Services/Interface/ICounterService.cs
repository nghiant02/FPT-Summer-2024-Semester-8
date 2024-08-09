using BusinessObjects.DTO;
using BusinessObjects.Dto.Counter;
using BusinessObjects.Dto.Other;
using BusinessObjects.Models;

namespace Services.Interface;

public interface ICounterService
{
    // public Task<PagingResponse> GetCounterPaging(int pageNumber, int pageSize);
    Task<IEnumerable<ViewCounterDto>?> GetCounters();
    Task<ViewCounterDto?> GetCounterById(string id);
    public Task<int> CreateCounter(CounterDto counterDto);
    public Task<int> UpdateCounter(string id, UpdateCounter counterDto);
    public Task<int> DeleteCounter(string id);
    Task<IEnumerable<CounterStatus>> GetAvailableCounters();
    Task CreateCounterMongo(CounterStatus counterDto);
}