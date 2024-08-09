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
    public class ColorServices : IColorServices
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorServices(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<ResponeModel> CreateColor(string colorName, string hexCode)
        {
            return await _colorRepository.CreateColor(colorName, hexCode);
        }

        public async Task<ResponeModel> DeleteColor(int colorId)
        {
            return await _colorRepository.DeleteColor(colorId);
        }

        public async Task<IEnumerable<Color>> GetAllColors()
        {
            return await _colorRepository.GetAllColors();
        }

        public async Task<ResponeModel> UpdateColor(int colorId, string newColorName, string newHexCode)
        {
            return await _colorRepository.UpdateColor(colorId, newColorName, newHexCode);
        }
    }
}
