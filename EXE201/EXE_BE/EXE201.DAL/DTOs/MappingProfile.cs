using AutoMapper;
using EXE201.BLL.DTOs.UserDTOs;
using EXE201.DAL.DTOs.CartDTOs;
using EXE201.DAL.DTOs.CategoryDTOs;
using EXE201.DAL.DTOs.NotificationDTOs;
using EXE201.DAL.DTOs.FeedbackDTOs;
using EXE201.DAL.DTOs.ProductDTOs;
using EXE201.DAL.DTOs.UserDTOs;
using EXE201.DAL.Models;

namespace EXE201.BLL.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            UserMappingProfile();
        }

        private void UserMappingProfile()
        {
            CreateMap<User, GetUserDTOs>();
            CreateMap<AddNewUserDTO, User>();
            CreateMap<UpdateProfileUserDTO, User>();
            CreateMap<UpdateCategoryDTOs, Category>();
            CreateMap<AddCategoryDTOs, Category>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationSendDto, Notification>();
            CreateMap<AddNewCartDTO, Cart>();
            CreateMap<AddRatingDTO, Rating>();
            CreateMap<UpdateAvatarUserDTO, User>();
            CreateMap<ViewCartDto, Cart>();
            CreateMap<UpdateCartDto, Cart>();
            CreateMap<UpdateCartDto, RentalOrderDetail>();
        }
    }
}
