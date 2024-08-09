using AutoMapper;
using BusinessObjects.Dto;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarrantyController(IWarrantyService warrantyService, IMapper mapper) : ControllerBase
{
    private IWarrantyService WarrantyService { get; } = warrantyService;
    public IMapper Mapper { get; } = mapper;
    [Authorize(Roles = "Admin, Manager, Staff")]
    [HttpGet("GetWarranties")]
    public async Task<IActionResult> Get()
    {
        var warranties = await WarrantyService.GetWarranties();
        return Ok(warranties);
    }
    [Authorize(Roles = "Admin, Manager, Staff")]

    [HttpGet("GetWarrantyById/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var warranty = await WarrantyService.GetWarrantyById(id);
        return Ok(warranty);
    }
    [Authorize(Roles = "Admin, Manager")]
    [HttpPost("CreateWarranty")]
    public async Task<IActionResult> CreateWarranty(WarrantyDto warrantyDto)
    {
        var result = await WarrantyService.CreateWarranty(warrantyDto);
        return Ok(result);
    }
    [Authorize(Roles = "Admin, Manager")]
    [HttpPut("UpdateWarranty/{id}")]
    public async Task<IActionResult> UpdateWarranty(string id,WarrantyDto warrantyDto)
    {
        var result = await WarrantyService.UpdateWarranty(id ,warrantyDto);
        return Ok(result);
    }
}
