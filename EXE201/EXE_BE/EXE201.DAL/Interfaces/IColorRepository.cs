using EXE201.DAL.DTOs;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.Interfaces
{
    public interface IColorRepository
    {
        Task<IEnumerable<Color>> GetAllColors();
        Task<ResponeModel> CreateColor(string colorName, string hexCode);
        Task<ResponeModel> DeleteColor(int colorId);
        Task<ResponeModel> UpdateColor(int colorId, string newColorName, string newHexCode);
    }
}
