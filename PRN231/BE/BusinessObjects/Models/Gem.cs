using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Gem
{
    [MaxLength(7)]
    public required string GemId { get; set; }
    [MaxLength(255)]
    public string? Type { get; set; }
    [MaxLength(255)]
    public string? City { get; set; }
    public float BuyPrice { get; set; }
    public float SellPrice { get; set; }
    public DateTimeOffset LastUpdated { get; set; }
    
    
    public virtual ICollection<JewelryMaterial> JewelryMaterials { get; set; } = new List<JewelryMaterial>();
}