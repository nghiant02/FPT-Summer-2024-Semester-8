using BusinessObject.Dto.RequestDto;
using BusinessObject.Dto.ResponseDto.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserRepository? _userRepository;

    public AuthController(IUserRepository? userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (loginDto == null || _userRepository == null) return BadRequest("Invalid login request");
        var data = await _userRepository.Login(loginDto);
        if(data == null) return Unauthorized();
        var response = data.Data as UserResponseDto;
        return Ok(response);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (registerDto == null || _userRepository == null) return BadRequest("Invalid register request");
        var user = await _userRepository.Register(registerDto);
        if(user == null) return BadRequest("Failed to register user");
        return Ok(user);
    }
}
