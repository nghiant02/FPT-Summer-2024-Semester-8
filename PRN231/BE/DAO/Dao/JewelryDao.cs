using BusinessObjects.Context;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace DAO.Dao
{
    public class JewelryDao
    {
        private readonly JssatsContext _context;
        public JewelryDao()
        {
            _context = new JssatsContext();
        }
        public async Task<(int,int,IEnumerable<Jewelry>)> GetJewelries(int pageNumber, int pageSize)
        {
            var totalRecord = await _context.Jewelries.CountAsync(j=>j.IsSold == false && j.IsEnable == true);
            var totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            var jewelries = await _context.Jewelries
                .Where(x => x.IsSold == false && x.IsEnable == true)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (totalRecord,totalPage, jewelries);
        }
        [Obsolete]
        public async Task<(int,int,IEnumerable<Jewelry>)> GetJewelriesByType(string jewelryTypeId, int pageNumber, int pageSize)
        {
            var totalRecord = await _context.Jewelries.Where(x => x.JewelryTypeId  == jewelryTypeId).CountAsync();
            var totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            var jewelries = await _context.Jewelries
                .Where(x => x.JewelryTypeId == jewelryTypeId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (totalRecord,totalPage, jewelries);
        }

        public async Task<Jewelry?> GetJewelryById(string id)
        {
           var jewelry = await _context.Jewelries.Where(j=>j.JewelryId == id).FirstOrDefaultAsync();
           return jewelry;
        }

        public async Task<int> CreateJewelry(Jewelry jewelry)
        {
            
            try
            {
                jewelry.JewelryId = Generator.GenerateId();
                jewelry.IsSold = false;
                jewelry.IsEnable = true;
                _context.Jewelries.Add(jewelry);
            }
            catch {
                throw new Exception("Error while creating jewelry");
            }
           
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateJewelry(string id, Jewelry jewelry)
        {
            var existingJewelry = await _context.Jewelries
                .FirstOrDefaultAsync(w => w.JewelryId == id);
            jewelry.JewelryId = id;
            if (existingJewelry == null) return 0;
            _context.Entry(existingJewelry).CurrentValues.SetValues(jewelry);
            _context.Entry(existingJewelry).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteJewelry(string id)
        {
            var jewelry = await _context.Jewelries.FindAsync(id);
            _context.Jewelries.Remove(jewelry);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> IsSold(int id)
        {
            var jewelry = await _context.Jewelries.FindAsync(id);
            return jewelry?.IsSold ?? false;
        }
        public async Task<IEnumerable<Jewelry>> GetJewelriesByBillId(string billId)
        {
            return await _context.Jewelries
                .Where(j => j.BillJewelries.Any(bj => bj.BillId == billId))
                .ToListAsync();
        }
        public async Task<int> GetTotalSellJewelry()
        {
            return await _context.Jewelries.CountAsync(j => j.IsSold == true);
        }
        public async Task<bool> DisableJewelry(string id)
        {
            var jewelry = await _context.Jewelries.FindAsync(id);
            if (jewelry == null) return false;
            jewelry.IsEnable = false;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
