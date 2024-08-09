using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public partial class Promotion
{
    [MaxLength(7)]
    public required string PromotionId { get; set; }
    [MaxLength(255)]

    public string? Type { get; set; }
    [MaxLength(255)]

    public string? ApproveManager { get; set; }
    [MaxLength(255)]

    public string? Description { get; set; }

    public double? DiscountRate { get; set; }

    public DateTimeOffset? StartDate { get; set; }

    public DateTimeOffset? EndDate { get; set; }

    public virtual ICollection<BillPromotion> BillPromotions { get; set; } = new List<BillPromotion>();
}
