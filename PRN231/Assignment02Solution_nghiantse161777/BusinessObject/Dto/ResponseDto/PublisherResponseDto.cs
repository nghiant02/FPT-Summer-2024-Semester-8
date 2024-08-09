namespace BusinessObject.Dto.ResponseDto;

public class PublisherResponseDto
{
    public required string PubId { get; set; }
    public required string PublisherName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}
