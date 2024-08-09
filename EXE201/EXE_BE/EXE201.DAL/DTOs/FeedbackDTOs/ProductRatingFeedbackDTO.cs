using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.FeedbackDTOs
{
    public class ProductRatingFeedbackDTO
    {
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RatingValue { get; set; }
        public DateTime? DateGiven { get; set; }
        public string FeedbackComment { get; set; }
        public string FeedbackImage { get; set; }
    }

    public class ProductRatingFeedbackResponseDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public List<ProductRatingFeedbackDTO> Ratings { get; set; }
    }
}
