namespace Repository.GenericRepository
{
    public interface IFindRepository<T>
    {
        Task<IEnumerable<T>> Find(Func<T, bool> predicate);
    }
}
