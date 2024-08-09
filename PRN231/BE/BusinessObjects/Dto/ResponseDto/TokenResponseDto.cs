namespace BusinessObjects.Dto.ResponseDto;

public class TokenResponseDto
{
    public string? Token { get; set; }
    public DateTime Expiration { get; set; }
}