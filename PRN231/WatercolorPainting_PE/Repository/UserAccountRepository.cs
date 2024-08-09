using BusinessObject.Models;
using Dao;

namespace Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly UserAccountdao _userAccountdao;
        public UserAccountRepository(UserAccountdao userAccountdao)
        {
            _userAccountdao = userAccountdao;
        }
        public async Task<UserAccount> Login(string email, string password)
        {
            return await _userAccountdao.Login(email, password);
        }
    }
}
