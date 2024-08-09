using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Interface;
using BusinessObject.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;

        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetMembers()
        {
            var members = await _memberRepository.GetMembersAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDTO>> GetMemberById(int id)
        {
            var member = await _memberRepository.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult<MemberDTO>> CreateMember(MemberCreateUpdateDTO memberDTO)
        {
            var createdMember = await _memberRepository.AddMemberAsync(memberDTO);
            return CreatedAtAction(nameof(GetMemberById), new { id = createdMember.MemberId }, createdMember);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, MemberCreateUpdateDTO memberDTO)
        {
            await _memberRepository.UpdateMemberAsync(id, memberDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            await _memberRepository.DeleteMemberAsync(id);
            return NoContent();
        }
    }
}
