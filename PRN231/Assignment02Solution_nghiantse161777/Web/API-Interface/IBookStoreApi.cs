using BusinessObject.Dto.RequestDto;
using BusinessObject.Dto.ResponseDto;
using BusinessObject.Dto.ResponseDto.ResponseDto;
using BusinessObject.Models;
using Refit;

namespace Web.API_Interface;

public interface IBookStoreApi
{
    [Get("/api/Test")]
    Task<IEnumerable<Author>> Test();
    // Auth API
    [Post("/api/Auth/login")]
    Task<UserResponseDto> Login([Body] LoginDto loginDto);
    [Post("/api/Auth/register")]
    Task<ResponseBaseDto> Register([Body] RegisterDto registerDto);
    // Author API
    [Get("/api/Author/GetAuthors")]
    Task<IEnumerable<AuthorResponseDto>> GetAuthors();
    [Get("/api/Author/GetAuthorById/{authorId}")]
    Task<AuthorResponseDto> GetAuthorById(string authorId);
    [Post("/api/Author/CreateAuthor")]
    Task<ResponseBaseDto> CreateAuthor([Body] AuthorRequestDto authorRequestDto);
    [Delete("/api/Author/DeleteAuthor/{authorId}")]
    Task<int> DeleteAuthor(string authorId);
    [Put("/api/Author/UpdateAuthor/{authorId}")]
    Task<bool> UpdateAuthor(string authorId, [Body] AuthorUpdateRequestDto authorUpdateRequestDto);
    // Publisher API
    [Get("/api/Publisher/GetPublishers")]
    Task<IEnumerable<PublisherResponseDto>> GetPublishers();
    [Get("/api/Publisher/GetPublisherById/{publisherId}")]
    Task<PublisherResponseDto> GetPublisherById(string publisherId);
    [Post("/api/Publisher/CreatePublisher")]
    Task<ResponseBaseDto> CreatePublisher([Body] PublisherRequestDto publisherRequestDto);
    [Delete("/api/Publisher/DeletePublisher/{publisherId}")]
    Task<bool> DeletePublisher(string publisherId);
    [Put("/api/Publisher/UpdatePublisher/{publisherId}")]
    Task<bool> UpdatePublisher(string publisherId, [Body] PublisherRequestDto publisherRequestDto);
}