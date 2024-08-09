using BusinessObjects.Dto.BuyBack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;
using Services.Interface;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpPost("BuyBackById")]
        public async Task<IActionResult> BuybackById([FromBody] BuybackByIdRequest request)
        {
            try
            {
                var result = await _purchaseService.ProcessBuybackById(request.JewelryId);
                return Ok(new { Message = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = -1, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpPost("CountBuyBackById")]
        public async Task<IActionResult> CountBuyBackById([FromBody] BuybackByIdRequest request)
        {
            try
            {
                var result = await _purchaseService.CountProcessBuybackById(request.JewelryId);
                return Ok(new { Message = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = -1, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpPost("BuyBackByName")]
        public async Task<IActionResult> BuybackByName([FromBody] BuybackByNameRequest request)
        {
            try
            {
                var result = await _purchaseService.ProcessBuybackByName(request);
                return Ok(new { Message = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = -1, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpPost("CountBuyBackByName")]
        public async Task<IActionResult> CountBuybackByName([FromBody] CountBuybackByNameRequest request)
        {
            try
            {
                var result = await _purchaseService.CountProcessBuybackByName(request);
                return Ok(new { Message = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = -1, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
