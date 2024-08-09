using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.RentalOrderDTOs
{
    public class RentalOrderResponseDTO
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? DatePlaced { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public decimal? OrderTotal { get; set; }
        public int? PointsEarned { get; set; }
        public string? ProductImage { get; set; }
        public string? OrderCode { get; set; }
    }
}
