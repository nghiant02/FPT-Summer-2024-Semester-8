﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EXE201.DAL.Models;

[Table("Notification")]
public partial class Notification
{
    [Key]
    [Column("NotificationID")]
    public int NotificationId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "ntext")]
    public string NotificationMessage { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateSent { get; set; }

    [StringLength(50)]
    public string NotificationType { get; set; }

    public bool? Seen { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Notifications")]
    public virtual User User { get; set; }
}