using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class BookAuthor
{
    [MaxLength(7)]
    public required string AuthorId { get; set; }
    [MaxLength(7)]
    public required string BookId { get; set; }
    [MaxLength(50)]
    public string? AuthorOrder { get; set; }
    [MaxLength(50)]
    public string? RoyaltyPercentage { get; set; }
    // Navigation property
    public Author? Author { get; set; }
    public Book? Book { get; set; }
}