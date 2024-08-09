using BusinessObjects.Dto;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController(IPromotionService promotionService) : ControllerBase
    {
        private IPromotionService PromotionService { get; } = promotionService;
        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet("GetPromotions")]
        public async Task<IActionResult> GetAllPromotion()
        {
            var result = await PromotionService.GetPromotions();
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Manager, Staff")]

        [HttpGet("GetPromotionById/{id}")]
        public async Task<IActionResult> GetPromotionById(string id)
        {
            var result = await PromotionService.GetPromotionById(id);
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Manager")]

        [HttpPost("AddNewPromotion")]
        public async Task<IActionResult> AddPromotion(string userId, PromotionDto promotionDto)
        {
            var result = await PromotionService.CreatePromotion(userId, promotionDto);
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("DeletePromotion")]
        public async Task<IActionResult> DeletePromotion(string id)
        {
            var result = await PromotionService.DeletePromotion(id);
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("UpdatePromotion")]
        public async Task<IActionResult> UpdatePromotion(string id, PromotionDto promotionDto)
        {
            var result = await PromotionService.UpdatePromotion(id, promotionDto);
            return Ok(result);
        }
    }
}
