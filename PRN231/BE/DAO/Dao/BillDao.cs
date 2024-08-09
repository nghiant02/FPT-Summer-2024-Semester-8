using BusinessObjects.Context;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace DAO.Dao
{
    public class BillDao
    {
        private readonly JssatsContext _context = new();
        public async Task<IEnumerable<Bill>> GetBills()
        {
            return await _context.Bills.ToListAsync();
        }

        public IQueryable<Bill> GetAllBills()
        {
            return _context.Bills.AsQueryable();
        }

        public async Task<Bill?> GetBillById(string id)
        {
            return await _context.Bills.FindAsync(id);
        }

        public async Task<string> CreateBill(Bill bill)
        {
            bill.BillId = Generator.GenerateId();
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
            return bill.BillId;
        }
        public async Task<int> UpdateBill(Bill bill)
        {
            _context.Bills.Update(bill);
            return await _context.SaveChangesAsync();
        }
    }
}
