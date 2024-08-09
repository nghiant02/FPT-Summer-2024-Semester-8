using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Tools;

namespace BusinessObjects.Models;

public partial class JewelryType
{
    [MaxLength(7)]
    public required string JewelryTypeId { get; set; }
    [MaxLength(255)]
    public string? Name { get; set; }
    public virtual ICollection<Jewelry> Jewelries { get; set; } = new List<Jewelry>();
}
