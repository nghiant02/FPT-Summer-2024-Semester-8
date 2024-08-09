using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.PaymentDTOs
{
    public class UpdatePaymentMethodNameDTO
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
    }
}
