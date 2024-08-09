using AutoMapper;
using BusinessObjects.Dto;
using BusinessObjects.Models;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implementation;

public class RoleService(IRoleRepository roleRepository, IMapper mapper) : IRoleService
{
    public IRoleRepository RoleRepository { get; } = roleRepository;
    public IMapper Mapper { get; } = mapper;

    public async Task<IEnumerable<Role?>?> Gets()
    {
        return await RoleRepository.Gets();
    }

    public async Task<Role?> GetById(string id)
    {
        return await RoleRepository.GetById(id);
    }

    public async Task<int> Create(RoleDto entity)
    {
        return await RoleRepository.Create(Mapper.Map<Role>(entity));
    }
}