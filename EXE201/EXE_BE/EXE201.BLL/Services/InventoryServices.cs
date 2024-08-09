using AutoMapper;
using EXE201.BLL.Interfaces;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXE201.DAL.DTOs;

namespace EXE201.BLL.Services
{
    public class InventoryServices : IInventoryServices
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public InventoryServices(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public async Task<PagingResponse> GetInventories(int pageNumber, int pageSize)
        {
            var listInventories = await _inventoryRepository.Inventories(pageNumber, pageSize);
            var inventoriesPaging = new PagingResponse
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecord = listInventories.Item1,
                TotalPage = listInventories.Item2,
                Data = listInventories.Item3
            };
            return inventoriesPaging;
        }

        public async Task<bool> DeleteInventory(int inventoryId)
        {
             return await _inventoryRepository.DeleteInventory(inventoryId);
        }
    }
}
