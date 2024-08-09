using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Interfaces
{
    public interface IDepositServices
    {
        Task<Deposit> GetDepositByIdAsync(int id);
        Task<IEnumerable<Deposit>> GetAllDepositAsync();
    }
}
