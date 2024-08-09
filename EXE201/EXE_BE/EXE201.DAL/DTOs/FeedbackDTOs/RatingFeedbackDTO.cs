using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.FeedbackDTOs
{
    public class RatingFeedbackDTO
    {
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RatingValue { get; set; }
        public DateTime? DateGiven { get; set; }
        public string FeedbackComment { get; set; }
        public string FeedbackImage { get; set; }
    }
}
