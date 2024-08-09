using BusinessObjects.Dto;
using Management.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController(IRoleService roleService) : ControllerBase
{
    public IRoleService RoleService { get; } = roleService;
    [Authorize(Roles = "Admin, Manager, Staff")]
    [HttpGet("GetRoles")]
    public async Task<IActionResult> GetRoles()
    {
        return Ok(await roleService.Gets());
    }
    [Authorize(Roles = "Admin, Manager, Staff")]

    [HttpGet("GetRoleById/{roleId}")]
    public async Task<IActionResult> GetRoleById(string roleId)
    {
        return Ok(await roleService.GetById(roleId));
    }
    [Authorize(Roles = "Admin, Manager")]

    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRole(RoleDto roleDto)
    {
        return Ok(await roleService.Create(roleDto));
    }
}
