using BusinessObjects.Dto.Dashboard;
using BusinessObjects.Models;
using Repositories.Implementation;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillJewelryRepository _billJewelryRepository;
        private readonly ICustomerRepository _customerRepository;

        public DashboardService(IBillRepository billRepository, ICustomerRepository customerRepository, IBillJewelryRepository billJewelryRepository)
        {
            _billRepository = billRepository;
            _customerRepository = customerRepository;
            _billJewelryRepository = billJewelryRepository;
        }
        public async Task<RevenueDto> GetTotalRevenueAllTime()
        {
            var totalRevenue = await _billRepository.GetTotalRevenueAllTime();
            return new RevenueDto { TotalRevenue = totalRevenue };
        }

        public async Task<RevenueDto> GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            var totalRevenue = await _billRepository.GetTotalRevenue(startDate, endDate);
            return new RevenueDto { TotalRevenue = totalRevenue };
        }

        public async Task<decimal> GetTotalRevenueByMonth(int month, int year)
        {
            return await _billJewelryRepository.GetTotalRevenueByMonth(month, year);
        }

        public async Task<RevenueByCounterDto> GetRevenueByCounter(string counterId)
        {
            return await _billRepository.GetRevenueByCounter(counterId);
        }

        public async Task<RevenueByEmployeeDto> GetRevenueByEmployee(string userId)
        {
            return await _billRepository.GetRevenueByEmployee(userId);
        }

        public async Task<RevenueByProductTypeDto> GetRevenueByProductType(string typeId)
        {
            return await _billRepository.GetRevenueByProductType(typeId);
        }

        public async Task<int> GetTotalCustomers()
        {
            return await _customerRepository.GetTotalCustomers();
        }

        public async Task<int> GetNewCustomers(DateTime startDate, DateTime endDate)
        {
            return await _customerRepository.GetNewCustomers(startDate, endDate);
        }

        public async Task<int> GetRepeatCustomers()
        {
            return await _customerRepository.GetRepeatCustomers();
        }

        public async Task<int> GetActiveCustomers(DateTime startDate, DateTime endDate)
        {
            return await _customerRepository.GetActiveCustomers(startDate, endDate);
        }

        public async Task<IEnumerable<BestSellingProductDto>> GetBestSellingProducts()
        {
            return await _billJewelryRepository.GetBestSellingProducts();
        }

        public async Task<IEnumerable<BestSellingProductTypeDto>> GetBestSellingProductTypes()
        {
            return await _billJewelryRepository.GetBestSellingProductTypes();
        }

        public async Task<IEnumerable<ProductRevenueDto>> GetTotalRevenueByProducts()
        {
            return await _billJewelryRepository.GetTotalRevenueByProducts();
        }

        public async Task<IEnumerable<ProductTypeRevenueDto>> GetTotalRevenueByProductTypes()
        {
            return await _billJewelryRepository.GetTotalRevenueByProductTypes();
        }
    }
}
