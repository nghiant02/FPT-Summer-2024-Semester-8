namespace BusinessObjects.Dto.BillReqRes;

public class BillCashCheckoutResponseDto
{
    public required string BillId { get; set; }
    public required string CustomerName { get; set; }
    public float InitialAmount { get; set; }
    public float CashBack { get; set; }
    
    public float FinalAmount { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? Status { get; set; }
}