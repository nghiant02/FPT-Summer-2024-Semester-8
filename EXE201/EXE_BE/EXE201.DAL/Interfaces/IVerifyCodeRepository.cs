using EXE201.DAL.Models;
using MCC.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.Interfaces
{
    public interface IVerifyCodeRepository : IGenericRepository<VerifyCode>
    {
        Task<VerifyCode> GetByCodeAsync(string code);
    }
}
