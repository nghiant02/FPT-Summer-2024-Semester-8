using EXE201.DAL.Models;
using MCC.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXE201.DAL.DTOs;

namespace EXE201.DAL.Interfaces
{
    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
        Task<(int, int, IEnumerable<ViewInventoryDto>)> Inventories(int pageNumber, int pageSize);
        Task<bool> DeleteInventory(int inventoryId);
    }
}