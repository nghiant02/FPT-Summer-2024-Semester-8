using EXE201.DAL.DTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Implements;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EXE201.DAL.Repository
{
    public class SizeRepository : GenericRepository<Size>, ISizeRepository
    {
        private readonly EXE201Context _context;

        public SizeRepository(EXE201Context context) : base(context)
        {
            _context = context;
        }

        // View all sizes
        public async Task<IEnumerable<Size>> GetAllSizes()
        {
            return await _context.Sizes.ToListAsync();
        }

        // Create a new size
        public async Task<ResponeModel> CreateSize(string sizeName)
        {
            if (string.IsNullOrEmpty(sizeName))
            {
                return new ResponeModel { Status = "Error", Message = "Size name is required" };
            }

            // Check if size name already exists
            var exists = await _context.Sizes.AnyAsync(s => s.SizeName == sizeName);
            if (exists)
            {
                return new ResponeModel { Status = "Error", Message = "Size name already exists" };
            }

            var size = new Size { SizeName = sizeName };

            _context.Sizes.Add(size);
            await _context.SaveChangesAsync();

            return new ResponeModel { Status = "Success", Message = "Size created successfully", DataObject = size };
        }

        // Delete a size by ID
        public async Task<ResponeModel> DeleteSize(int sizeId)
        {
            var size = await _context.Sizes
                .Include(s => s.ProductSizes)
                .FirstOrDefaultAsync(s => s.SizeId == sizeId);

            if (size == null)
            {
                return new ResponeModel { Status = "Error", Message = "Size not found" };
            }

            if (size.ProductSizes.Any())
            {
                return new ResponeModel { Status = "Error", Message = "Cannot delete size. It is associated with one or more products." };
            }

            _context.Sizes.Remove(size);
            await _context.SaveChangesAsync();

            return new ResponeModel { Status = "Success", Message = "Size deleted successfully" };
        }

        // Update a size by ID
        public async Task<ResponeModel> UpdateSize(int sizeId, string newSizeName)
        {
            var size = await _context.Sizes.FindAsync(sizeId);

            if (size == null)
            {
                return new ResponeModel { Status = "Error", Message = "Size not found" };
            }

            if (string.IsNullOrEmpty(newSizeName))
            {
                return new ResponeModel { Status = "Error", Message = "New size name is required" };
            }

            // Check if new size name already exists
            var exists = await _context.Sizes.AnyAsync(s => s.SizeName == newSizeName && s.SizeId != sizeId);
            if (exists)
            {
                return new ResponeModel { Status = "Error", Message = "Size name already exists" };
            }

            size.SizeName = newSizeName;
            _context.Sizes.Update(size);
            await _context.SaveChangesAsync();

            return new ResponeModel { Status = "Success", Message = "Size updated successfully", DataObject = size };
        }
    }
}
