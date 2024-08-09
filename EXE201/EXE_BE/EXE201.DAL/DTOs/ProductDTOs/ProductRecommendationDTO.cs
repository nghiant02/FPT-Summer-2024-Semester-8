using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.ProductDTOs
{
    public class ProductRecommendationDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public IEnumerable<string> ProductImage { get; set; }
        public double? ProductPrice { get; set; }
        public IEnumerable<string> ProductSize { get; set; }
        public IEnumerable<string> ProductColor { get; set; }
        public string ProductStatus { get; set; }
        public string CategoryName { get; set; }
        public double AverageRating { get; set; }
        public int NumberOfPurchases { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
