namespace EXE201.DAL.DTOs.UserDTOs;

public class MembershipUserDto
{
    public int MembershipId { get; set; }
    public int? UserId { get; set; }
    public int? MembershipTypeId { get; set; }
    public string? MembershipTypeName { get; set; }
    public string? MembershipTypeDescription { get; set; }
    public string? MembershipTypeBenefits { get; set; }
    public DateTime? StartDate { get; set; }
    public string? MembershipStatus { get; set; }
}