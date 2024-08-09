using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.RentalOrderDTOs
{
    public class RentalOrderDetailResponseDTO
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public DateTime? RentalStart { get; set; }
        public DateTime? RentalEnd { get; set; }
        public DateTime? PaymentTime { get; set; }
        public string Status { get; set; }
        public string OrderCode { get; set; }
    }
}
