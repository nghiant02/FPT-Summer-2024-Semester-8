using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.PaymentDTOs
{
    public class ProcessPaymentDTO
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public bool Confirm { get; set; } // True if confirming the payment, otherwise just selecting the method
    }
}
