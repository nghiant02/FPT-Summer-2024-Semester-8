using BusinessObjects.Dto.Dashboard;
using BusinessObjects.Models;
using Repositories.Interface.GenericRepository;

namespace Repositories.Interface;

public interface IBillRepository : IReadRepository<Bill>, ICreateRepository<Bill>
{
    Task<string> CreateBill(Bill bill);
    Task<int> UpdateBill(Bill bill);
    Task<decimal> GetTotalRevenueAllTime();
    Task<decimal> GetTotalRevenue(DateTime startDate, DateTime endDate);
    Task<RevenueByCounterDto> GetRevenueByCounter(string counterId);
    Task<RevenueByEmployeeDto> GetRevenueByEmployee(string userId);
    Task<RevenueByProductTypeDto> GetRevenueByProductType(string typeId);
}
