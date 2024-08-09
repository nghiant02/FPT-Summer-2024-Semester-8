using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.ProductDTOs
{
    public class ProductDetailViewDTO
    {
        public string[] Description { get; set; }
        public string[] AdditionalInformation { get; set; }
        public string[] ShippingAndReturns { get; set; }
        public string[] SizeChart { get; set; }
        public string[] Reviews { get; set; }
        public string[] Questions { get; set; }
        public string[] VendorInfo { get; set; }
        public string[] MoreProducts { get; set; }
        public string[] ProductPolicies { get; set; }
    }
}
