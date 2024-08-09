namespace BusinessObject.Dto.ResponseDto.ResponseDto;

public class AuthorResponseDto
{
    public required string AuthorId { get; set; }
    public required string LastName { get; set; }
    public required string MiddelName { get; set; }
    public required string FirstName { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Zip { get; set; }
    public string? EmailAddress { get; set; }
}
