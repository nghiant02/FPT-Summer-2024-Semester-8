using EXE201.BLL.Interfaces;
using EXE201.BLL.Services;
using EXE201.DAL.DTOs.EmailDTOs;
using EXE201.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tools.Tools;

namespace EXE201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IEmailService _emailService;
        private readonly IForgotPawwordService _forgotPawwordService;
        public ForgotPasswordController(IUserServices userService, IForgotPawwordService forgotPasswordService, IEmailService emailService)
        {
            _userServices = userService;
            _forgotPawwordService = forgotPasswordService;
            _emailService = emailService;

        }
        [HttpGet("Verify-Code")]
        public async Task<IActionResult> VerifyCode([FromQuery] string code, [FromQuery] string email)
        {
            User user = await _forgotPawwordService.VerifyCode(code, email);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);

        }
        [HttpPost("Send-Code")]
        public async Task<IActionResult> CheckEmailAndSendCode([FromBody] string email)
        {
            var user = await _userServices.FindUserByEmail(email);
            if (user != null)
            {
                if (await _forgotPawwordService.IsCodeExist(email))
                {
                    await _forgotPawwordService.Delete(email);
                }
                string code = IdGenerator.GenerateRandomVerifyCode();
                await _forgotPawwordService.AddCode(code, email);
                EmailDTO emailDto = new EmailDTO();
                emailDto.ToEmail = email;
                emailDto.Subject = "Verification Code";
                emailDto.Body = GetHtmlcontent(code);
                await _emailService.SendEmail(emailDto);
                var result = new Dictionary<string, string>() {
                    {"email",email }
                };
                return Ok(result);
            }
            else
            {
                return BadRequest("User not found!");
            }

        }

        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword(string email, string password, int userID)
        {
            var user = await _userServices.UpdatePassword(email, password, userID);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("Cannot reset password!");
            }
        }

        private string GetHtmlcontent(string code)
        {
            string Response = " <div class=\"container\" style=\"text-align: center\">\r\n "
                + "<img\r\n src=\"https://cdn-icons-png.flaticon.com/512/3617/3617039.png\"\r\n alt=\"image\"\r\n class=\"image\"\r\n style=\"width: 160px; height: 160px;\"\r\n/>"
                + "\r\n<div class=\"h4\" style=\"padding-top: 16px; font-size: 18px;\">Hi</div>\r\n"
                + "<div style=\"padding-top: 16px; font-size: 20px;\">Here is the confirmation code for your online form:</div>\r\n"
                + " <div class=\"code\" style=\"padding-top: 16px; font-size: 50px; font-weight: bold; color: #f57f0e;\"> " + code + " </div>\r\n"
                + "<div style=\"padding-top: 16px; font-size: 18px;\">\r\nAll you have to do is copy the confirmation code and paste it to your\r\n form to complete the email verification process.\r\n</div>\r\n</div>";
            return Response;
        }
    }
}
