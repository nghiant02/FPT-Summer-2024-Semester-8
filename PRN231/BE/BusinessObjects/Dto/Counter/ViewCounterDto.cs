using BusinessObjects.Models;

namespace BusinessObjects.Dto.Counter;

public class ViewCounterDto
{
    public string? CounterId { get; set; }
    public int? Number { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    
    public Bill? Bill { get; set; }
}