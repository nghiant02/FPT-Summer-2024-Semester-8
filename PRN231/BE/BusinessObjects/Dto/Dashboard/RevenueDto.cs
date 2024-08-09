namespace BusinessObjects.Dto.Dashboard
{
    public class RevenueDto
    {
        public decimal TotalRevenue { get; set; }

        public static implicit operator decimal(RevenueDto v)
        {
            throw new NotImplementedException();
        }
    }
}
