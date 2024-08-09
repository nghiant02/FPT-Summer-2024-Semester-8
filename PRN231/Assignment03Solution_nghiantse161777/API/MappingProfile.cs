using AutoMapper;
using BussiniseObject.Dto;
using BussiniseObject.Models;

namespace API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ResponseDto to Model
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
        // RequestDto to Model

    }
}