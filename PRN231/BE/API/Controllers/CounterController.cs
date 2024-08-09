using BusinessObjects.DTO;
using BusinessObjects.Dto.Counter;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CounterController(ICounterService counterService) : ControllerBase
{
    private ICounterService CounterService { get; } = counterService;

    [Authorize(Roles = "Admin, Manager, Staff")]
    [HttpGet("GetCounterById/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var counter = await CounterService.GetCounterById(id);
        if (counter == null) return NotFound();
        return Ok(counter);
    }

    [Authorize(Roles = "Admin, Manager, Staff")]
    [HttpGet("GetCounters")]
    public async Task<IActionResult> GetCounters()
    {
        var counter = await CounterService.GetCounters();
        if (counter == null) return NotFound();
        return Ok(counter);
    }

    //[Authorize(Roles = "Admin, Manager, Staff")]
    [HttpGet("GetAvailableCounters")]
    public async Task<IActionResult> GetAvailableCounters()
    {
        var counter = await CounterService.GetAvailableCounters();
        return Ok(counter);
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpPost("CreateCounter")]
    public async Task<IActionResult> Create(CounterDto counter)
    {
        var newCounter = await CounterService.CreateCounter(counter);
        return Ok(newCounter);
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpPost("CreateCounterMongo")]
    public async Task<IActionResult> CreateCounter(CounterStatus counter)
    {
        await CounterService.CreateCounterMongo(counter);
        return Ok();
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpPut("UpdateCounter")]
    public async Task<IActionResult> UpdateCounter(string id, UpdateCounter counter)
    {
        var updateCounter = await CounterService.UpdateCounter(id, counter);
        return Ok(updateCounter);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteCounter")]
    public async Task<IActionResult> DeleteCounter(string id)
    {
        var counter = await CounterService.DeleteCounter(id);
        return Ok(counter);
    }
}