using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs.UserDTOs;
using EXE201.ViewModel.UserViewModel;
using Microsoft.AspNetCore.Mvc;
using EXE201.DAL.DTOs.EmailDTOs;
using System.Text;
using EXE201.BLL.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using EXE201.DAL.Models;
using System.Security.Claims;
using EXE201.DAL.DTOs.TokenDTOs;

namespace EXE201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;

        public UserController(IUserServices userServices, IEmailService emailService, IJwtService jwtService)
        {
            _userServices = userServices;
            _emailService = emailService;
            _jwtService = jwtService;
        }

        [HttpGet("GetAllProfileUsers")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _userServices.GetAllProfileUser();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDTOs loginUserDTOs)
        {
            try
            {
                var result = await _userServices.Login(loginUserDTOs.Username, loginUserDTOs.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, Message = ex.Message });
            }
        }
        
        [HttpPost("GoogleLogin")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleUserDto googleUserDto)
        {
            try
            {
                var result = await _userServices.GoogleAuthorizeUser(googleUserDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTOs refreshTokenDTOs)
        {
            try
            {
                var (newToken, newRefreshToken) = await _userServices.RefreshTokenAsync(refreshTokenDTOs.Token, refreshTokenDTOs.RefreshToken);
                return Ok(new
                {
                    Token = newToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            try
            {
                var result = await _userServices.RegisterUserAsync(request);
                if (result.Success)
                {
                    return Ok(new { Message = "Registration successful, please check your email to verify your account.", UserId = result.UserId });
                }
        
                return BadRequest("An error occurred while registering the user.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("VerifyCode")]
        public async Task<IActionResult> VerifyEmailWithCode(int userId, string code)
        {
            try
            {
                var result = await _userServices.VerifyEmailWithCodeAsync(userId, code);
                if (result)
                {
                    return Ok(new { Message = "Email verified successfully." });
                }

                return BadRequest("Email verification failed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddNewUserForStaff")]
        public async Task<IActionResult> AddNewUser(AddNewUserDTO addNewUserDTO)
        {
            try
            {
                var result = await _userServices.AddUserForStaff(addNewUserDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("BlockUser")]
        public async Task<IActionResult> BlockUser(int userId)
        {
            try
            {
                var result = await _userServices.ChangeStatusUserToNotActive(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> UpdatePassword(
            int userId, ChangePasswordDTO changePassword)
        {
            var user = await _userServices.ChangePasword(userId, changePassword);
            return Ok(user);
        }

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateUser([FromQuery] [Required] int userId, UpdateProfileUserDTO userView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userServices.UserUpdateUser(userId, userView);
            return Ok(user);
        }

        [HttpPut("UpdateAvatar")]
        public async Task<IActionResult> UpdateAvatar([FromQuery][Required] int userId, UpdateAvatarUserDTO updateAvatarUserDTO)
        {
            var result = await _userServices.UserUpdateAvartar(userId, updateAvatarUserDTO);
            return Ok(result);
        }

        [HttpGet("GetFilteredUser")]
        public async Task<IActionResult> GetFilteredUser(
            [FromQuery] UserFilterDTO filter)
        {
            var users = await _userServices.GetFilteredUser(filter);
            return Ok(users);
        }

        [HttpGet("ViewProfile/{userId}")]
        public async Task<IActionResult> ViewProfile(int userId)
        {
            var userProfile = await _userServices.GetUserProfile(userId);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok(userProfile);
        }
    }
}
