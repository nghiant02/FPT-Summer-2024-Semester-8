using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly UserAccountDao _userAccountDao;

        public UserAccountRepository(UserAccountDao userAccountDao)
        {
            _userAccountDao = userAccountDao;
        }

        public async Task<UserAccount> Login(string email, string password)
        {
            return await _userAccountDao.Login(email, password);
        }
    }
}
