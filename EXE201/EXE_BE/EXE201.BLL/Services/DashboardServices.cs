using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs.DashboardDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Services
{
    public class DashboardServices : IDashboardServices
    {
        private readonly IRentalOrderRepository _rentalOrderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;


        public DashboardServices(IRentalOrderRepository rentalOrderRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _rentalOrderRepository = rentalOrderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public decimal GetTotalRevenueAllTime()
        {
            return _rentalOrderRepository.GetTotalRevenueAllTime();
        }

        public Dictionary<string, decimal> GetMonthlyRevenue2024()
        {
            return _rentalOrderRepository.GetMonthlyRevenue2024();
        }

        public Dictionary<DateTime, decimal> GetRevenueLast7Days()
        {
            return _rentalOrderRepository.GetRevenueLast7Days();
        }

        public async Task<int> GetTotalUsersCustomer()
        {
            return await _userRepository.GetTotalUsersByRole("Customer");
        }

        public async Task<int> GetTotalUsersStaff()
        {
            return await _userRepository.GetTotalUsersByRole("Staff");
        }

        public async Task<int> GetTotalItemsInStock()
        {
            return await _productRepository.GetTotalItemsInStock();
        }

        public async Task<int> GetTotalReturnedOrders()
        {
            return await _rentalOrderRepository.GetTotalReturnedOrders();
        }
        public async Task<int> GetTotalCompletedRentalOrders()
        {
            return await _rentalOrderRepository.GetTotalCompletedRentalOrders();
        }

        public async Task<List<CategoryOrderCountDTO>> GetMostOrderedProductCategory()
        {
            return await _productRepository.GetMostOrderedProductCategory();
        }

        public async Task<IEnumerable<ProductStockDTO>> GetTotalItemsInStockForEachProduct()
        {
            return await _productRepository.GetTotalItemsInStockForEachProduct();
        }
    }
}
