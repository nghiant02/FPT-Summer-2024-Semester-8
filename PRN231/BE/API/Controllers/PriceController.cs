using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController(IGoldPriceService goldPriceService, IGemPriceService gemPriceService) : ControllerBase
    {
        private IGoldPriceService GoldPriceService { get; } = goldPriceService;
        private IGemPriceService GemPriceService { get; } = gemPriceService;
        
        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet("GetGoldPrices")]
        public async Task<IActionResult> GetGoldPrices()
        {
            var goldPrices = await GoldPriceService.GetGoldPrices();
            return Ok(goldPrices);
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet("GetGemPrices")]
        public async Task<IActionResult> GetGemPrices()
        {
            var gemPrices = await GemPriceService.GetGemPrices();
            return Ok(gemPrices);
        }
    }
}