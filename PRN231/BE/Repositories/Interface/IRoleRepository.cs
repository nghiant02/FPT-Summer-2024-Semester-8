using BusinessObjects.Models;
using Repositories.Interface.GenericRepository;

namespace Repositories.Interface;

public interface IRoleRepository : IReadRepository<Role>, ICreateRepository<Role>
{
    
}