using EXE201.DAL.DTOs.EmailDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(EmailDTO request);
        Task<bool> SendEmailAsync(EmailDTO emailDto);
    }
}
