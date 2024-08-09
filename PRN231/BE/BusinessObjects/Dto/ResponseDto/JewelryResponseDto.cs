namespace BusinessObjects.Dto.ResponseDto;

public class JewelryResponseDto
{
    public string? JewelryId { get; set; }
    public string? Name { get; set; }
    public string? JewelryTypeId { get; set; }
    public string? Type { get; set; }
    public string? Barcode { get; set; }
    public double? LaborCost { get; set; }
    public float JewelryPrice { get; set; }
    public bool IsSold { get; set; }
    public string? ImageUrl { get; set; }
    public IList<Materials>? Materials { get; set; }
    public float TotalPrice { get; set; }
}