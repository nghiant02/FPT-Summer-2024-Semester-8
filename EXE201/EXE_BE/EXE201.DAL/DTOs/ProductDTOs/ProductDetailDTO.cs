using EXE201.DAL.DTOs.FeedbackDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXE201.DAL.Models;

namespace EXE201.DAL.DTOs.ProductDTOs
{
    public class ProductDetailDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public IEnumerable<int> ProductImageId { get; set; }
        public IEnumerable<string> ProductImage { get; set; }
        public double? ProductPrice { get; set; }
        public IEnumerable<int> ProductSizeId { get; set; }
        public IEnumerable<string> ProductSize { get; set; }

        public IEnumerable<int> ProductColorId { get; set; }
        public IEnumerable<string> ProductColor { get; set; }
        public IEnumerable<string> ProductColorImage { get; set; }
        public string ProductStatus { get; set; }
        public string CategoryName { get; set; }
        public double AverageRating { get; set; }
        public List<RatingFeedbackDTO> RatingsFeedback { get; set; }
    }
}
