
namespace Repositories.Interface.GenericRepository
{
    public interface IUpdateRepository<T>
    {
        Task<int> Update(string id, T entity);
    }
}
