using BusinessObjects.Context;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Tools;

namespace DAO.Dao;

public class UserDao
{
    private readonly JssatsContext _context;

    public UserDao()
    {
        _context = new JssatsContext();
    }

    public async Task<User?> GetUser(string email, string password)
    {
        return await _context.Users
            .Include(r => r.Role)
            .FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
    }

    public async Task<IEnumerable<User?>?> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<int> AddUser(User user)
    {
        _context.Users.Add(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> CreateUser(User user)
    {
        user.UserId = Generator.GenerateId();
        user.CreatedAt = DateTime.UtcNow.ToUniversalTime();
        await _context.Users.AddAsync(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateUser(string id, User user)
    {
        var existUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        if (existUser == null) return 0;
        
        existUser.CounterId = user.CounterId;
        existUser.Gender = user.Gender;
        existUser.Email = user.Email;
        existUser.PhoneNumber = user.PhoneNumber;
        existUser.FullName = user.FullName;
        existUser.Username = user.Username;
        existUser.RoleId = user.RoleId;
        
        existUser.UpdatedAt = DateTime.UtcNow.ToUniversalTime();
        _context.Users.Update(existUser);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateCounterByUserId(string userId, string counterId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        if (user == null) return false;
        user.CounterId = counterId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> GetUserById(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<int> DeleteUser(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return 0;
        _context.Users.Remove(user);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            return 0;
        }

        return 1;
    }
    public async Task<string> GetUserIdByName(string name)
    {
        var user = await _context.Users.FirstOrDefaultAsync(c => c.Username == name);
        return user?.UserId;
    }
}