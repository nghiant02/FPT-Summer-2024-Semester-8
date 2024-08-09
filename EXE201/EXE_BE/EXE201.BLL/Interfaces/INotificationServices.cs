using EXE201.DAL.DTOs.NotificationDTOs;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Interfaces
{
    public interface INotificationServices
    {
        Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(int userId);
        Task SendNotificationAsync(NotificationSendDto notificationSendDto);
        Task MarkNotificationAsSeenAsync(int notificationId);
    }
}
