using EXE201.DAL.DTOs.FeedbackDTOs;
using EXE201.DAL.DTOs.ProductDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Implements;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.Repository
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(EXE201Context context) : base(context)
        {
        }

        public async Task<IEnumerable<Rating>> GetRatings()
        {
            try
            {
                var ratingList = await _context.Ratings
                    .Include(x => x.User)
                    .Include(x => x.Product)
                    .Include(x => x.Feedback)
                    .ToListAsync();

                return ratingList;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserRatingFeedbackDTO>> GetUserRatingsAndFeedback(int userId)
        {
            var ratings = await _context.Ratings
                .Include(r => r.Feedback)
                .Include(r => r.Product)
                .Where(r => r.UserId == userId)
                .Select(r => new UserRatingFeedbackDTO
                {
                    RatingId = r.RatingId,
                    ProductId = r.Product.ProductId,
                    ProductName = r.Product.ProductName,
                    RatingValue = r.RatingValue ?? 0,
                    DateGiven = r.DateGiven,
                    FeedbackComment = r.Feedback.FeedbackComment,
                    FeedbackImage = r.Feedback.FeedbackImage
                })
                .ToListAsync();

            return ratings;
        }

        public async Task<ProductRatingFeedbackResponseDTO> GetProductRatingsAndFeedback(int productId, int? ratingFilter)
        {
            var ratingsQuery = _context.Ratings
                .Include(r => r.Feedback)
                .Include(r => r.User)
                .Where(r => r.ProductId == productId);

            if (ratingFilter.HasValue)
            {
                ratingsQuery = ratingsQuery.Where(r => r.RatingValue == ratingFilter.Value);
            }

            var ratings = await ratingsQuery.Select(r => new ProductRatingFeedbackDTO
            {
                RatingId = r.RatingId,
                UserId = r.User.UserId,
                UserName = r.User.UserName,
                RatingValue = r.RatingValue ?? 0,
                DateGiven = r.DateGiven,
                FeedbackComment = r.Feedback.FeedbackComment,
                FeedbackImage = r.Feedback.FeedbackImage
            }).ToListAsync();

            var averageRating = ratings.Any() ? ratings.Average(r => r.RatingValue) : 0;
            var totalRatings = ratings.Count;

            return new ProductRatingFeedbackResponseDTO
            {
                ProductId = productId,
                ProductName = (await _context.Products.FindAsync(productId))?.ProductName,
                AverageRating = averageRating,
                TotalRatings = totalRatings,
                Ratings = ratings
            };
        }

        public async Task<IEnumerable<ProductDetailDTO>> GetAllProductsWithRatingsFeedback()
        {
            var products = await _context.Products
                .Include(p => p.Ratings)
                    .ThenInclude(r => r.User)
                .Include(p => p.Ratings)
                    .ThenInclude(r => r.Feedback)
                .Include(p => p.Category)
                .Select(p => new ProductDetailDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    //ProductImage = p.ProductImage,
                    ProductPrice = p.ProductPrice,
                    //ProductSize = p.ProductSize,
                    //ProductColor = p.ProductColor,
                    ProductStatus = p.ProductStatus,
                    CategoryName = p.Category.CategoryName,
                    AverageRating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue ?? 0) : 0,
                    RatingsFeedback = p.Ratings.Select(r => new RatingFeedbackDTO
                    {
                        RatingId = r.RatingId,
                        UserId = r.User.UserId,
                        UserName = r.User.UserName,
                        RatingValue = r.RatingValue ?? 0,
                        DateGiven = r.DateGiven,
                        FeedbackComment = r.Feedback.FeedbackComment,
                        FeedbackImage = r.Feedback.FeedbackImage
                    }).ToList()
                })
                .ToListAsync();

            return products;
        }
    }
}
