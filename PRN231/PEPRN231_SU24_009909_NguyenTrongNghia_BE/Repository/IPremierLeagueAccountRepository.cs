using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IPremierLeagueAccountRepository
    {
        Task<PremierLeagueAccount> Login(string email, string password);
    }
}
