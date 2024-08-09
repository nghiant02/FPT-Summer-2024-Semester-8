namespace BusinessObjects.Dto.Jewelry;

public class JewelryRequestDto
{
    public required string JewelryTypeId { get; set; }
    
    public required string Name { get; set; }
    public string? ImageUrl { get; set; }
    
    public JewelryMaterialRequestDto? JewelryMaterial { get; set; }

    public string? Barcode { get; set; }
    
    public required double? LaborCost { get; set; }
}