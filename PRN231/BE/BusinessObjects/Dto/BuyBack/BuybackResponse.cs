using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.BuyBack
{
    public class BuybackResponse
    {
        public string Message { get; set; }
        public int IsBuyBack { get; set; }
        public double PurchasePrice { get; set; }
    }
}
