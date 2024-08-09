namespace BusinessObjects.Dto.ResponseDto;

public class UserResponseDto
{
    public required string UserId { get; set; }
    public string? RoleName { get; set; }
    public string? CounterId { get; set; }
    public int? CounterNumber { get; set; }
    public string? Username { get; set; }
    public string? FullName { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool Status { get; set; }
}