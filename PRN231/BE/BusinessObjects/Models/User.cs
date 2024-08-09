using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public partial class User
{
    [MaxLength(7)]
    public required string UserId { get; set; }
    [MaxLength(7)]
    public string? RoleId { get; set; }
    [MaxLength(7)]
    public string? CounterId { get; set; }
    [MaxLength(255)]
    public string? Username { get; set; }
    [MaxLength(255)]
    
    public string? FullName { get; set; }
    [MaxLength(255)]
    
    public string? Gender { get; set; }
    [MaxLength(255)]
    
    public string? PhoneNumber { get; set; }
    [MaxLength(255)]
    public string? Email { get; set; }
    [MaxLength(255)]
    public string? Password { get; set; }
    public bool Status { get; set; }
    
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
    public virtual Counter? Counter { get; set; }
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    public virtual Role? Role { get; set; }
}
