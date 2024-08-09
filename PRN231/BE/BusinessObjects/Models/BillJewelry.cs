using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models;

public partial class BillJewelry
{
    [MaxLength(7)]
    public required string BillJewelryId { get; set; }
    [MaxLength(7)]
    public string? BillId { get; set; }
    [MaxLength(7)]
    public string? JewelryId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public virtual Bill? Bill { get; set; }

    public virtual Jewelry? Jewelry { get; set; }
}
