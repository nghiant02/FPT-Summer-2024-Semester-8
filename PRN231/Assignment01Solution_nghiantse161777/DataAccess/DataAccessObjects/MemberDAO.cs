using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private readonly eStoreDBContext _context;
        private readonly IMapper _mapper;

        public MemberDAO(eStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDTO>> GetMembersAsync() =>
            _mapper.Map<IEnumerable<MemberDTO>>(await _context.Members.ToListAsync());

        public async Task<MemberDTO> GetMemberByIdAsync(int memberId) =>
            _mapper.Map<MemberDTO>(await _context.Members.FindAsync(memberId));

        public async Task<MemberDTO> GetMemberByEmailAsync(string email) =>
            _mapper.Map<MemberDTO>(await _context.Members.FirstOrDefaultAsync(m => m.Email == email));

        public async Task AddMemberAsync(MemberCreateUpdateDTO memberDTO)
        {
            var member = _mapper.Map<Member>(memberDTO);
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMemberAsync(int memberId, MemberCreateUpdateDTO memberDTO)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member != null)
            {
                _mapper.Map(memberDTO, member);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMemberAsync(int memberId)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
        }
    }
}
