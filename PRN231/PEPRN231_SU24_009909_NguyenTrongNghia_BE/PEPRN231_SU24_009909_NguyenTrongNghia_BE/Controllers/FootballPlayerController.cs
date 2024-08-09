using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace PEPRN231_SU24_009909_NguyenTrongNghia_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballPlayerController : Controller
    {
        private readonly IFootballPlayerRepository _footballPlayerRepository;

        public FootballPlayerController(IFootballPlayerRepository footballPlayerRepository)
        {
            _footballPlayerRepository = footballPlayerRepository;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _footballPlayerRepository.GetFootballPlayer();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _footballPlayerRepository.GetFootballPlayerId(id);
            return Ok(result);
        }
    }
}
