using BusinessObjects.Context;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Tools;

namespace DAO.Dao
{
    public class CustomerDao
    {
        public readonly JssatsContext _context;
        public CustomerDao()
        {
            _context = new JssatsContext();
        }
        public async Task<(int,int,IEnumerable<Customer>)> GetCustomersPaging(int pageNumber, int pageSize)
        {
            var totalRecord = await _context.Customers.CountAsync();
            var totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            var customers = await _context.Customers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (totalRecord,totalPage, customers);
        }
        public async Task<IEnumerable<Customer>?> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<Customer?> GetCustomerById(string id)
        {
            return await _context.Customers.FindAsync(id);
        }
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            customer.CustomerId = Generator.GenerateId();
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<int> UpdateCustomer(string id,Customer customer)
        {
            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);
            if (existingCustomer == null) return 0;
            customer.CustomerId = id;
            _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
            _context.Entry(existingCustomer).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteCustomer(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return 0;
            }
            _context.Customers.Remove(customer);
            return await _context.SaveChangesAsync();
        }
        public async Task<Customer?> GetCustomerByBillId(string? billId)
        {
            var bill = await _context.Bills.FindAsync(billId);
            if (bill == null) return null;
            return await _context.Customers.FindAsync(bill.CustomerId);
        }
        public async Task<Customer?> GetCustomerByPhone(string phoneNumber)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Phone == phoneNumber);
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return _context.Customers.AsQueryable();
        }
        public async Task<Customer> CustomerLogin(string phone, string password)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Phone == phone && c.Password == password);
        }
        public async Task<bool> RegisterCustomer(Customer customer)
        {
            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Phone == customer.Phone);
            if (existingCustomer != null) return false;
            customer.CustomerId = Generator.GenerateId();
            customer.CreatedAt = DateTime.UtcNow.ToUniversalTime();
            customer.Point = 0;
            _context.Customers.Add(customer);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<string> GetCustomerIdByName(string name)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.FullName == name);
            return customer?.CustomerId;
        }
        public async Task<int> AddPoint(string customerId, int point)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) return 0;
            customer.Point += point;
            return await _context.SaveChangesAsync();
        }
    }
}