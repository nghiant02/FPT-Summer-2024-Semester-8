using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.PaymentDTOs
{
    namespace EXE201.DAL.DTOs.PaymentDTOs
    {
        public class AddPaymentDTO
        {
            public string? FullName { get; set; }
            public string? Phone { get; set; }
            public string? Address { get; set; }
            public int PaymentMethodId { get; set; }
        }
    }
}
