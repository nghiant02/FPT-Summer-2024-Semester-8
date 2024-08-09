namespace EXE201.DAL.DTOs.RentalOrderDTOs;

public class ViewReturnOrderDto
{
    public int OrderId { get; set; }
    public DateTime? DatePlaced { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string? Username { get; set; }
    public string? ReturnReason { get; set; }
}