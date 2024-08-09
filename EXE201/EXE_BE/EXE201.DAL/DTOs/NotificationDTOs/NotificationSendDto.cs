using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.NotificationDTOs
{
    public class NotificationSendDto
    {
        public int UserId { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime DateSent { get; set; }
        public string NotificationType { get; set; }
    }
}
