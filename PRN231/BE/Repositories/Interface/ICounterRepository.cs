using System.Diagnostics.Metrics;
using BusinessObjects.DTO;
using BusinessObjects.Dto.Counter;
using BusinessObjects.Models;
using Repositories.Interface.GenericRepository;

namespace Repositories.Interface;

public interface ICounterRepository : IReadRepository<Counter>, IDeleteRepository<Counter>
{
    Task<int> Create(CounterDto entity);
    Task<int> Update(string id, UpdateCounter entity);
    Task<IEnumerable<CounterStatus>> GetAvailableCounters();
    Task AddMongo(CounterStatus counterStatus);
    Task<IEnumerable<ViewCounterDto>?> GetCounters();
    Task<ViewCounterDto?> GetCounterById(string id);
}