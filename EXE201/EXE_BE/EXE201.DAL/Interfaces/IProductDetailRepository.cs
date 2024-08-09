using EXE201.DAL.DTOs.ProductDTOs;
using EXE201.DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXE201.DAL.Models;

namespace EXE201.DAL.Interfaces
{
    public interface IProductDetailRepository
    {
        Task<ProductDetail> GetProductDetailByProductId(int productId);
        Task<ResponeModel> AddProductDetail(AddProductDetailDTO addProductDetail);
        Task<ResponeModel> DeleteProductDetail(int productDetailId);
        Task<ResponeModel> UpdateProductDetail(UpdateProductDetailDTO updateProductDetail);
    }
}
