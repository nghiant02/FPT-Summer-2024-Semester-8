using EXE201.DAL.DTOs.ProductDTOs;
using EXE201.DAL.DTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EXE201.DAL.Repository
{
    public class ProductDetailRepository : GenericRepository<Product>, IProductDetailRepository
    {
        private readonly EXE201Context _context;
        public ProductDetailRepository(EXE201Context context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductDetail> GetProductDetailByProductId(int productId)
        {
            return await _context.ProductDetails
                .FirstOrDefaultAsync(pd => pd.Product.ProductId == productId);
        }

        public async Task<ResponeModel> AddProductDetail(AddProductDetailDTO addProductDetail)
        {
            try
            {
                var productDetail = new ProductDetail
                {
                    ProductId = addProductDetail.ProductId,
                    Description = addProductDetail.Description,
                    AdditionalInformation = addProductDetail.AdditionalInformation,
                    ShippingAndReturns = addProductDetail.ShippingAndReturns,
                    SizeChart = addProductDetail.SizeChart,
                    Reviews = addProductDetail.Reviews,
                    Questions = addProductDetail.Questions,
                    VendorInfo = addProductDetail.VendorInfo,
                    MoreProducts = addProductDetail.MoreProducts,
                    ProductPolicies = addProductDetail.ProductPolicies
                };

                _context.ProductDetails.Add(productDetail);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Product detail added successfully", DataObject = productDetail };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while adding the product detail" };
            }
        }

        // Delete ProductDetail
        public async Task<ResponeModel> DeleteProductDetail(int productDetailId)
        {
            try
            {
                var productDetail = await _context.ProductDetails.FindAsync(productDetailId);

                if (productDetail == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Product detail not found" };
                }

                _context.ProductDetails.Remove(productDetail);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Product detail deleted successfully" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while deleting the product detail" };
            }
        }

        // Update ProductDetail
        public async Task<ResponeModel> UpdateProductDetail(UpdateProductDetailDTO updateProductDetail)
        {
            try
            {
                var productDetail = await _context.ProductDetails.FindAsync(updateProductDetail.ProductDetailId);

                if (productDetail == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Product detail not found" };
                }

                productDetail.Description = updateProductDetail.Description;
                productDetail.AdditionalInformation = updateProductDetail.AdditionalInformation;
                productDetail.ShippingAndReturns = updateProductDetail.ShippingAndReturns;
                productDetail.SizeChart = updateProductDetail.SizeChart;
                productDetail.Reviews = updateProductDetail.Reviews;
                productDetail.Questions = updateProductDetail.Questions;
                productDetail.VendorInfo = updateProductDetail.VendorInfo;
                productDetail.MoreProducts = updateProductDetail.MoreProducts;
                productDetail.ProductPolicies = updateProductDetail.ProductPolicies;

                _context.ProductDetails.Update(productDetail);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Product detail updated successfully", DataObject = productDetail };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updating the product detail" };
            }
        }
    }
}