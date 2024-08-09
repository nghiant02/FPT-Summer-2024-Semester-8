using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MCC.DAL.Repository.Interface;

public interface IGenericRepository<T> where T : class
{
    DbSet<T> Entities();
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    Task Delete(T entity);
    Task SaveChangesAsync();

    Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);
}