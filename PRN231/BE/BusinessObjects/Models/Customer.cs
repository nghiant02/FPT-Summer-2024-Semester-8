using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public partial class Customer
{
    [MaxLength(7)]
    public required string CustomerId { get; set; }
    [MaxLength(255)]

    public string? UserName { get; set; }
    [MaxLength(255)]
    
    public string? FullName { get; set; }
    [MaxLength(255)]
    
    public string? Email { get; set; }
    [MaxLength(255)]
    public string? Phone { get; set; }
    [MaxLength(255)]
    
    public string? Gender { get; set; }
    [MaxLength(255)]
    public string? Address { get; set; }
    [MaxLength(255)]
    public string? Password { get; set; }
    public int? Point { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset UpdatedAt { get; set; }
    
    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
