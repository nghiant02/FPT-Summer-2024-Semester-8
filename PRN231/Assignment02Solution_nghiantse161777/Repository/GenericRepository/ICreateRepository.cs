namespace Repository.GenericRepository
{
    public interface ICreateRepository<T>
    {
        Task<T> Create(T entity);
    }
}
