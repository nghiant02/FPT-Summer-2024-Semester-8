using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.DashboardDTOs
{
    public class ProductStockDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalItemsInStock { get; set; }
    }

}
