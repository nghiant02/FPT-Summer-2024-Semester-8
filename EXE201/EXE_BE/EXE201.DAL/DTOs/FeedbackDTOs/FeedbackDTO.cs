using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.FeedbackDTOs
{
    public class FeedbackDTO
    {
        // RatingDTO
        public int UserId { get; set; }
        public int ProductId { get; set; }
        [Range(1, 5)]
        public int RatingValue { get; set; }

        // FeedbackDTO
        public string FeedbackCommment { get; set; }
        public string FeedbackImage { get; set; }
        public string FeedbackStatus { get; set; }

    }
}
