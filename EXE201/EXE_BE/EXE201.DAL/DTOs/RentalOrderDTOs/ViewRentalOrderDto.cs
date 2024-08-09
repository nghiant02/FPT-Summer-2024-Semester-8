namespace EXE201.DAL.DTOs.RentalOrderDTOs;

public class ViewRentalOrderDto
{
    public int OrderId { get; set; }
    public string? OrderStatus { get; set; }
    public DateTime? DatePlaced { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal? MoneyReturned { get; set; }
    public string? ProductName { get; set; }
    public string? Username { get; set; }
    public string? ReturnReason { get; set; }
}