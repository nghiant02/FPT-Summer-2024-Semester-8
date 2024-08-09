using BusinessObjects.Models;
using Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PremierLeagueAccountRepository : IPremierLeagueAccountRepository
    {
        private readonly PremierLeagueAccountDao _premierLeagueAccountDao;

        public PremierLeagueAccountRepository(PremierLeagueAccountDao premierLeagueAccountDao)
        {
            _premierLeagueAccountDao = premierLeagueAccountDao;
        }

        public async Task<PremierLeagueAccount> Login (string email, string password)
        {
            return await _premierLeagueAccountDao.Login (email, password);
        }
    }
}
