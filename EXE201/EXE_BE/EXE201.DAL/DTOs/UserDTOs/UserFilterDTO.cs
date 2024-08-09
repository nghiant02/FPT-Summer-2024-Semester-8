public class UserFilterDTO
{
    public string? Search { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public int? Gender { get; set; }
    public string? MembershipTypeName { get; set; }
    public string? SortBy { get; set; }
    public bool Sort { get; set; } // true for descending, false for ascending
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}