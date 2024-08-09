using EXE201.BLL.Interfaces;
using EXE201.BLL.Services;
using EXE201.DAL.DTOs;
using EXE201.DAL.DTOs.ProductDTOs;
using EXE201.DAL.Models;
using EXE201.ViewModel.UserViewModel;
using LMSystem.Repository.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EXE201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var product = await _productServices.GetAll();
            return Ok(product);
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById([FromQuery] int productId)
        {
            var response = await _productServices.GetById(productId);
            if (response.ProductStatus == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDTO addProductDTO)
        {
            var response = await _productServices.AddProduct(addProductDTO);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpDelete("PermanentDeleteProduct/{productId}")]
        public async Task<IActionResult> PermanentDeleteProduct(int productId)
        {
            var result = await _productServices.PermanentDeleteProduct(productId);
            if (result.Status == "Success")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int productId)
        {
            var response = await _productServices.DeleteProduct(productId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPost("RecoverProduct")]
        public async Task<IActionResult> RecoverProduct([FromQuery] int productId)
        {
            var response = await _productServices.RecoverProduct(productId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromQuery] UpdateProductDTO updateProductDTO)
        {
            var response = await _productServices.UpdateProduct(updateProductDTO);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("PagingAndFilteredProducts")]
        public async Task<IActionResult> GetFilteredProducts([FromQuery] ProductFilterDTO filter)
        {
            var products = await _productServices.GetFilteredProducts(filter);
            return Ok(products);
        }

        [HttpGet("RecommendHot")]
        public async Task<IActionResult> RecommendHot(int topN = 10)
        {
            var products = await _productServices.GetHotProducts(topN);
            return Ok(products);
        }

        [HttpGet("RecommendNew")]
        public async Task<IActionResult> RecommendNew(int topN = 10)
        {
            var products = await _productServices.GetNewProducts(topN);
            return Ok(products);
        }

        [HttpGet("RecommendHighlyRated")]
        public async Task<IActionResult> RecommendHighlyRated(int topN = 10)
        {
            var products = await _productServices.GetHighlyRatedProducts(topN);
            return Ok(products);
        }

        [HttpGet("RecommendByCategory/{productId}")]
        public async Task<IActionResult> GetProductRecommendationsByCategory(int productId, [FromQuery] ProductPagingRecommendByCategoryDTO filter)
        {
            var result = await _productServices.GetProductRecommendationsByCategory(productId, filter);

            if (!result.Items.Any())
            {
                return NotFound(new { Message = "No recommendations found" });
            }

            return Ok(result);
        }

        [HttpGet("SuggestionsForSearch")]
        public async Task<IActionResult> GetProductSuggestions([FromQuery] string searchTerm)
        {
            var suggestions = await _productServices.GetProductSuggestions(searchTerm);

            if (!suggestions.Any())
            {
                return NotFound(new { Message = "No product suggestions found" });
            }

            return Ok(suggestions);
        }

        [HttpPost("AddColorToProduct")]
        public async Task<IActionResult> AddColorToProduct([FromQuery] int productId, [FromQuery] int colorId, [FromQuery] string ProductColorImage)
        {
            var response = await _productServices.AddColorToProduct(productId, colorId, ProductColorImage);
            if (response.Status == "Error")
            {
                return BadRequest(new { Message = response.Message });
            }
            return Ok(new { Message = response.Message });
        }

        [HttpDelete("DeleteColorFromProduct")]
        public async Task<IActionResult> DeleteColorFromProduct([FromQuery] int productId, [FromQuery] int colorId)
        {
            var response = await _productServices.DeleteColorFromProduct(productId, colorId);
            if (response.Status == "Error")
            {
                return BadRequest(new { Message = response.Message });
            }
            return Ok(new { Message = response.Message });
        }

        [HttpPost("AddSizeToProduct")]
        public async Task<IActionResult> AddSizeToProduct([FromQuery] int productId, [FromQuery] int sizeId)
        {
            var response = await _productServices.AddSizeToProduct(productId, sizeId);
            if (response.Status == "Error")
            {
                return BadRequest(new { Message = response.Message });
            }
            return Ok(new { Message = response.Message });
        }

        [HttpDelete("DeleteSizeFromProduct")]
        public async Task<IActionResult> DeleteSizeFromProduct([FromQuery] int productId, [FromQuery] int sizeId)
        {
            var response = await _productServices.DeleteSizeFromProduct(productId, sizeId);
            if (response.Status == "Error")
            {
                return BadRequest(new { Message = response.Message });
            }
            return Ok(new { Message = response.Message });
        }

        [HttpPost("UpdateColorImage")]
        public async Task<IActionResult> UpdateColorImageForProduct([FromBody] UpdateColorImageDTO updateColorImageDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _productServices.UpdateColorImageForProduct(
                updateColorImageDTO.ProductId,
                updateColorImageDTO.ColorId,
                updateColorImageDTO.NewColorImage);

            if (response.Status == "Success")
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
