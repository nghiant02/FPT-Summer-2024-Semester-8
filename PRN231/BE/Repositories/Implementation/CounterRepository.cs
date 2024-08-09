using BusinessObjects.DTO;
using BusinessObjects.Dto.Counter;
using BusinessObjects.Models;
using DAO.Dao;
using Repositories.Interface;
using Tools;

namespace Repositories.Implementation;

public class CounterRepository(CounterDao counterDao) : ICounterRepository
{
    private CounterDao CounterDao { get; } = counterDao;

    public async Task<IEnumerable<ViewCounterDto>?> GetCounters()
    {
        return await CounterDao.GetCounters();
    }

    public async Task<ViewCounterDto?> GetCounterById(string id)
    {
        return await CounterDao.GetCounterByCounterId(id);
    }

    public async Task<int> Create(CounterDto entity)
    {
        var counter = new Counter
        {
            CounterId = Generator.GenerateId(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Number = entity.Number,
        };
        return await CounterDao.CreateCounter(counter);
    }

    public async Task AddMongo(CounterStatus counterStatus)
    {
        await CounterDao.AddCounter(counterStatus);
    }

    public async Task<int> Delete(string id)
    {
        return await CounterDao.DeleteCounter(id);
    }

    public async Task<int> Update(string id, UpdateCounter entity)
    {
        return await CounterDao.UpdateCounter(id, entity);
    }

    public async Task<IEnumerable<CounterStatus>> GetAvailableCounters()
    {
        return await CounterDao.GetAvailableCountersv2();
    }

    public Task<IEnumerable<Counter>?> Gets()
    {
        throw new NotImplementedException();
    }

    public async Task<Counter?> GetById(string id)
    {
        return await CounterDao.GetCounterByIdv2(id);
    }
}