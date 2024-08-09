using System;

namespace BusinessObject.DTO
{
    public class SalesReportDTO
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
