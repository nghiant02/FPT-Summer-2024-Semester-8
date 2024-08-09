using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.PaymentDTOs
{
    public class PaymentResponseDTO
    {
        public int PaymentId { get; set; }
        public int? OrderId { get; set; }
        public int UserId { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentStatus { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime PaymentTime { get; set; }
        public string PaymentLink { get; set; }
        public List<CartResponseDTO> Carts { get; set; }
    }

    public class CartResponseDTO
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductResponseDTO Product { get; set; }
    }

    public class ProductResponseDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int CategoryId { get; set; }
    }
}
