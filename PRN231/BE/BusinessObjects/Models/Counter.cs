using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public partial class Counter
{
    [MaxLength(7)]
    public required string CounterId { get; set; }
    public int? Number { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
