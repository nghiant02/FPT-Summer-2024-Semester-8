using BusinessObjects.Dto.Dashboard;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IDashboardService
    {
        Task<RevenueDto> GetTotalRevenueAllTime();
        Task<RevenueDto> GetTotalRevenue(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalRevenueByMonth(int month, int year);
        Task<RevenueByCounterDto> GetRevenueByCounter(string counterId);
        Task<RevenueByEmployeeDto> GetRevenueByEmployee(string userId);
        Task<RevenueByProductTypeDto> GetRevenueByProductType(string typeId);
        Task<int> GetTotalCustomers();
        Task<int> GetNewCustomers(DateTime startDate, DateTime endDate);
        Task<int> GetRepeatCustomers();
        Task<int> GetActiveCustomers(DateTime startDate, DateTime endDate);
        Task<IEnumerable<BestSellingProductDto>> GetBestSellingProducts();
        Task<IEnumerable<BestSellingProductTypeDto>> GetBestSellingProductTypes();
        Task<IEnumerable<ProductRevenueDto>> GetTotalRevenueByProducts();
        Task<IEnumerable<ProductTypeRevenueDto>> GetTotalRevenueByProductTypes();
    }
}
