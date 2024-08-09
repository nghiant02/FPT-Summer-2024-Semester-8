using EXE201.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EXE201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DepositController : Controller
    {
        private readonly IDepositServices _depositServices;

        public DepositController(IDepositServices depositServices)
        {
            _depositServices = depositServices;
        }

        [HttpGet("GetDepositByUserId")]
        public async Task<IActionResult> GetDepositByUserId(int userId)
        {
            var result = await _depositServices.GetDepositByIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("GetAllDeposit")]
        public async Task<IActionResult> GetAllDeposit()
        {
            var result = await _depositServices.GetAllDepositAsync();
            return Ok(result);
        }
    }
}
