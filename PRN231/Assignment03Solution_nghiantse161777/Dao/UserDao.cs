using BussiniseObject.Models;
using Microsoft.EntityFrameworkCore;

namespace Dao;

public class UserDao
{
    private readonly ApplicationDbContext _context = new();

    public async Task<IEnumerable<AspNetUser>> GetUsers()
    {
        return await _context.AspNetUsers.ToListAsync();
    }
    public async Task<AspNetUser?> Login(string email, string password)
    {
        var user = await _context.AspNetUsers.FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == password);
        return user;
    } 
    public async Task<string> AccessFailedCount(string email)
    {
        var user = await _context.AspNetUsers.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null) return string.Empty;

        user.AccessFailedCount++;
        await _context.SaveChangesAsync();

        if (user.AccessFailedCount < 5)
        {
            return $"You have reached {user.AccessFailedCount} attempts. If you reach 5, your account will be locked for 5 minutes.";
        }

        if (user.AccessFailedCount >= 5)
        {
            // Lock the account and set the lockout end date
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(5);
            await _context.SaveChangesAsync();
        
            // Calculate the remaining lockout time
            var remainingLockoutTime = user.LockoutEnd.Value - DateTimeOffset.UtcNow;
            return $"Your account has been locked for 5 minutes due to too many failed login attempts. " +
                   $"Time remaining: {remainingLockoutTime.Minutes} minutes and {remainingLockoutTime.Seconds} seconds.";
        }

        return $"You have {user.AccessFailedCount} failed login attempts.";
    }


    public async Task LockUser(string email)
    {
        var user = await _context.AspNetUsers.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null) return;
        user.LockoutEnd = DateTime.UtcNow.AddMinutes(5);
        await _context.SaveChangesAsync();
    }
}