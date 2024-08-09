namespace BusinessObject.Dto.ResponseDto;

public class ResponseBaseDto
{
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
    public object? Data { get; set; }
}
