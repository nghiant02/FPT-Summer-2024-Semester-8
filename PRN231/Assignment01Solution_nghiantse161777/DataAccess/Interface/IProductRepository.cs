using BusinessObject.DTO;
using BusinessObject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(int productId, ProductCreateUpdateDTO productDTO);
        Task DeleteProductAsync(int productId);
        Task<IEnumerable<ProductDTO>> SearchProductsAsync(string productName, decimal? unitPrice);
    }
}
