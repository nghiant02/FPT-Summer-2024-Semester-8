namespace Repository.GenericRepository
{
    public interface IUpdateRepository<T>
    {
        Task<bool> Update(string id, T entity);
    }
}
