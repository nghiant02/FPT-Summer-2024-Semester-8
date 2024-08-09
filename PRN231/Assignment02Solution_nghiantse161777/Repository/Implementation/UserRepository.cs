using AutoMapper;
using BusinessObject.Dto.RequestDto;
using BusinessObject.Dto.ResponseDto;
using BusinessObject.Dto.ResponseDto.ResponseDto;
using BusinessObject.Models;
using Dao;
using Repository.Interface;

namespace Repository.Implementation;

public class UserRepository : IUserRepository
{
    private readonly UserDao? _userDao;
    private readonly IMapper? _mapper;

    public UserRepository(IMapper mapper)
    {
        _userDao = new UserDao();
        _mapper = mapper;
    }

    public async Task<ResponseBaseDto?> Login(LoginDto loginDto)
    {
        if (_userDao == null || _mapper == null) throw new InvalidOperationException("BE Error");
        var data = await _userDao.Login(loginDto);
        var loginResponse = _mapper.Map<UserResponseDto>(data);
        bool isSuccess = data != null;
        string message = isSuccess ? "Login successful!" : "Login failed!";
        return new ResponseBaseDto { Data = loginResponse, IsSuccess = isSuccess, Message = message };
    }

    public async Task<ResponseBaseDto?> Register(RegisterDto registerDto)
    {
        if (_userDao == null || _mapper == null)
        {
            throw new InvalidOperationException("BE Error");
        }

        // Map DTO to User entity
        var user = _mapper.Map<User>(registerDto);
        var isDuplicateEmail = await _userDao.CheckDupicateEmail(user.EmailAddress);
        if (isDuplicateEmail) {
            return new ResponseBaseDto
            {
                IsSuccess = false,
                Message = "Email already exists",
                Data = null
            };
        }
        // Attempt to register user
        var registeredUser = await _userDao.Register(user);

        if (registeredUser == null)
        {
            return new ResponseBaseDto
            {
                IsSuccess = false,
                Message = "Registration failed",
                Data = null
            };
        }

        // Registration successful
        return new ResponseBaseDto
        {
            IsSuccess = true,
            Message = "Registration successful",
            Data = registeredUser  // Assuming registeredUser is the response from _userDao.Register
        };
    }

}
