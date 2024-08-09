using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class PremierLeagueAccountDao
    {
        private readonly EnglishPremierLeague2024DbContext _context;

        public PremierLeagueAccountDao()
        {
            _context = new EnglishPremierLeague2024DbContext();
        }

        public async Task<PremierLeagueAccount> Login(string email, string password)
        {
            return await _context.PremierLeagueAccounts
                .Where(u => u.EmailAddress == email && u.Password == password)
                .FirstOrDefaultAsync();
        }
    }
}
