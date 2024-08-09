using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Publisher
{
    [MaxLength(7)]
    public required string PubId { get; set; }
    [MaxLength(50)]
    public required string PublisherName { get; set; }
    [MaxLength(50)]
    public string? City { get; set; }   
    [MaxLength(50)]
    public string? State { get; set; }
    [MaxLength(50)]
    public string? Country { get; set; }
    // Navigation property
    public IEnumerable<User>? Users { get; set; }
    public IEnumerable<Book>? Books { get; set; }
}