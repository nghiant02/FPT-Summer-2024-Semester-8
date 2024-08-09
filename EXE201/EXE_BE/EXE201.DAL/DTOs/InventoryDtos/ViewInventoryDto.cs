namespace EXE201.DAL.DTOs;

public class ViewInventoryDto
{
    public int InventoryId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductImage { get; set; }
    public int? QuantityAvailable { get; set; }
    public string? CategoryName { get; set; }
    public string? Status { get; set; }
}