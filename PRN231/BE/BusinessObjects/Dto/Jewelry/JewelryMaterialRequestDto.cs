namespace BusinessObjects.Dto.Jewelry;

public class JewelryMaterialRequestDto
{
    public  string? GemId { get; set; }
    public  string? GoldId { get; set; }
    
    public  float GoldQuantity { get; set; }
    public float GemQuantity { get; set; }
}