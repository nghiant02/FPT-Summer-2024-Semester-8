using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Validation;

namespace DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly eStoreDBContext _context;
        private readonly IMapper _mapper;
        private readonly MemberValidator _validator;

        public MemberRepository(eStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _validator = new MemberValidator();
        }

        public async Task<IEnumerable<MemberDTO>> GetMembersAsync() =>
            _mapper.Map<IEnumerable<MemberDTO>>(await _context.Members.ToListAsync());

        public async Task<MemberDTO> GetMemberByIdAsync(int memberId) =>
            _mapper.Map<MemberDTO>(await _context.Members.FindAsync(memberId));

        public async Task<MemberDTO> AddMemberAsync(MemberCreateUpdateDTO memberDTO)
        {
            var member = _mapper.Map<Member>(memberDTO);
            _validator.Validate(member);

            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
            return _mapper.Map<MemberDTO>(member);
        }

        public async Task UpdateMemberAsync(int memberId, MemberCreateUpdateDTO memberDTO)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member != null)
            {
                _mapper.Map(memberDTO, member);
                _validator.Validate(member);

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

        public async Task<Member> GetMemberByEmailAsync(string email)
        {
            return await _context.Members.SingleOrDefaultAsync(m => m.Email == email);
        }
    }
}
