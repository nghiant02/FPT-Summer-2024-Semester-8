using AutoMapper;
using EXE201.DAL.DTOs.ProductDTOs;
using EXE201.DAL.DTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXE201.BLL.Interfaces;
using EXE201.DAL.Models;

namespace EXE201.BLL.Services
{
    public class ProductDetailServices : IProductDetailServices
    {
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IMapper _mapper;

        public ProductDetailServices(IProductDetailRepository productDetailRepository, IMapper mapper)
        {
            _productDetailRepository = productDetailRepository;
            _mapper = mapper;
        }

        public async Task<ResponeModel> AddProductDetail(AddProductDetailDTO addProductDetail)
        {
            return await _productDetailRepository.AddProductDetail(addProductDetail);
        }

        public async Task<ResponeModel> DeleteProductDetail(int id)
        {
            return await _productDetailRepository.DeleteProductDetail(id);
        }

        public async Task<ProductDetail> GetProductDetailByProductId(int productId)
        {
            return await _productDetailRepository.GetProductDetailByProductId(productId);
        }

        public async Task<ResponeModel> UpdateProductDetail(UpdateProductDetailDTO updateProductDetail)
        {
            return await _productDetailRepository.UpdateProductDetail(updateProductDetail);
        }
    }
}
