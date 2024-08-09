using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Implements;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository 
    {
        public CategoryRepository(EXE201Context context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoriesByStatusAsync(string status)
        {
            return await _context.Categories.Where(c => c.CategoryStatus == status).ToListAsync();
        }

        public async Task<Category> GetByNameAsync(string categoryName)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName);
        }
    }
}
