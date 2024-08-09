using AutoMapper;
using BusinessObjects.Dto.Jewelry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JewelryController(IJewelryService jewelryService, IMapper mapper) : ControllerBase
{
    private IJewelryService JewelryService { get; } = jewelryService;
    private IMapper Mapper { get; } = mapper;
    [HttpGet("GetJewelries")]
    public async Task<IActionResult> GetJewelries(int pageNumber, int pageSize, string? name, string? typeId)
    {
        var jewelries = await JewelryService.GetJewelries(pageNumber, pageSize, name, typeId);
        return Ok(jewelries);
    }
    //[Authorize(Roles = "Admin, Manager, Staff")]

    [HttpGet("GetJewelriesByType")]
    private async Task<IActionResult> GetJewelriesByType(string? jewelryTypeId, int pageNumber, int pageSize)
    {
        if (jewelryTypeId == null)
        {
            return await GetJewelries(pageNumber, pageSize, "", "");
        }
        var jewelries = await JewelryService.GetJewelryByType(jewelryTypeId, pageNumber, pageSize);
        return Ok(jewelries);
    }
    //[Authorize(Roles = "Admin, Manager, Staff")]

    [HttpGet("GetJewelryById/{id}")]
    public async Task<IActionResult> GetJewelryById(string id)
    {
        var jewelry = await JewelryService.GetJewelryById(id);
        if (jewelry == null) return NotFound();
        return Ok(jewelry);
    }
    [Authorize(Roles = "Admin, Manager")]
    [HttpPost("CreateJewelry")]
    public async Task<IActionResult> CreateJewelry(JewelryRequestDto jewelryRequestDto)
    {
        var result = await JewelryService.CreateJewelry(jewelryRequestDto);
        return Ok(result);
    }
    [Authorize(Roles = "Admin, Manager")]
    [HttpPut("UpdateJewelry/{id}")]
    public async Task<IActionResult> UpdateJewelry(string id, JewelryRequestDto jewelryRequestDto)
    {
        var result = await JewelryService.UpdateJewelryWithMaterial(id, jewelryRequestDto);
        return Ok(result);
    }
    [Authorize(Roles = "Admin, Manager")]
    [HttpDelete("DeleteJewelry/{id}")]
    public async Task<IActionResult> DeleteJewelry(string id)
    {
        var result = await JewelryService.DisableJewelry(id);
        if (!result) return NotFound();
        return Ok("Jewelry has been disabled");
    }
}