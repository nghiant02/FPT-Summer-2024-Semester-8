using BusinessObject.Models;
using Repository.GenericRepository;

namespace Repository.Interface
{
    public interface IAuthorRepository : IReadRepository<Author>, ICreateRepository<Author>, IUpdateRepository<Author>, IDeleteRepository<Author>
    {

    }
}
