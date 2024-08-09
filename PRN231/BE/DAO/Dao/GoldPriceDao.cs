using BusinessObjects.Context;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO.Dao;

public class GoldPriceDao
{
    private readonly JssatsContext _context;
    public GoldPriceDao()
    {
        _context = new JssatsContext();
    }
    public async Task<IEnumerable<Gold>?> GetGoldPrices()
    {
        return await _context.Golds.ToListAsync();
    }
    public async Task<Gold?> GetGoldPriceById(string id)
    {
        return await _context.Golds.FindAsync(id);
    }
    public async Task<int> Create(Gold gold)
    {
        _context.Golds.Add(gold);
        return await _context.SaveChangesAsync();
    }
    public async Task<int> Update(Gold gold)
    {
        _context.Golds.Update(gold);
        return await _context.SaveChangesAsync();
    }
}