using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Interfaces
{
    public interface IForgotPawwordService
    {
        Task<VerifyCode> AddCode(string code, string email);
        Task<User> VerifyCode(string code, string email);
        Task<bool> IsCodeExist(string email);
        Task<bool> Delete(string email);
    }
}
