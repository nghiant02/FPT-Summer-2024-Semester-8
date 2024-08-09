using BusinessObject.Models;

namespace Repository
{
    public interface IUserAccountRepository
    {
        Task<UserAccount> Login(string email, string password);
    }
}
