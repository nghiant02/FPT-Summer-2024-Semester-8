using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs.NotificationDTOs;
using Microsoft.AspNetCore.Mvc;

namespace EXE201.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly INotificationServices _notificationService;

        public NotificationController(INotificationServices notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("GetNotificationByUserId")]
        public async Task<IActionResult> GetNotificationsByUserIdAsync(int userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(notifications);
        }

        [HttpPost("SendNotification")]
        public async Task<IActionResult> SendNotificationAsync([FromBody] NotificationSendDto notificationSendDto)
        {
            if (notificationSendDto == null)
            {
                return BadRequest("Notification data is null.");
            }

            await _notificationService.SendNotificationAsync(notificationSendDto);
            return Ok("Notification sent successfully.");
        }

        [HttpPut("MarkAsSeen")]
        public async Task<IActionResult> MarkAsSeenAsync(int notificationId)
        {
            await _notificationService.MarkNotificationAsSeenAsync(notificationId);
            return Ok("Notification marked as seen.");
        }
    }
}
