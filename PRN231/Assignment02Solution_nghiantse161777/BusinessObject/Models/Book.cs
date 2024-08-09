using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Book
{
    [MaxLength(7)]
    public required string BookId { get; set; }
    [MaxLength(50)]
    public required string Title { get; set; }
    [MaxLength(50)]
    public string? Type { get; set; }
    [MaxLength(7)]
    public required string PubId { get; set; }
    public float Price { get; set; }
    public int Advance { get; set; }
    public int Royalty { get; set; }
    public int? YtdSales { get; set; }
    public string? Notes { get; set; }
    public DateTimeOffset PublishedDate { get; set; }
    // Navigation property
    public Publisher? Publisher { get; set; } 
    public IEnumerable<BookAuthor>? BookAuthors { get; set; }
}