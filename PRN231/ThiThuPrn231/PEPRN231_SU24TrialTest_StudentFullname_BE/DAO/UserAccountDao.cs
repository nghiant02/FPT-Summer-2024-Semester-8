using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class UserAccountDao
    {
        private readonly WatercolorsPainting2024DbContext _context;

        public UserAccountDao()
        {
            _context = new WatercolorsPainting2024DbContext();
        }

        public async Task<UserAccount> Login(string email, string password)
        {
            return await _context.UserAccounts
                .Where(u => u.UserEmail == email && u.UserPassword == password)
                .FirstOrDefaultAsync();
        }
    }
}
