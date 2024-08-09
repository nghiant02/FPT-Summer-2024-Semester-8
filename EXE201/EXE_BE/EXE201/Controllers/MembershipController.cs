using EXE201.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXE201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        //private readonly IMembershipServices _membershipServices;
        //public MembershipController(IMembershipServices membershipServices)
        //{
        //    _membershipServices = membershipServices;
        //}

        //[HttpGet("GetAllMembership")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var result = await _membershipServices.GetMemberships();
        //    return Ok(result);
        //}

        //[HttpGet("GetMembershipByUserId")]
        //public async Task<IActionResult> GetMembershipByUserId(int userId)
        //{
        //    var result = await _membershipServices.GetMembershipByUserId(userId);
        //    return Ok(result);
        //}
    }
}
