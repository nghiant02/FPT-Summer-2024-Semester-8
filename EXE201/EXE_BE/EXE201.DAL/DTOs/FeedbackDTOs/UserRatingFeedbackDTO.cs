using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.FeedbackDTOs
{
    public class UserRatingFeedbackDTO
    {
        public int RatingId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int RatingValue { get; set; }
        public DateTime? DateGiven { get; set; }
        public string FeedbackComment { get; set; }
        public string FeedbackImage { get; set; }
    }

}
