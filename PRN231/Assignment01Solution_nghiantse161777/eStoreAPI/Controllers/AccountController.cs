using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessObject.DTO;
using DataAccess.Repositories;
using eStoreAPI.Utils;
using Microsoft.Extensions.Configuration;
using DataAccess.Interface;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly JwtConfig _jwtConfig;
        private readonly IConfiguration _configuration;

        public AccountController(IMemberRepository memberRepository, JwtConfig jwtConfig, IConfiguration configuration)
        {
            _memberRepository = memberRepository;
            _jwtConfig = jwtConfig;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var adminEmail = _configuration["AdminAccount:Email"];
            var adminPassword = _configuration["AdminAccount:Password"];
            string token;

            if (loginDto.Email == adminEmail && loginDto.Password == adminPassword)
            {
                token = JwtHelper.GenerateJwtToken(adminEmail, "Admin", _jwtConfig);
                return Ok(token);
            }

            var member = await _memberRepository.GetMemberByEmailAsync(loginDto.Email);
            if (member == null || member.Password != loginDto.Password)
            {
                return Unauthorized("Invalid email or password.");
            }

            var role = "User";
            token = JwtHelper.GenerateJwtToken(member.Email, role, _jwtConfig);

            return Ok(token);
        }
    }
}
