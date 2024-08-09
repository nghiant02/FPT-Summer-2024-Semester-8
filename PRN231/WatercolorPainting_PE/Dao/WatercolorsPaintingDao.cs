using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class WatercolorsPaintingDao
    {
        private readonly WatercolorsPainting2024DbContext _context;
        public WatercolorsPaintingDao()
        {
            _context = new WatercolorsPainting2024DbContext();
        }

        public async Task<IEnumerable<WatercolorsPainting>> GetWatercolorsPaintings()
        {
            return await _context.WatercolorsPaintings.Include(s=>s.Style).ToListAsync();
        }
        public async Task<WatercolorsPainting> GetWatercolorsPaintingId(string id)
        {
            return await _context.WatercolorsPaintings.Include(s=>s.Style).FirstOrDefaultAsync(p=> p.PaintingId == id);
        }
        public async Task<bool> AddWatercolorsPainting(WatercolorsPainting watercolorsPainting)
        {
            try
            {
                watercolorsPainting.PaintingId = Guid.NewGuid().ToString();
                watercolorsPainting.CreatedDate = DateTime.UtcNow;
                _context.WatercolorsPaintings.Add(watercolorsPainting);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } 
        public async Task<bool> UpdateWatercolorsPainting(string id,WatercolorsPainting watercolorsPainting)
        {
            try
            {
                var exitPainting = await _context.WatercolorsPaintings.FindAsync(id);

                exitPainting.PaintingAuthor = watercolorsPainting.PaintingAuthor;
                exitPainting.PublishYear = watercolorsPainting.PublishYear;
                exitPainting.PaintingName = watercolorsPainting.PaintingName;
                exitPainting.PaintingDescription = watercolorsPainting.PaintingDescription;
                exitPainting.Price = watercolorsPainting.Price;
                exitPainting.StyleId = watercolorsPainting.StyleId;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteWatercolorsPainting(string id)
        {
            try
            {
                var painting = await _context.WatercolorsPaintings.FindAsync(id);
                _context.WatercolorsPaintings.Remove(painting);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
