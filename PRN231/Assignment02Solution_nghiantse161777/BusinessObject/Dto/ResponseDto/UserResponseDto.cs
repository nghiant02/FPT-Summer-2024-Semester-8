namespace BusinessObject.Dto.ResponseDto.ResponseDto;

public class UserResponseDto
{
    public string? UserId { get; set; }
    public string? EmailAddress { get; set; }
    public string? Password { get; set; }
    public string? Source { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? RoleId { get; set; }
    public string? PubId { get; set; }
    public DateTimeOffset? HireDate { get; set; }
}
