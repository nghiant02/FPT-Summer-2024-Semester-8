using AutoMapper;
using BusinessObjects.Dto;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implementation;

public class UserService(IUserRepository userRepository, ICounterRepository counterRepository, IMapper mapper)
    : IUserService
{
    private IUserRepository UserRepository { get; } = userRepository;
    private ICounterRepository CounterRepository { get; } = counterRepository;
    private IMapper Mapper { get; } = mapper;

    public async Task<User?> Login(LoginDto loginDto)
    {
        var user = await UserRepository.GetUser(loginDto.Email ?? "", loginDto.Password ?? "");
        if (loginDto.CounterId != null)
        { 
            await UserRepository.AssignCounterToUser(user.UserId, loginDto.CounterId);
        }
        return user ?? null;
    }

    public async Task<User?> Logout(string userId)
    {
        var user = await UserRepository.GetUserById(userId);
        await UserRepository.ReleaseCounterFromUser(user);
        return user ?? null;
    }

    public async Task<bool> UpdateCounterByUserId(string userId, string counterId)
    {
        return await userRepository.UpdateCounterByUserId(userId, counterId);
    }
    public async Task<IEnumerable<UserResponseDto>?> GetUsers()
    {
        var users = await UserRepository.Gets();
        var userResponseDtos = Mapper.Map<IEnumerable<UserResponseDto>>(users);
        foreach (var userResponseDto in userResponseDtos)
        {
            var user = await UserRepository.GetById(userResponseDto.UserId);
            userResponseDto.CounterNumber = user?.Counter?.Number;
            userResponseDto.RoleName = user?.Role?.RoleName;
        }

        return userResponseDtos;
    }

    public async Task<bool> IsUser(LoginDto loginDto)
    {
        var users = await UserRepository.Find(a => a.Email == loginDto.Email && a.Password == loginDto.Password);
        return users.Any();
    }

    public async Task<int> UpdateUser(string id, UserDto userDto)
    {
        var user = Mapper.Map<User>(userDto);
        return await UserRepository.Update(id, user);
    }

    public async Task<int> AddUser(UserDto userDto)
    {
        var user = Mapper.Map<User>(userDto);
        return await UserRepository.Create(user);
    }

    public async Task<UserResponseDto?> GetUserById(string id)
    {
        var user = await UserRepository.GetById(id);
        var userResponseDto = Mapper.Map<UserResponseDto>(user);
        userResponseDto.CounterNumber = user?.Counter?.Number;
        userResponseDto.RoleName = user?.Role?.RoleName;
        return userResponseDto;
    }

    public Task<int> DeleteUser(string id)
    {
        return UserRepository.Delete(id);
    }
    public Task<Customer> LoginCustomer(CustomerLoginDto customerLoginDto)
    {
        var customer = Mapper.Map<Customer>(customerLoginDto);
        return UserRepository.GetCustomer(customer);
    }   
}