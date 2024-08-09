using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly IJewelryService _jewelryService;

        public DashboardController(IDashboardService dashboardService, IJewelryService jewelryService)
        {
            _dashboardService = dashboardService;
            _jewelryService = jewelryService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("TotalRevenueAllTime")]
        public async Task<IActionResult> GetTotalRevenueAllTime()
        {
            var totalRevenue = await _dashboardService.GetTotalRevenueAllTime();
            return Ok(totalRevenue);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetTotalRevenue")]
        public async Task<IActionResult> GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            var result = await _dashboardService.GetTotalRevenue(startDate, endDate);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("TotalRevenueByMonth")]
        public async Task<IActionResult> GetTotalRevenueByMonth([FromQuery] int month, [FromQuery] int year)
        {
            var totalRevenue = await _dashboardService.GetTotalRevenueByMonth(month, year);
            return Ok(totalRevenue);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetRevenueByCounter")]
        public async Task<IActionResult> GetRevenueByCounter(string counterId)
        {
            var result = await _dashboardService.GetRevenueByCounter(counterId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetRevenueByEmployee")]
        public async Task<IActionResult> GetRevenueByEmployee(string userId)
        {
            var result = await _dashboardService.GetRevenueByEmployee(userId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetRevenueByProductType")]
        public async Task<IActionResult> GetRevenueByProductType(string typeId)
        {
            var result = await _dashboardService.GetRevenueByProductType(typeId);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("TotalCustomers")]
        public async Task<IActionResult> GetTotalCustomers()
        {
            var totalCustomers = await _dashboardService.GetTotalCustomers();
            return Ok(totalCustomers);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("NewCustomers")]
        public async Task<IActionResult> GetNewCustomers([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var newCustomers = await _dashboardService.GetNewCustomers(startDate, endDate);
            return Ok(newCustomers);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("RepeatCustomers")]
        public async Task<IActionResult> GetRepeatCustomers()
        {
            var repeatCustomers = await _dashboardService.GetRepeatCustomers();
            return Ok(repeatCustomers);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("ActiveCustomers")]
        public async Task<IActionResult> GetActiveCustomers([FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var activeCustomers = await _dashboardService.GetActiveCustomers(startDate, endDate);
            return Ok(activeCustomers);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("BestSellingJewelry")]
        public async Task<IActionResult> GetBestSellingProducts()
        {
            var bestSellingProducts = await _dashboardService.GetBestSellingProducts();
            return Ok(bestSellingProducts);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("BestSellingJewelryTypes")]
        public async Task<IActionResult> GetBestSellingProductTypes()
        {
            var bestSellingProductTypes = await _dashboardService.GetBestSellingProductTypes();
            return Ok(bestSellingProductTypes);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("TotalRevenueByJewelry")]
        public async Task<IActionResult> GetTotalRevenueByProducts()
        {
            var totalRevenueByProducts = await _dashboardService.GetTotalRevenueByProducts();
            return Ok(totalRevenueByProducts);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("TotalRevenueByJewelryTypes")]
        public async Task<IActionResult> GetTotalRevenueByProductTypes()
        {
            var totalRevenueByProductTypes = await _dashboardService.GetTotalRevenueByProductTypes();
            return Ok(totalRevenueByProductTypes);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("TotalSoldJewelry")]
        public async Task<IActionResult> GetTotalSoldJewelry()
        {
            return Ok(await _jewelryService.GetSoldJewelryCount());
        }
    }
}