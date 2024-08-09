using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models;

public partial class Bill
{
    [MaxLength(7)]
    public required string BillId { get; set; }
    
    [MaxLength(7)]
    public required string CustomerId { get; set; }
    [MaxLength(7)]
    public required string UserId { get; set; }
    [MaxLength(7)]
    public required string CounterId { get; set; }

    public double? TotalAmount { get; set; }
    
    public bool IsPaid { get; set; }

    public DateTimeOffset SaleDate { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    // Navigation properties
    public virtual ICollection<BillJewelry> BillJewelries { get; set; } = new List<BillJewelry>();
    public virtual ICollection<BillPromotion> BillPromotions { get; set; } = new List<BillPromotion>();

    public virtual Customer? Customer { get; set; }

    public virtual User? User { get; set; }
    
    public virtual Counter? Counter { get; set; }
    
    public virtual Payment? Payment { get; set; }
}
