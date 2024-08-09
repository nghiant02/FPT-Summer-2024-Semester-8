using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Implements;
using MCC.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXE201.DAL.DTOs;

namespace EXE201.DAL.Repository
{
    public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
    {
        private readonly IProductRepository _productRepository;
        public InventoryRepository(EXE201Context context, IProductRepository productRepository) : base(context)
        {
            _productRepository = productRepository;
        }

        public async Task<(int, int, IEnumerable<ViewInventoryDto>)> Inventories(int pageNumber, int pageSize)
        {
            try
            {
                var totalRecord = await _context.Inventories.CountAsync();
                var totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);

                var inventories = await _context.Inventories
                    .Include(p => p.Product).ThenInclude(c => c.Category)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                var listInventories = new List<ViewInventoryDto>();
                foreach (var inventory in inventories)
                {
                    var products = await _productRepository.GetProductsById(inventory.Product.ProductId);
                    var viewInventoryDto =  new ViewInventoryDto
                    {
                        InventoryId = inventory.InventoryId,
                        ProductName = inventory.Product.ProductName,
                        ProductImage = products.First().ProductImage.First(),
                        QuantityAvailable = inventory.QuantityAvailable,
                        CategoryName = inventory.Product.Category.CategoryName,
                        Status = inventory.Status
                    };
                    listInventories.Add(viewInventoryDto);
                }  

               
                return (totalRecord, totalPage, listInventories);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteInventory(int inventoryId)
        {
            try
            {
                var inventory = await _context.Inventories.FindAsync(inventoryId);
                if (inventory == null)
                {
                    return false;
                }

                _context.Inventories.Remove(inventory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}