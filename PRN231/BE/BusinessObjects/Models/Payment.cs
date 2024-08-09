using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Payment
{
    [MaxLength(7)]
    public required string PaymentId { get; set; }
    [MaxLength(7)]
    public required string BillId { get; set; }
    public long OrderCode { get; set; }
    public float Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus PaymentStatus { get; set; } 
    // Navigation properties
    public Bill? Bill { get; set; }
}