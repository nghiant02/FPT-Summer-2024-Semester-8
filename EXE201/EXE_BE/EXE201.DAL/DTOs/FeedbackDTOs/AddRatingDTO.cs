using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.FeedbackDTOs
{
    public class AddRatingDTO
    {
        //Rating
        public int UserId { get; set; }
        public int ProductId { get; set; }
        [Range(1, 5)]
        public int RatingValue { get; set; }
        public DateTime DateGiven { get; set; }

        //Feedback
        public string? FeedbackComment { get; set; }
        public string? FeedbackImage { get; set;}
    }
}
