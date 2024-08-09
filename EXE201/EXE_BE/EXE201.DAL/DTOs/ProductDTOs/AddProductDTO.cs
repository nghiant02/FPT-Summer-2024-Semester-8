using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.ProductDTOs
{
    public class AddProductDTO
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required!")]
        public IEnumerable<string> ProductImage { get; set; }

        [Required(ErrorMessage = "Price is required!")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Category is required!")]
        public int? CategoryId { get; set; }

        //public IEnumerable<ColorDetailDTO> ProductColors { get; set; }

        public IEnumerable<ExistingColorDetailDTO> ExistingColorIds { get; set; }

        //public IEnumerable<string> ProductSize { get; set; }

        public IEnumerable<int> ExistingSizeIds { get; set; }

        public ProductDetailDTOForAddProduct ProductDetail { get; set; } 
    }

    public class ColorDetailDTO
    {
        public string ColorName { get; set; }
        public string HexCode { get; set; }
        public string ColorImage { get; set; }
    }

    public class ExistingSizeDetailDTO
    {
        public int ColorId { get; set; }
        public string ColorImage { get; set; }
        public string ColorName { get; set; }
        public string HexCode { get; set; }
    }

    public class ExistingColorDetailDTO
    {
        public int ColorId { get; set; }
        public string ColorImage { get; set; }
    }

    public class ProductDetailDTOForAddProduct
    {
        public string Description { get; set; }
        public string AdditionalInformation { get; set; }
        public string ShippingAndReturns { get; set; }
        public string SizeChart { get; set; }
        public string Reviews { get; set; }
        public string Questions { get; set; }
        public string VendorInfo { get; set; }
        public string MoreProducts { get; set; }
        public string ProductPolicies { get; set; }
    }
}
