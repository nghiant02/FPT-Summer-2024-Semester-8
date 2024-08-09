using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MCC.DAL.Repository.Implements;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    public EXE201Context _context;
    public DbSet<T> _dbSet;
    public GenericRepository(EXE201Context context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        await SaveChangesAsync();
    }

    public DbSet<T> Entities()
    {
        return _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    // public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    // {
    //     return await _dbSet.Where(predicate).ToListAsync();
    // }
    
    public async Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.Where(predicate);

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
