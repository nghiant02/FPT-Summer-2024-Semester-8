using AutoMapper;
using EXE201.BLL.Interfaces;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Services
{
    public class DepositServices : IDepositServices
    {
        private readonly IDepositRepository _depositRepository;
        private readonly IMapper _mapper;

        public DepositServices(IDepositRepository depositRepository, IMapper mapper)
        {
            _depositRepository = depositRepository;
            _mapper = mapper;
        }

        public async Task<Deposit> GetDepositByIdAsync(int id)
        {
            return await _depositRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Deposit>> GetAllDepositAsync()
        {
            return await _depositRepository.GetAllAsync();
        }
    }
}
