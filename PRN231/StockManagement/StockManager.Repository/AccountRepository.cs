using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockManage.BusinessObjects.Models;
using StockManager.DAO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockManage.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _configuration;

        public Account GetAccount(string email, string password) => AccountDAO.Instance.GetAccount(email, password);
        public List<Account> GetAllAccount() => AccountDAO.Instance.GetAccounts();
    }
}
