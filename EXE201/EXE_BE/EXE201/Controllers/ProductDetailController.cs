using EXE201.BLL.Interfaces;
using EXE201.BLL.Services;
using EXE201.DAL.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;

namespace EXE201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailServices _productDetailServices;

        public ProductDetailController(IProductDetailServices productDetailServices)
        {
            _productDetailServices = productDetailServices;
        }

        [HttpGet("ViewProductDetailByProductId")]
        public async Task<IActionResult> GetProductDetailByProductId([FromQuery] int productId)
        {
            var productDetail = await _productDetailServices.GetProductDetailByProductId(productId);

            if (productDetail == null)
            {
                return NotFound(new { Message = "Product detail not found" });
            }

            var productDetailView = new ProductDetailViewDTO
            {
                Description = productDetail.Description?.Split("\n").ToArray(),
                AdditionalInformation = productDetail.AdditionalInformation?.Split("\n").ToArray(),
                ShippingAndReturns = productDetail.ShippingAndReturns?.Split("\n").ToArray(),
                SizeChart = productDetail.SizeChart?.Split("\n").ToArray(),
                Reviews = productDetail.Reviews?.Split("\n").ToArray(),
                Questions = productDetail.Questions?.Split("\n").ToArray(),
                VendorInfo = productDetail.VendorInfo?.Split("\n").ToArray(),
                MoreProducts = productDetail.MoreProducts?.Split("\n").ToArray(),
                ProductPolicies = productDetail.ProductPolicies?.Split("\n").ToArray()
            };

            return Ok(productDetailView);
        }

        [HttpPost("AddProductDetail")]
        public async Task<IActionResult> AddProductDetail([FromBody] AddProductDetailDTO addProductDetailDTO)
        {
            var response = await _productDetailServices.AddProductDetail(addProductDetailDTO);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPost("DeleteProductDetail")]
        public async Task<IActionResult> DeleteProductDetail([FromQuery] int productId)
        {
            var response = await _productDetailServices.DeleteProductDetail(productId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPost("UpdateProductDetail")]
        public async Task<IActionResult> UpdateProductDetail([FromBody] UpdateProductDetailDTO updateProductDetailDTO)
        {
            var response = await _productDetailServices.UpdateProductDetail(updateProductDetailDTO);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }
    }
}
