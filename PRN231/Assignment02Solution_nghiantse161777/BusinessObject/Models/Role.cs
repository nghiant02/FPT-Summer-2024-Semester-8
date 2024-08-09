using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Role
{
    [MaxLength(7)]
    public required string RoleId { get; set; }
    [MaxLength(50)]
    public required string RoleDesc { get; set; }
    // Navigation property 
    public IEnumerable<User>? Users { get; set; }
}