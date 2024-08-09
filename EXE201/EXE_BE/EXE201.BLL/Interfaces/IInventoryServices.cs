using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXE201.DAL.DTOs;

namespace EXE201.BLL.Interfaces
{
    public interface IInventoryServices
    {
        Task<PagingResponse> GetInventories(int pageNumber, int pageSize);
        Task<bool> DeleteInventory(int inventoryId);
    }
}
