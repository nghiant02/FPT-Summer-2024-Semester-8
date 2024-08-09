using BusinessObjects.Context;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace DAO.Dao
{
    public class WarrantyDao
    {
        private readonly JssatsContext _context;

        public WarrantyDao()
        {
            _context = new JssatsContext();
        }
        public async Task<IEnumerable<Warranty>?> GetWarranties()
        {
            return await _context.Warranties.ToListAsync();
        }
        public async Task<Warranty?> GetWarrantyById(string id)
        {
            return await _context.Warranties.FirstOrDefaultAsync(w=>w.WarrantyId == id);
        }
        public async Task<int> CreateWarranty(Warranty warranty)
        {
            warranty.WarrantyId = Generator.GenerateId();
            _context.Warranties.Add(warranty);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateWarranty(string id, Warranty warranty)
        {
            var existingWarranty = await _context.Warranties
                .FirstOrDefaultAsync(w => w.WarrantyId == id);
            warranty.WarrantyId = id;
            if (existingWarranty == null) return 0;
            _context.Entry(existingWarranty).CurrentValues.SetValues(warranty);
            _context.Entry(existingWarranty).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteWarranty(string id)
        {
            var warranty = await _context.Warranties.FindAsync(id);
            if (warranty == null) return 0;
            _context.Warranties.Remove(warranty);
            return await _context.SaveChangesAsync();
        }
    }
}
