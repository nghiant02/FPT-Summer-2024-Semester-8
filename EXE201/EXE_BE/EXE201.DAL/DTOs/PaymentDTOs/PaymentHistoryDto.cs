using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.PaymentDTOs
{
    public class PaymentHistoryDto
    {
        public int PaymentId { get; set; }
        public DateTime? PaymentTime { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string PaymentContent { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentStatus { get; set; }
    }
}
