using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public partial class Gold
{
    [MaxLength(7)]
    public required string GoldId { get; set; }
    [MaxLength(255)]
    public string? Type { get; set; }
    [MaxLength(255)]
    public string? City { get; set; }
    public float BuyPrice { get; set; }
    public float SellPrice { get; set; }
    public DateTimeOffset? LastUpdated { get; set; }
    public virtual IList<JewelryMaterial> JewelryMaterials { get; set; } = new List<JewelryMaterial>();
}
