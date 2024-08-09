using BussiniseObject;
using BussiniseObject.Dto;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepository repository) : ControllerBase
{
    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await repository.GetUsers();
        return Ok(users);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await repository.CreateToken(loginDto.Email, loginDto.Password);
        if (string.IsNullOrEmpty(result))
        {
            return Unauthorized();
        }
        return Ok(result);
    }
}