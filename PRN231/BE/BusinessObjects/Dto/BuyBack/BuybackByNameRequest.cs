using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.BuyBack
{
    public class BuybackByNameRequest
    {
        public string JewelryName { get; set; }
        public string JewelryTypeId { get; set; }
        public string ImageUrl { get; set; }
        public JewelryMaterialDto JewelryMaterial { get; set; }
        public string UserId { get; set; } 
        public BuybackCustomerDto Customer { get; set; }
        public double LaborCost { get; set; }
        public bool HasGuarantee { get; set; }
    }

    public class CountBuybackByNameRequest
    {
        public string JewelryName { get; set; }
        public string JewelryTypeId { get; set; }
        public JewelryMaterialDto JewelryMaterial { get; set; }
        public double LaborCost { get; set; }
        public bool HasGuarantee { get; set; }
    }
}
