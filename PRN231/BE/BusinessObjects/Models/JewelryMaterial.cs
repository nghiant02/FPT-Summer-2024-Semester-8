using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class JewelryMaterial
{
    [MaxLength(7)]
    public required string JewelryMaterialId { get; set; }
    [MaxLength(7)]
    public string? JewelryId { get; set; }
    [MaxLength(255)]
    
    public string? GoldPriceId { get; set; }
    [MaxLength(255)]
    
    public string? StonePriceId { get; set; }
    
    public float GoldQuantity { get; set; }
    
    public float StoneQuantity { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public virtual Gem? StonePrice { get; set; }
    public virtual Gold? GoldPrice { get; set; }
    public virtual Jewelry? Jewelry { get; set; }
}