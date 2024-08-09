using System.Diagnostics.Metrics;
using BusinessObjects.DTO;
using BusinessObjects.Dto.Counter;
using BusinessObjects.Dto.Other;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implementation
{
    public class CounterService(ICounterRepository counterRepository) : ICounterService
    {
        private ICounterRepository CounterRepository { get; } = counterRepository;

        public async Task<IEnumerable<ViewCounterDto>?> GetCounters()
        {
            return await CounterRepository.GetCounters();
        }

        public async Task<ViewCounterDto?> GetCounterById(string id)
        {
            return await CounterRepository.GetCounterById(id);
        }

        public async Task<int> CreateCounter(CounterDto counterDto)
        {
            return await CounterRepository.Create(counterDto);
        }
        public async Task CreateCounterMongo(CounterStatus counterDto)
        {
            await CounterRepository.AddMongo(counterDto);
        }

        public async Task<int> UpdateCounter(string id, UpdateCounter counterDto)
        {
            return await CounterRepository.Update(id, counterDto);
        }

        public async Task<int> DeleteCounter(string id)
        {
            return await CounterRepository.Delete(id);
        }
        
        public async Task<IEnumerable<CounterStatus>> GetAvailableCounters()
        {
            return await CounterRepository.GetAvailableCounters();
        }
    }
}