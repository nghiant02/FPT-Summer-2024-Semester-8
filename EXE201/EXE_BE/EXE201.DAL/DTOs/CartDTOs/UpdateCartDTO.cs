using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.CartDTOs
{
    public class UpdateCartDto
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime? RentalStart { get; set; }
        public DateTime? RentalEnd { get; set; }
    }
}
