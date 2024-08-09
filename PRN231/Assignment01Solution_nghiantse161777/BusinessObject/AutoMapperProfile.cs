using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;

namespace BusinessObject
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<MemberCreateUpdateDTO, Member>().ReverseMap();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ReverseMap();
            CreateMap<ProductCreateUpdateDTO, Product>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderCreateUpdateDTO, Order>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ReverseMap();
            CreateMap<OrderDetailCreateUpdateDTO, OrderDetail>().ReverseMap();
        }
    }
}
