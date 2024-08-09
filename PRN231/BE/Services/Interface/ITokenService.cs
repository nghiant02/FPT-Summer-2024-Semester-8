using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;

namespace Services.Interface;

public interface ITokenService
{
    Task<TokenResponseDto> CreateToken(User user);
    Task<TokenResponseDto> CreateToken(Customer customer);
}