using System.Net.Mime;
using EXE201.DAL.Models;

namespace EXE201.DAL.DTOs.CartDTOs;

public class ViewCartDto
{
    public int CartId { get; set; }
    public int? UserId { get; set; }
    public int? ProductId { get; set; }
    public string ProductTitle { get; set; }
    public int? Quantity { get; set; }
    public double? ProductPrice { get; set; }
    public List<string> ProductImageUrl { get; set; }
    public DateTime? RentalStart { get; set; }
    public DateTime? RentalEnd { get; set; }
}