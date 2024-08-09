using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class User
{
    [MaxLength(7)]
    public required string UserId { get; set; }
    [MaxLength(50)]
    public string? EmailAddress { get; set; }
    [MaxLength(50)]
    public required string? Password { get; set; }
    [MaxLength(50)]
    public string? Source { get; set; }
    [MaxLength(50)]
    public string? FirstName { get; set; }
    [MaxLength(50)]
    public string? LastName { get; set; }
    [MaxLength(50)]
    public string? MiddleName { get; set; }
    [MaxLength(7)]
    public required string RoleId { get; set; }
    [MaxLength(7)]
    public required string PubId { get; set; }
    public DateTimeOffset? HireDate { get; set; }
    // Navigation property
    public Role? Role { get; set; }
    public Publisher? Publisher { get; set; }
}