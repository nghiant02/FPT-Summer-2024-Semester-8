using EXE201.DAL.DTOs.CategoryDTOs;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Interfaces
{
    public interface ICategoryServices
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(AddCategoryDTOs category);
        Task UpdateCategoryAsync(UpdateCategoryDTOs category);
        Task UpdateCategoryStatusAsync(int id, string status);
        Task<IEnumerable<Category>> GetCategoriesByStatusAsync(string status);
    }
}
