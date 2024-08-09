using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserAccountRepository
    {
        Task<UserAccount> Login(string email, string password);
    }
}
