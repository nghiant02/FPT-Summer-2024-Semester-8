using BusinessObjects.Dto;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    private ICustomerService CustomerService { get; } = customerService;
    [Authorize(Roles = "Admin, Manager")]
    [HttpGet("GetCustomers")]
    public async Task<IActionResult> GetCustomersPaging(int pageNumber, int pageSize)
    {
        return Ok(await CustomerService.GetCustomersPaging(pageNumber, pageSize));
    }
    [HttpGet("GetCustomerById/{id}")]
    public async Task<IActionResult> GetCustomerById(string id)
    {
        return Ok(await CustomerService.GetCustomerById(id));
    }
    [HttpGet("GetCustomerByPhone/{phoneNumber}")]
    public async Task<IActionResult> GetCustomerByPhone(string phoneNumber)
    {
        var result = await CustomerService.GetCustomerByPhone(phoneNumber);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    [HttpPost("CreateCustomer")]
    public async Task<IActionResult> CreateCustomer(CustomerDto customer)
    {
        var result = await CustomerService.CreateCustomer(customer);
        return Ok(result);
    }
    [HttpPut("UpdateCustomer/{id}")]
    public async Task<IActionResult> UpdateCustomer(string id, CustomerDto customer)
    {
        var result = await CustomerService.UpdateCustomer(id ,customer);
        return Ok(result);
    }
    [HttpDelete("DeleteCustomer/{id}")]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        var result = await CustomerService.DeleteCustomer(id);
        return Ok(result);
    }
}
