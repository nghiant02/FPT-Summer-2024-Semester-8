using BusinessObjects.Dto;
using BusinessObjects.Models;

namespace Services.Interface;

public interface IRoleService
{
    public Task<IEnumerable<Role?>?> Gets();
    public Task<Role?> GetById(string id);
    public Task<int> Create(RoleDto entity);
}