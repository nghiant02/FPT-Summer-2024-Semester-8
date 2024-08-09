using AutoMapper;
using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs.NotificationDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Services
{
    public class NotificationServices : INotificationServices
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationServices(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(int userId)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }

        public async Task SendNotificationAsync(NotificationSendDto notificationSendDto)
        {
            var notification = _mapper.Map<Notification>(notificationSendDto);
            notification.DateSent = DateTime.UtcNow;
            notification.Seen = false;
            await _notificationRepository.AddNotificationAsync(notification);
        }

        public async Task MarkNotificationAsSeenAsync(int notificationId)
        {
            await _notificationRepository.MarkAsSeenAsync(notificationId);
        }
    }
}
