namespace BusinessObjects.Dto.BillReqRes
{
    public class BillRequestDto
    {
        public string? CustomerId { get; set; }
        public string? UserId { get; set; }
        public string? CounterId { get; set; }
        public double AdditionalDiscount { get; set; }
        public IEnumerable<BillItemRequestDto?>? Jewelries { get; set; }
        public IEnumerable<BillPromotionRequestDto?>? Promotions { get; set; }
    }
}