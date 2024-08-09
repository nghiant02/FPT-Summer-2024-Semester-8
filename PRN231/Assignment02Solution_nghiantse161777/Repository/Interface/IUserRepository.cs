using BusinessObject.Dto.RequestDto;
using BusinessObject.Dto.ResponseDto;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        Task<ResponseBaseDto?> Login(LoginDto loginDto);
        Task<ResponseBaseDto?> Register(RegisterDto registerDto);
    }
}
