using BusinessObject.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace PEPRN231_SU24TrialTest_StudentName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {

        private readonly IUserAccountRepository _userAccountRepository;
        private readonly ITokenRepository _tokenRepository;
        public UserAccountController(IUserAccountRepository userAccountRepository, ITokenRepository tokenRepository)
        {
            _userAccountRepository = userAccountRepository;
            _tokenRepository = tokenRepository;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _userAccountRepository.Login(loginDto.Email, loginDto.Password);
            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("LoginToken")]
        public async Task<IActionResult> LoginToken(LoginDto loginDto)
        {
            var user = await _userAccountRepository.Login(loginDto.Email, loginDto.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            if (user.Role != 2 && user.Role != 3) return Unauthorized();
            var result = _tokenRepository.GenerateAccessToken(user.UserEmail, (int)user.Role);

            return Ok(result);
        }
    }
}
