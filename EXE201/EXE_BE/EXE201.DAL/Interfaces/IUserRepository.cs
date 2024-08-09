using EXE201.DAL.DTOs;
using EXE201.DAL.DTOs.UserDTOs;
using EXE201.DAL.Models;
using LMSystem.Repository.Helpers;
using MCC.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserById(int userId);
        Task<IEnumerable<AllProfileUser>> GetAllUsers();
        Task<User> UpdateUser(User user);
        Task<User> AddNewUser(User user);
        Task<User> GetLatestUser();
        Task<User> GetUserByEmail(string email);
        Task<Role> GetRoleById(int roleId);
        Task<PagedResponseDTO<UserListDTO>> GetFilteredUser(UserFilterDTO filter);
        Task<UserProfileDTO> GetUserProfile(int userId);
        Task UpdateToken(Token token);
        Task<Token> GetRefreshTokenByUserId(string userId);
        Task<int> GetTotalUsersByRole(string roleName);


    }
}
