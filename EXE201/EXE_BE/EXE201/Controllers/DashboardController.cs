using EXE201.BLL.Interfaces;
using EXE201.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXE201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardServices _dashboardService;

        public DashboardController(IDashboardServices dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("TotalRevenue")]
        public IActionResult GetTotalRevenueAllTime()
        {
            var revenue = _dashboardService.GetTotalRevenueAllTime();
            return Ok(new { TotalRevenue = revenue });
        }

        [HttpGet("MonthlyRevenue2024")]
        public IActionResult GetMonthlyRevenue2024()
        {
            var revenue = _dashboardService.GetMonthlyRevenue2024();
            return Ok(revenue);
        }

        [HttpGet("RevenueLast7Days")]
        public IActionResult GetRevenueLast7Days()
        {
            var revenue = _dashboardService.GetRevenueLast7Days();
            return Ok(revenue);
        }


        [HttpGet("TotalUsersCustomer")]
        public async Task<IActionResult> GetTotalUsersCustomer()
        {
            var totalCustomers = await _dashboardService.GetTotalUsersCustomer();
            return Ok(new { totalCustomers = totalCustomers });
        }

        [HttpGet("TotalUsersStaff")]
        public async Task<IActionResult> GetTotalUsersStaff()
        {
            var totalStaff = await _dashboardService.GetTotalUsersStaff();
            return Ok(new { totalStaff = totalStaff });
        }

        [HttpGet("TotalItemsInStock")]
        public async Task<IActionResult> GetTotalItemsInStock()
        {
            var totalItemsInStock = await _dashboardService.GetTotalItemsInStock();
            return Ok(new { totalItemsInStock = totalItemsInStock });
        }

        [HttpGet("GetTotalItemsInStockForEachProduct")]
        public async Task<IActionResult> GetTotalItemsInStockForEachProduct()
        {
            var totalItemsInStock = await _dashboardService.GetTotalItemsInStockForEachProduct();
            return Ok(totalItemsInStock);
        }

        [HttpGet("TotalReturnedOrders")]
        public async Task<IActionResult> GetTotalReturnedOrders()
        {
            var totalReturnedOrders = await _dashboardService.GetTotalReturnedOrders();
            return Ok(new { totalReturnedOrders = totalReturnedOrders });
        }

        [HttpGet("TotalCompletedRentalOrders")]
        public async Task<IActionResult> GetTotalCompletedRentalOrders()
        {
            var totalCompletedOrders = await _dashboardService.GetTotalCompletedRentalOrders();
            return Ok(new { TotalCompletedOrders = totalCompletedOrders });
        }

        [HttpGet("MostOrderedProductCategory")]
        public async Task<IActionResult> GetMostOrderedProductCategory()
        {
            var mostOrderedCategories = await _dashboardService.GetMostOrderedProductCategory();
            return Ok(mostOrderedCategories);
        }
    }
}
