using BusinessObjects.Models;
using Repositories.Interface.GenericRepository;

namespace Repositories.Interface;

public interface IPromotionRepository : IReadRepository<Promotion>, ICreateRepository<Promotion>, IUpdateRepository<Promotion>, IDeleteRepository<Promotion>
{
}
