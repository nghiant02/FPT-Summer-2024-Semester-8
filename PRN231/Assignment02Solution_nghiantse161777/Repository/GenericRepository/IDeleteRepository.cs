namespace Repository.GenericRepository
{
    public interface IDeleteRepository<T>
    {
        Task<int> Delete(string id);
    }
}
