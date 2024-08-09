using BusinessObject.Models;
using Repository.GenericRepository;
namespace Repository.Interface;

public interface IPublisherRepository : IReadRepository<Publisher>, ICreateRepository<Publisher>, IUpdateRepository<Publisher>, IDeleteRepository<Publisher>
{
}
