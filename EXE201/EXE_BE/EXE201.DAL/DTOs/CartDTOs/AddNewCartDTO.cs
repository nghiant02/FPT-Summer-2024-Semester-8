using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace EXE201.DAL.DTOs.CartDTOs
{
    public class AddNewCartDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ProductColorId { get; set; }
        public int ProductImageId { get; set; }
        public int ProductSizeId { get; set; }
        public DateTime? RentalStart { get; set; }
        public DateTime? RentalEnd { get; set; }
        public int Quantity {  get; set; }
    }
}
