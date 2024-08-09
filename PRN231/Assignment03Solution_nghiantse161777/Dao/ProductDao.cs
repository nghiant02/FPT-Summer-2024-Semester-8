using BussiniseObject.Models;
using Microsoft.EntityFrameworkCore;

namespace Dao;

public class ProductDao
{
    private readonly  ApplicationDbContext _context = new();
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }
    public async Task<Product> GetProduct(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
    }
    public async Task<Product> CreateProduct(Product product)
    {
        var lastProduct = await _context.Products.OrderByDescending(x => x.ProductId).FirstOrDefaultAsync();
        product.ProductId = lastProduct == null ? 1 : lastProduct.ProductId + 1;
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task<Product> UpdateProduct(int id, Product product)
    {
        var productToUpdate = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
        if (productToUpdate == null) return null;
        productToUpdate.ProductName = product.ProductName;
        productToUpdate.CategoryId = product.CategoryId;
        productToUpdate.Weight = product.Weight;
        productToUpdate.UnitPrice = product.UnitPrice;
        productToUpdate.UnitsInStock = product.UnitsInStock;
        await _context.SaveChangesAsync();
        return productToUpdate;
    }
    public async Task<Product> DeleteProduct(int id)
    {
        var productToDelete = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
        if (productToDelete == null) return null;
        _context.Products.Remove(productToDelete);
        await _context.SaveChangesAsync();
        return productToDelete;
    }
}