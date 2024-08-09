using EXE201.DAL.DTOs;
using EXE201.DAL.DTOs.DashboardDTOs;
using EXE201.DAL.DTOs.ProductDTOs;
using EXE201.DAL.Models;
using LMSystem.Repository.Helpers;
using MCC.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<ProductDetailDTO> GetById(int id);
        Task<IEnumerable<ProductDetailDTO>> GetProductsById(int productId);
        Task<ResponeModel> AddProduct(AddProductDTO addProduct);
        Task<ResponeModel> PermanentDeleteProduct(int productId);
        Task<ResponeModel> UpdateProduct(UpdateProductDTO updateProductDTO);
        Task<ResponeModel> DeleteProduct(int id);
        Task<ResponeModel> RecoverProduct(int id);
        Task<PagedResponseDTO<ProductListDTO>> GetFilteredProducts(ProductFilterDTO filter);
        Task<IEnumerable<ProductRecommendationDTO>> GetHotProducts(int topN);
        Task<IEnumerable<ProductRecommendationDTO>> GetNewProducts(int topN);
        Task<IEnumerable<ProductRecommendationDTO>> GetHighlyRatedProducts(int topN);
        Task<PagedResponseDTO<ProductListRecommendByCategoryDTO>> GetProductRecommendationsByCategory(int productId, ProductPagingRecommendByCategoryDTO filter);
        Task<IEnumerable<ProductSuggestionDTO>> GetProductSuggestions(string searchTerm);
        Task<ResponeModel> AddColorToProduct(int productId, int colorId, string ProductColorImage);
        Task<ResponeModel> DeleteColorFromProduct(int productId, int colorId);
        Task<ResponeModel> AddSizeToProduct(int productId, int sizeId);
        Task<ResponeModel> DeleteSizeFromProduct(int productId, int sizeId);
        Task<ResponeModel> UpdateColorImageForProduct(int productId, int colorId, string newColorImage);
        Task<int> GetTotalItemsInStock();
        Task<List<CategoryOrderCountDTO>> GetMostOrderedProductCategory();
        Task<IEnumerable<ProductStockDTO>> GetTotalItemsInStockForEachProduct();
    }
}
