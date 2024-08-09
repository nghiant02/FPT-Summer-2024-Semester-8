using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.OrderDTOs
{
    public class RentalOrderDto
    {
        public string OrderCode { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime PaymentTime { get; set; }
        public string OrderStatus { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int ProductQuantity { get; set; }
        public DateTime RentalStart { get; set; }
        public DateTime RentalEnd { get; set; }
    }
}

