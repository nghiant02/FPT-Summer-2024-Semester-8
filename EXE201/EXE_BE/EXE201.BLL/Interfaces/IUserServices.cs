using EXE201.BLL.DTOs.UserDTOs;
using EXE201.DAL.DTOs;
using EXE201.DAL.DTOs.UserDTOs;
using EXE201.DAL.Models;
using LMSystem.Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Interfaces
{
    public interface IUserServices
    {
        Task<LoginResponseDTOs> Login(string username, string password);
        Task<IEnumerable<AllProfileUser>> GetAllProfileUser();
        Task<bool> ChangeStatusUserToNotActive(int userId);
        Task<User> AddUserForStaff(AddNewUserDTO addNewUserDTO);
        Task<User> ChangePasword(int id, ChangePasswordDTO changePasswordDTO);
        Task<User> FindUserByEmail(string email);
        Task<User> UpdatePassword(string email, string password, int id);
        Task<User> UserUpdateUser(int id, UpdateProfileUserDTO userView);
        Task<PagedResponseDTO<UserListDTO>> GetFilteredUser(UserFilterDTO filter);
        Task<UserProfileDTO> GetUserProfile(int userId);
        Task<(bool Success, int UserId)> RegisterUserAsync(RegisterUserRequest request);
        Task<bool> VerifyEmailWithCodeAsync(int userId, string code);
        Task<(string Token, string RefreshToken)> RefreshTokenAsync(string token, string refreshToken);
        Task<User> UserUpdateAvartar(int id, UpdateAvatarUserDTO updateAvatarUserDTO);
        Task<LoginResponseDTOs> GoogleAuthorizeUser(GoogleUserDto googleUser);
    }
}