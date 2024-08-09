using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public partial class Warranty
{
    [MaxLength(7)]
    public required string WarrantyId { get; set; }
    [MaxLength(7)]
    public string? JewelryId { get; set; }
    [MaxLength(255)]
    public string? Description { get; set; }

    public DateTimeOffset? EndDate { get; set; }
    public Jewelry? Jewelry { get; set; }
}
