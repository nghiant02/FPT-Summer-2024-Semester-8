using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models;

public partial class Role
{
    [MaxLength(7)]
    public required string RoleId { get; set; }
    [MaxLength(255)]
    public string? RoleName { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
