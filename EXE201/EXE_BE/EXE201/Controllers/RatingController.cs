using EXE201.BLL.Interfaces;
using EXE201.BLL.Services;
using EXE201.DAL.DTOs.FeedbackDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXE201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingServices _ratingServices;
        public RatingController(IRatingServices ratingServices)
        {
            _ratingServices = ratingServices;
        }

        [HttpGet("GetAllRatings")]
        public async Task<IActionResult> Get()
        {
            var result = await _ratingServices.GetRatings();
            return Ok(result);
        }

        [HttpPost("AddRating")]
        public async Task<IActionResult> addRating(AddRatingDTO addRatingDTO)
        {
            var result = await _ratingServices.AddRating(addRatingDTO);
            return Ok(result);
        }

        [HttpGet("GetAllRatingOfUser/{userId}")]
        public async Task<IActionResult> GetUserRatingsAndFeedback(int userId)
        {
            var ratings = await _ratingServices.GetUserRatingsAndFeedback(userId);
            if (ratings == null || !ratings.Any())
            {
                return NotFound();
            }
            return Ok(ratings);
        }

        [HttpGet("GetProductRatingsAndFeedback")]
        public async Task<IActionResult> GetProductRatingsAndFeedback([FromQuery] int productId, [FromQuery] int? ratingFilter)
        {
            // Log the received parameters
            Console.WriteLine($"Received productId: {productId}, ratingFilter: {ratingFilter}");

            if (!ratingFilter.HasValue)
            {
                ratingFilter = null;
            }

            var productRatings = await _ratingServices.GetProductRatingsAndFeedback(productId, ratingFilter);

            if (productRatings == null)
            {
                return NotFound(new { Message = "Product ratings and feedback not found" });
            }

            return Ok(productRatings);
        }

        [HttpGet("AllProductsWithRatingsFeedback")]
        public async Task<IActionResult> GetAllProductsWithRatingsFeedback()
        {
            var products = await _ratingServices.GetAllProductsWithRatingsFeedback();
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }
    }
}
