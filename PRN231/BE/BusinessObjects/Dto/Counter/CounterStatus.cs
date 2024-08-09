namespace BusinessObjects.DTO;

public class CounterStatus
{
    public required string Id { get; set; }
    public string? CounterId { get; set; }
    public string? Number { get; set; }
    public bool IsOccupied { get; set; }
}