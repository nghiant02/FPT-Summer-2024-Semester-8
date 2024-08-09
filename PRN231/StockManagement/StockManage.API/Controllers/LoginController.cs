using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StockManage.API.ViewModel;
using StockManage.BusinessObjects.Models;
using StockManage.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockManage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IAccountServices accountServices;
        private readonly IJwtServices jwtServices;

        public LoginController(IAccountServices accountServices, IJwtServices jwtServices)
        {
            this.accountServices = accountServices;
            this.jwtServices = jwtServices;
        }

        [HttpPost]
        public IActionResult Login([FromBody] AccountDTOs accountDTOs)
        {
            var account = accountServices.GetAccount(accountDTOs.Email, accountDTOs.Password);
            if (account == null)
            {
                return Unauthorized();
            }

            var token = jwtServices.GenerateToken(account.Email);
            return Ok(new { Token = token });
        }
    }
}
