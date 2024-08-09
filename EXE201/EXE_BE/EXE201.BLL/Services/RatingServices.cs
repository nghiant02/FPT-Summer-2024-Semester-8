using AutoMapper;
using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs.FeedbackDTOs;
using EXE201.DAL.DTOs.ProductDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using EXE201.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Services
{
    public class RatingServices : IRatingServices
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public RatingServices(IRatingRepository ratingRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public async Task<AddRatingDTO> AddRating(AddRatingDTO addRatingDTO)
        {
            try
            {
                var rating = new Rating
                {
                    UserId = addRatingDTO.UserId,
                    ProductId = addRatingDTO.ProductId,
                    RatingValue = addRatingDTO.RatingValue,
                    DateGiven = addRatingDTO.DateGiven
                };

                if (!string.IsNullOrEmpty(addRatingDTO.FeedbackComment) || !string.IsNullOrEmpty(addRatingDTO.FeedbackImage))
                {
                    var feedback = new Feedback
                    {
                        UserId = addRatingDTO.UserId,
                        ProductId = addRatingDTO.ProductId,
                        FeedbackComment = addRatingDTO.FeedbackComment,
                        FeedbackImage = addRatingDTO.FeedbackImage,
                        DateGiven = addRatingDTO.DateGiven,
                        FeedbackStatus = "Pending"
                    };
                    rating.Feedback = feedback;
                }

                await _ratingRepository.AddAsync(rating);
                await _ratingRepository.SaveChangesAsync();

                var result = new AddRatingDTO
                {
                    UserId = rating.UserId.Value,
                    ProductId = rating.ProductId.Value,
                    RatingValue = rating.RatingValue.Value,
                    DateGiven = rating.DateGiven.Value,
                    FeedbackComment = rating.Feedback?.FeedbackComment,
                    FeedbackImage = rating.Feedback?.FeedbackImage
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductRatingFeedbackResponseDTO> GetProductRatingsAndFeedback(int productId, int? ratingFilter)
        {
            return await _ratingRepository.GetProductRatingsAndFeedback(productId, ratingFilter);
        }

        public async Task<IEnumerable<Rating>> GetRatings()
        {
            return await _ratingRepository.GetRatings();
        }

        public async Task<IEnumerable<UserRatingFeedbackDTO>> GetUserRatingsAndFeedback(int userId)
        {
            return await _ratingRepository.GetUserRatingsAndFeedback(userId);
        }

        public async Task<IEnumerable<ProductDetailDTO>> GetAllProductsWithRatingsFeedback()
        {
            return await _ratingRepository.GetAllProductsWithRatingsFeedback();
        }
    }
}
