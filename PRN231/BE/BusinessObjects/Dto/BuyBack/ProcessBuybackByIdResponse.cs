using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.BuyBack
{
    public class ProcessBuybackByIdResponse
    {
        public float TotalPrice { get; set; }
        public string BillId { get; set; }
    }

    public class CountProcessBuybackByIdResponse
    {
        public float TotalPrice { get; set; }
    }

    public class ProcessBuybackByNameResponse
    {
        public float TotalPrice { get; set; }
        public string BillId { get; set; }
    }

    public class CountProcessBuybackByNameResponse
    {
        public float TotalPrice { get; set; }
    }

}
