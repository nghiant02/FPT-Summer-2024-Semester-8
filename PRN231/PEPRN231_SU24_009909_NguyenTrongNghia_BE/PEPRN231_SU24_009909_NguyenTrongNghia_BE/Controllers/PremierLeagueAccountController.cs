using BusinessObjects.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace PEPRN231_SU24_009909_NguyenTrongNghia_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremierLeagueAccountController : ControllerBase
    {
        private readonly IPremierLeagueAccountRepository _premierLeagueAccountRepository;
        private readonly ITokenRepository _tokenRepository;

        public PremierLeagueAccountController(IPremierLeagueAccountRepository premierLeagueAccountRepository, ITokenRepository tokenRepository)
        {
            _premierLeagueAccountRepository = premierLeagueAccountRepository;
            _tokenRepository = tokenRepository;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _premierLeagueAccountRepository.Login(loginDto.Email, loginDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("LoginToken")]
        public async Task<IActionResult> LoginToken(LoginDto loginDto)
        {
            var user = await _premierLeagueAccountRepository.Login(loginDto.Email, loginDto.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            if (user.Role != 1 && user.Role != 2)
            {
                return Unauthorized();
            }
            var result = _tokenRepository.GenerateAccessToken(user.EmailAddress, (int)user.Role);
            return Ok(result);
        }
    }
}
