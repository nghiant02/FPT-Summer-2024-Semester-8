namespace EXE201.DAL.DTOs;

public class PagingResponse
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPage { get; set; }
    public int TotalRecord { get; set; }
    public object? Data { get; set; }
}
