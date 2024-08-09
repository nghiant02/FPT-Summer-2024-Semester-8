using StockManage.BusinessObjects.Models;
using StockManage.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.DAO
{
    public class AccountDAO
    {
        private readonly Stocks2024DBContext dBContext = null;
        private static AccountDAO instance = null;

        public AccountDAO()
        {
            dBContext = new Stocks2024DBContext();
        }

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance; 
            }
        }

        public Account GetAccount(string email, string password)
        {
            try
            {
                return dBContext.Accounts.FirstOrDefault(a => a.Email.Equals(email) && a.Password.Equals(password) 
                        && a.Status.Equals("active"));
            }catch (Exception ex)
            {
                throw new Exception();
            }return null;
        }

        public List<Account> GetAccounts()
        {
            try
            {
                return dBContext.Accounts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return null;
        }
    }
}
