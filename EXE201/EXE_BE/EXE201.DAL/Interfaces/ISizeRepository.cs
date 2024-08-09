using EXE201.DAL.DTOs;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.Interfaces
{
    public interface ISizeRepository
    {
        Task<IEnumerable<Size>> GetAllSizes();
        Task<ResponeModel> CreateSize(string sizeName);
        Task<ResponeModel> UpdateSize(int sizeId, string newSizeName);
        Task<ResponeModel> DeleteSize(int sizeId);
    }
}
