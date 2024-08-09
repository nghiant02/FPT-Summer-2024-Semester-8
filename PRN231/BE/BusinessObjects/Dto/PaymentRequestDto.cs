namespace BusinessObjects.Dto;
 
 public class PaymentRequestDto
 {
     public int Amount { get; set; }
     public string? ReturnUrl { get; set; }
 }