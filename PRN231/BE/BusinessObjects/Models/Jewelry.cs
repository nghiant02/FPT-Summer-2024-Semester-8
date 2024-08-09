using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models;

public partial class Jewelry
{
    [MaxLength(7)]
    public required string JewelryId { get; set; }
    [MaxLength(7)]
    public string? JewelryTypeId { get; set; }
    [MaxLength(255)]
    public string? Name { get; set; }
    [MaxLength(255)]
    public string? Barcode { get; set; }
    [MaxLength(255)]
    public string? ImageUrl { get; set; }
    public double? LaborCost { get; set; }
    public bool? IsSold { get; set; }
    public bool? IsEnable { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public virtual ICollection<BillJewelry> BillJewelries { get; set; } = new List<BillJewelry>();
    public virtual JewelryType? JewelryType { get; set; }
    public virtual Warranty? Warranty { get; set; }
    public virtual IList<JewelryMaterial> JewelryMaterials { get; set; } = new List<JewelryMaterial>();
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}