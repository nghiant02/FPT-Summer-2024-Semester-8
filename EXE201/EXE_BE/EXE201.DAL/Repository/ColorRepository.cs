using EXE201.DAL.DTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Implements;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EXE201.DAL.Repository
{
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        private readonly EXE201Context _context;

        public ColorRepository(EXE201Context context) : base(context)
        {
            _context = context;
        }

        // View all colors
        public async Task<IEnumerable<Color>> GetAllColors()
        {
            return await _context.Colors.ToListAsync();
        }

        // Create a new color
        public async Task<ResponeModel> CreateColor(string colorName, string hexCode)
        {
            if (string.IsNullOrEmpty(colorName))
            {
                return new ResponeModel { Status = "Error", Message = "Color name is required" };
            }

            // Check if color name already exists
            var exists = await _context.Colors.AnyAsync(c => c.ColorName == colorName);
            if (exists)
            {
                return new ResponeModel { Status = "Error", Message = "Color name already exists" };
            }

            var color = new Color { ColorName = colorName, HexCode = hexCode};


            _context.Colors.Add(color);
            await _context.SaveChangesAsync();

            return new ResponeModel { Status = "Success", Message = "Color created successfully", DataObject = color };
        }

        // Delete a color by ID
        public async Task<ResponeModel> DeleteColor(int colorId)
        {
            var color = await _context.Colors
                .Include(c => c.ProductColors)
                .FirstOrDefaultAsync(c => c.ColorId == colorId);

            if (color == null)
            {
                return new ResponeModel { Status = "Error", Message = "Color not found" };
            }

            if (color.ProductColors.Any())
            {
                return new ResponeModel { Status = "Error", Message = "Cannot delete color. It is associated with one or more products." };
            }

            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();

            return new ResponeModel { Status = "Success", Message = "Color deleted successfully" };
        }

        // Update a color by ID
        public async Task<ResponeModel> UpdateColor(int colorId, string newColorName, string newHexCode)
        {
            var color = await _context.Colors.FindAsync(colorId);

            if (color == null)
            {
                return new ResponeModel { Status = "Error", Message = "Color not found" };
            }

            if (string.IsNullOrEmpty(newColorName))
            {
                return new ResponeModel { Status = "Error", Message = "New color name is required" };
            }

            // Check if new color name already exists
            var exists = await _context.Colors.AnyAsync(c => c.ColorName == newColorName && c.ColorId != colorId);
            if (exists)
            {
                return new ResponeModel { Status = "Error", Message = "Color name already exists" };
            }

            color.HexCode = newHexCode;
            color.ColorName = newColorName;
            _context.Colors.Update(color);
            await _context.SaveChangesAsync();

            return new ResponeModel { Status = "Success", Message = "Color updated successfully", DataObject = color };
        }
    }
}
