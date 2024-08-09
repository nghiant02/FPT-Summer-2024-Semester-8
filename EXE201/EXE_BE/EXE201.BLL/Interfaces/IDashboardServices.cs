using EXE201.DAL.DTOs.DashboardDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Interfaces
{
    public interface IDashboardServices
    {
        decimal GetTotalRevenueAllTime();
        Dictionary<string, decimal> GetMonthlyRevenue2024();
        Dictionary<DateTime, decimal> GetRevenueLast7Days();
        Task<int> GetTotalUsersCustomer();

        Task<int> GetTotalUsersStaff();

        Task<int> GetTotalItemsInStock();

        Task<int> GetTotalReturnedOrders();
        Task<int> GetTotalCompletedRentalOrders();
        Task<List<CategoryOrderCountDTO>> GetMostOrderedProductCategory();
        Task<IEnumerable<ProductStockDTO>> GetTotalItemsInStockForEachProduct();
    }
}
