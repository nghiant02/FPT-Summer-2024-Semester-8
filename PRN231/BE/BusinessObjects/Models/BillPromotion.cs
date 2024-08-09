using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public partial class BillPromotion
{
    [MaxLength(7)]
    public required string BillPromotionId { get; set; }
    [MaxLength(7)]
    public string? BillId { get; set; }
    [MaxLength(7)]
    public string? PromotionId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public virtual Bill? Bill { get; set; }

    public virtual Promotion? Promotion { get; set; }
}
