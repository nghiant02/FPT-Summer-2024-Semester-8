using AutoMapper;
using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using EXE201.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Services
{
    public class SizeServices : ISizeServices
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;

        public SizeServices(ISizeRepository sizeRepository, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }

        public async Task<ResponeModel> CreateSize(string sizeName)
        {
            return await _sizeRepository.CreateSize(sizeName);
        }

        public async Task<ResponeModel> DeleteSize(int sizeId)
        {
            return await _sizeRepository.DeleteSize(sizeId);
        }

        public async Task<IEnumerable<Size>> GetAllSizes()
        {
            return await _sizeRepository.GetAllSizes();
        }

        public async Task<ResponeModel> UpdateSize(int sizeId, string newSizeName)
        {
            return await _sizeRepository.UpdateSize(sizeId, newSizeName);
        }
    }
}
