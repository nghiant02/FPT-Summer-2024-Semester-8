namespace BusinessObjects.Dto.ResponseDto;

public class GemPriceResponseDto
{
    public required string GemId { get; set; }
    public string? Type { get; set; }
    public string? City { get; set; }
    public float BuyPrice { get; set; }
    public float SellPrice { get; set; }
    public DateTimeOffset? LastUpdated { get; set; }
}