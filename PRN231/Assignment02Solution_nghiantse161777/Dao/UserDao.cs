using BusinessObject;
using BusinessObject.Dto.RequestDto;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace Dao;

public class UserDao
{
    private readonly AppDbContext? _context;
    public UserDao()
    {
        _context = new AppDbContext();
    }
    public async Task<User?> Login(LoginDto loginDto)
    {
        if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password) || _context == null)
        {
            return null;
        }
        var user = await _context.Users.FirstOrDefaultAsync(x => x.EmailAddress == loginDto.Email && x.Password == loginDto.Password);
        return user;
    }
    public async Task<User?> Register(User user)
    {
       if(_context == null) return null;
       user.UserId = Generator.GenerateId();
       user.PubId = "1";
       user.RoleId = "1";
       await _context.Users.AddAsync(user);
       await _context.SaveChangesAsync();
       return user;
    }
    public async Task<bool> CheckDupicateEmail(string? email)
    {
        if (_context == null) return true;
        return await _context.Users.AnyAsync(x => x.EmailAddress == email);
    }
}
