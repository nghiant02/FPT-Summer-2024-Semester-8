using EXE201.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EXE201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {
        private readonly IInventoryServices _inventoryService;

        public InventoryController(IInventoryServices inventoryServices)
        {
            _inventoryService = inventoryServices;
        }

        [HttpGet("GetInventories")]
        public async Task<IActionResult> Get(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _inventoryService.GetInventories( pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteInventory")]
        public async Task<IActionResult> Delete(int inventoryId)
        {
            var result = await _inventoryService.DeleteInventory(inventoryId);
            return Ok(result);
        }
    }
}