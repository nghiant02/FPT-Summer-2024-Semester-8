using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs.SizeDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SizeController : ControllerBase
{
    private readonly ISizeServices _sizeServices;

    public SizeController(ISizeServices sizeServices)
    {
        _sizeServices = sizeServices;
    }

    // View all sizes
    [HttpGet("GetAllSizes")]
    public async Task<IActionResult> GetAllSizes()
    {
        var sizes = await _sizeServices.GetAllSizes();
        return Ok(sizes);
    }

    // Create a new size
    [HttpPost("CreateSize")]
    public async Task<IActionResult> CreateSize([FromBody] CreateSizeDTO request)
    {
        var response = await _sizeServices.CreateSize(request.SizeName);
        if (response.Status == "Error")
        {
            return BadRequest(new { Message = response.Message });
        }
        return Ok(response.DataObject);
    }

    // Delete a size by ID
    [HttpDelete("DeleteSize")]
    public async Task<IActionResult> DeleteSize(int sizeId)
    {
        var response = await _sizeServices.DeleteSize(sizeId);
        if (response.Status == "Error")
        {
            return BadRequest(new { Message = response.Message });
        }
        return Ok(new { Message = response.Message });
    }

    // Update a size by ID
    [HttpPut("UpdateSize")]
    public async Task<IActionResult> UpdateSize([FromBody] UpdateSizeDTO request)
    {
        var response = await _sizeServices.UpdateSize(request.SizeId, request.NewSizeName);
        if (response.Status == "Error")
        {
            return BadRequest(new { Message = response.Message });
        }
        return Ok(response.DataObject);
    }
}
