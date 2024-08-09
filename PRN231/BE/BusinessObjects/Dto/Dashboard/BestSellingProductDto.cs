using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.Dashboard
{
    public class BestSellingProductDto
    {
        public string JewelryId { get; set; }
        public string JewelryName { get; set; }
        public int PurchaseTime
        { get; set; }
    }
}
