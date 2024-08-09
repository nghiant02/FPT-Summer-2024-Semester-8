using BusinessObject.DTO;
using BusinessObject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IMemberRepository
    {
        Task<IEnumerable<MemberDTO>> GetMembersAsync();
        Task<MemberDTO> GetMemberByIdAsync(int memberId);
        Task<MemberDTO> AddMemberAsync(MemberCreateUpdateDTO memberDTO);
        Task UpdateMemberAsync(int memberId, MemberCreateUpdateDTO memberDTO);
        Task DeleteMemberAsync(int memberId);
        Task<Member> GetMemberByEmailAsync(string email);
    }
}
