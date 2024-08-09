using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManage.API.ViewModel;
using StockManage.BusinessObjects.Models;
using StockManage.Services;

namespace StockManage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices accountServices;

        public AccountController(IAccountServices accountServices)
        {
            this.accountServices = accountServices;
        }

        [HttpGet("getAll")]
        [Authorize]
        public IActionResult GetAllAccount()
        {
            return Ok(accountServices.GetAllAccount());
        }
    }
}
