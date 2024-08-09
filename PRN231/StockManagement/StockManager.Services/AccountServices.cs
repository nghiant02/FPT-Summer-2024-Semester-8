using StockManage.BusinessObjects.Models;
using StockManage.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManage.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository accountRepository;

        public AccountServices(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public Account GetAccount(string email, string password)
        {
            return accountRepository.GetAccount(email, password);
        }

        public bool isUser(string email, string password)
        {
            return accountRepository.GetAccount(email, password) != null;
        }

        public List<Account> GetAllAccount()
        {
            return accountRepository.GetAllAccount();
        }
    }
}
