using EXE201.DAL.DTOs;
using EXE201.DAL.DTOs.UserDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using LMSystem.Repository.Helpers;
using MCC.DAL.Repository.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EXE201Context context) : base(context)
        {
        }

        public async Task<User> AddNewUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetLatestUser()
        {
            return await _context.Users.OrderByDescending(x => x.UserId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AllProfileUser>> GetAllUsers()
        {
            return await _context.Users
                // .Include(x => x.Carts)
                // .Include(x => x.Deposits)
                // .Include(x => x.Feedbacks)
                // .Include(x => x.Memberships)
                // .Include(x => x.Notifications)
                // .Include(x => x.Payments)
                // .Include(x => x.RentalOrders)
                // .Include(x => x.Ratings)
                .Include(x => x.Roles)
                .Select(x => new AllProfileUser
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    FullName = x.FullName,
                    Password = x.Password,
                    Phone = x.Phone,
                    Gender = x.Gender,
                    DateOfBirth = x.DateOfBirth,
                    Email = x.Email,
                    ProfileImage = x.ProfileImage,
                    UserStatus = x.UserStatus,
                    Roles = x.Roles.Select(r => r.RoleName).ToList()
                })
                .OrderByDescending(x => x.UserId)
                .ToListAsync();
        }


        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users
                .Include(x => x.Roles)
                .FirstAsync(x => x.UserId == userId);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User> UpdateUser(User user)
        {
            var existUser = await _context.Users.Where(x => x.UserId.Equals(user.UserId)).FirstOrDefaultAsync();
            if (existUser != null)
            {
                existUser.UserId = user.UserId;
                existUser.UserName = user.UserName;
                existUser.Password = user.Password;
                existUser.FullName = user.FullName;
                existUser.Phone = user.Phone;
                existUser.Gender = user.Gender;
                existUser.Email = user.Email;
                existUser.DateOfBirth = user.DateOfBirth;
                existUser.ProfileImage = user.ProfileImage;
                existUser.UserStatus = user.UserStatus;
                existUser.Deposits = user.Deposits;
                existUser.Carts = user.Carts;
                existUser.Feedbacks = user.Feedbacks;
                //existUser.Memberships = user.Memberships;
                existUser.Roles = user.Roles;
                existUser.Notifications = user.Notifications;
                existUser.Payments = user.Payments;
                existUser.Ratings = user.Ratings;
                existUser.RentalOrders = user.RentalOrders;

                _context.Users.Update(existUser);
                await _context.SaveChangesAsync();
                return existUser;
            }

            return null;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task<PagedResponseDTO<UserListDTO>> GetFilteredUser(UserFilterDTO filter)
        {
            var query = _context.Users
                .Include(u => u.Roles)
                //.Include(u => u.Memberships)
                //.ThenInclude(m => m.MembershipType)
                .Select(u => new UserListDTO
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Password = u.Password,
                    Phone = u.Phone,
                    Gender = u.Gender,
                    DateOfBirth = u.DateOfBirth,
                    Address = u.Address,
                    Email = u.Email,
                    ProfileImage = u.ProfileImage,
                    Roles = u.Roles.Select(r => r.RoleName).ToList(),
                    AccountStatus = u.UserStatus,
                    //MembershipTypeName = u.Memberships.FirstOrDefault().MembershipType.MembershipTypeName
                })
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(u => u.UserName.Contains(filter.Search) || u.FullName.Contains(filter.Search));
            }

            if (filter.DateOfBirth.HasValue)
            {
                query = query.Where(u => u.DateOfBirth == filter.DateOfBirth);
            }

            if (filter.Gender.HasValue)
            {
                query = query.Where(u => u.Gender == filter.Gender);
            }

            if (!string.IsNullOrEmpty(filter.MembershipTypeName))
            {
                query = query.Where(u => u.MembershipTypeName == filter.MembershipTypeName);
            }

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "username":
                        query = filter.Sort ? query.OrderByDescending(u => u.UserName) : query.OrderBy(u => u.UserName);
                        break;
                    case "fullname":
                        query = filter.Sort ? query.OrderByDescending(u => u.FullName) : query.OrderBy(u => u.FullName);
                        break;
                    case "dateofbirth":
                        query = filter.Sort
                            ? query.OrderByDescending(u => u.DateOfBirth)
                            : query.OrderBy(u => u.DateOfBirth);
                        break;
                    case "membershiptypename":
                        query = filter.Sort
                            ? query.OrderByDescending(u => u.MembershipTypeName)
                            : query.OrderBy(u => u.MembershipTypeName);
                        break;
                    default:
                        query = query.OrderBy(u => u.UserId); // Default sort order
                        break;
                }
            }
            else
            {
                query = query.OrderBy(u => u.UserId); // Default sort order
            }

            var totalCount = await query.CountAsync();
            var users = await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

            var pagedResponse = new PagedResponseDTO<UserListDTO>
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = totalCount,
                Items = users
            };

            return pagedResponse;
        }


        public async Task<UserProfileDTO> GetUserProfile(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                //.Include(u => u.Memberships)
                //.ThenInclude(m => m.MembershipType)
                .Where(u => u.UserId == userId)
                .Select(u => new UserProfileDTO
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Phone = u.Phone,
                    Gender = u.Gender,
                    DateOfBirth = u.DateOfBirth,
                    Email = u.Email,
                    Address = u.Address,
                    ProfileImage = u.ProfileImage,
                    AccountStatus = u.UserStatus,
                    Roles = string.Join(", ", u.Roles.Select(r => r.RoleName)),
                    //MembershipTypeName = u.Memberships.FirstOrDefault().MembershipType.MembershipTypeName
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<Token> GetRefreshTokenByUserId(string userId)
        {
            return await _context.Tokens.FirstOrDefaultAsync(t =>
                t.UserId.ToString() == userId && t.Status == "Active");
        }

        public async Task UpdateToken(Token token)
        {
            _context.Tokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTotalUsersByRole(string roleName)
        {
            return await _context.Users
                .Where(u => u.Roles.Any(r => r.RoleName == roleName))
                .CountAsync();
        }
    }
}