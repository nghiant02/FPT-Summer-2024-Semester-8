using AutoMapper;
using BusinessObject.Dto.RequestDto;
using BusinessObject.Dto.ResponseDto;
using BusinessObject.Dto.ResponseDto.ResponseDto;
using BusinessObject.Models;

namespace API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ResponseDto to Model
        CreateMap<UserResponseDto, User>().ReverseMap();
        CreateMap<AuthorResponseDto, Author>().ReverseMap();
        CreateMap<PublisherResponseDto, Publisher>().ReverseMap();
        // RequestDto to Model
        CreateMap<RegisterDto, User>().ReverseMap();
        CreateMap<AuthorRequestDto, Author>().ReverseMap();
        CreateMap<AuthorUpdateRequestDto, Author>().ReverseMap();
        CreateMap<PublisherRequestDto, Publisher>().ReverseMap();
    }
}
