using AutoMapper;
using BusinessObjects.Dto;
using BusinessObjects.Dto.BillReqRes;
using BusinessObjects.Dto.Jewelry;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;

namespace API.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // RequestDto Mapping
        CreateMap<Jewelry, JewelryRequestDto>().ReverseMap();

        // Dto Mapping
        CreateMap<Warranty, WarrantyDto>().ReverseMap();
        CreateMap<JewelryType, JewelryTypeDto>().ReverseMap();
        CreateMap<Promotion, PromotionDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Customer, CustomerLoginDto>().ReverseMap();
        CreateMap<Customer, CustomerRegisterDto>().ReverseMap();
        // ResponseDto Mapping
        CreateMap<Gold, GoldPriceResponseDto>().ReverseMap();
        CreateMap<Gem, GemPriceResponseDto>().ReverseMap();
        CreateMap<Customer, CustomerResponseDto>();
        CreateMap<User, UserResponseDto>();
        CreateMap<Jewelry, JewelryResponseDto>().ReverseMap();
        // MongoDB Mapping
        CreateMap<BillResponseDto, BillDetailDto>().ReverseMap();
        // Paging Mapping
    }
}