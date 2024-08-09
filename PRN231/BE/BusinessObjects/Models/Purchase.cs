using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models;

public partial class Purchase
{
    [MaxLength(7)]
    public required string PurchaseId { get; set; }
    [MaxLength(7)]

    public string? CustomerId { get; set; }
    [MaxLength(7)]

    public string? UserId { get; set; }
    [MaxLength(7)]

    public string? JewelryId { get; set; }
    [MaxLength(7)]
    public string? BillId { get; set; }

    public DateTimeOffset? PurchaseDate { get; set; }

    public double? PurchasePrice { get; set; }

    public int? IsBuyBack { get; set; }
    public virtual Customer? Customer { get; set; }
    public virtual Jewelry? Jewelry { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
}
