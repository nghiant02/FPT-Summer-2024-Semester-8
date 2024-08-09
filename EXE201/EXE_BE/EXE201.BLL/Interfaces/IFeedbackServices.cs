using EXE201.DAL.DTOs.FeedbackDTOs;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Interfaces
{
    public interface IFeedbackServices
    {
        Task<IEnumerable<Feedback>> GetFeedbacks();
        Task<Feedback> AddFeedback(FeedbackDTO feedback);
    }
}
