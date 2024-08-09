using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Implements;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.Repository
{
    public class VerifyCodeRepository : GenericRepository<VerifyCode>, IVerifyCodeRepository 
    { 
        public VerifyCodeRepository(EXE201Context context) : base(context)
        {
        }
        
        public async Task<VerifyCode> GetByCodeAsync(string code)
        {
            return await _context.VerifyCodes.FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}
