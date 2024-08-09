using BusinessObjects.Models;
using Repositories.Interface;
using Services.Interface;
using AutoMapper;
using BusinessObjects.Dto;

namespace Services.Implementation
{
    public class WarrantyService(IWarrantyRepository warrantyRepository, IMapper mapper) : IWarrantyService
    {
        private IWarrantyRepository WarrantyRepository { get; } = warrantyRepository;
        private IMapper Mapper { get; } = mapper;

        public Task<int> CreateWarranty(WarrantyDto warrantyDto)
        {
            var warranty = Mapper.Map<Warranty>(warrantyDto);
            return WarrantyRepository.Create(warranty);
        }

        public Task<int> DeleteWarranty(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Warranty?>?> GetWarranties()
        {
            return await WarrantyRepository.Gets();
        }

        public async Task<Warranty?> GetWarrantyById(string id)
        {
            return await WarrantyRepository.GetById(id);
        }

        public async Task<int> UpdateWarranty(string id,WarrantyDto warrantyDto)
        {
            var warranty = Mapper.Map<Warranty>(warrantyDto);
            return await WarrantyRepository.Update(id,warranty);
        }
    }
}
