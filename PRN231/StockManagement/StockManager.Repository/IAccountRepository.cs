using StockManage.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManage.Repository
{
    public interface IAccountRepository
    {
        public Account GetAccount(string email, string password);
        public List<Account> GetAllAccount();
    }
}
