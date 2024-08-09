using BussiniseObject.Models;
using Dao;

namespace Repository;

public class ProductRepository : IProductRepository
{
    private readonly ProductDao _productDao;

    public ProductRepository(ProductDao productDao)
    {
        _productDao = productDao;
    }
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _productDao.GetProducts();
    }

    public async Task<Product> GetProduct(int id)
    {
        return await _productDao.GetProduct(id);
    }

    public async Task<Product> CreateProduct(Product product)
    {
        return await _productDao.CreateProduct(product);
    }

    public async Task<Product> UpdateProduct(int id, Product product)
    {
        return await _productDao.UpdateProduct(id, product);
    }

    public async Task<Product> DeleteProduct(int id)
    {
        return await _productDao.DeleteProduct(id);
    }
}