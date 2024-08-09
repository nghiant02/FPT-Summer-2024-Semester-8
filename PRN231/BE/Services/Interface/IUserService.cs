using BusinessObjects.Dto;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;

namespace Services.Interface
{
    public interface IUserService
    {
        public Task<User?> Login(LoginDto loginDto);
        Task<User?> Logout(string userId);
        public Task<IEnumerable<UserResponseDto?>?> GetUsers();
        public Task<bool> IsUser(LoginDto loginDto);
        public Task<int> AddUser(UserDto userDto);
        public Task<int> UpdateUser(string id, UserDto userDto);
        public Task<UserResponseDto?> GetUserById(string id);
        public Task<int> DeleteUser(string id);
        public Task<bool> UpdateCounterByUserId(string userId, string counterId);
        public Task<Customer> LoginCustomer(CustomerLoginDto customerLoginDto);
    }
}
