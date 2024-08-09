using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Author
{
    [MaxLength(7)]
    public required string AuthorId { get; set; }
    [MaxLength(50)]
    public string? LastName { get; set; }
    [MaxLength(50)]
    public string? MiddelName { get; set; }
    [MaxLength(50)]
    public required string FirstName { get; set; }
    [MaxLength(16)]
    public string? Phone { get; set; }
    [MaxLength(255)]
    public string? Address { get; set; }
    [MaxLength(50)]
    public string? City { get; set; }
    [MaxLength(50)]
    public string? State { get; set; }
    [MaxLength(50)]
    public string? Zip { get; set; }
    [MaxLength(50)]
    public string? EmailAddress { get; set; }
    // Navigation property
    public IEnumerable<BookAuthor>? BookAuthors { get; set; }
}