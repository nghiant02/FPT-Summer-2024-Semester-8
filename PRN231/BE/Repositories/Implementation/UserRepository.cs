using BusinessObjects.DTO;
using BusinessObjects.Models;
using DAO;
using DAO.Dao;
using Repositories.Interface;
using Tools;

namespace Repositories.Implementation
{

    public class UserRepository(UserDao userDao, RoleDao roleDao, CounterDao counterDao, CustomerDao customerDao) : IUserRepository
    {
        public UserDao UserDao { get; } = userDao;
        public RoleDao RoleDao { get; } = roleDao;
        public CounterDao CounterDao { get; } = counterDao;
        public CustomerDao CustomerDao { get; } = customerDao;

        public Task<IEnumerable<User>> Find(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUser(string email, string password)
        {
            var user = await UserDao.GetUser(email, password);
            if (string.IsNullOrEmpty(user.RoleId))
            {
                throw new InvalidOperationException("User roleId not found");
            }
            return user;
        }

        public async Task<bool> UpdateCounterByUserId(string userId, string counterId)
        {
            return await UserDao.UpdateCounterByUserId(userId, counterId);
        }

        public async Task<User?> GetById(string id)
        {
            var user = await UserDao.GetUserById(id);
            if (user == null) return null;
            var role = await RoleDao.GetRoleById(user.RoleId);
            var counter = await CounterDao.GetCounterByIdv2(user.CounterId);
            user.Role = role;
            user.Counter = counter;
            return user;
        }

        public async Task<int> Update(string id, User entity)
        {
            return await UserDao.UpdateUser(id, entity);
        }

        public async Task<IEnumerable<User>?> Gets()
        {
            var users = await UserDao.GetUsers();
            if (users == null) return null;
            foreach (var user in users)
            {
                var userRole = await RoleDao.GetRoleById(user.RoleId);
                var counter = await CounterDao.GetCounterByIdv2(user.CounterId);
                user.Role = userRole;
                user.Counter = counter;
            }

            return users;
        }

        public async Task<int> Create(User entity)
        {
            return await UserDao.CreateUser(entity);
        }

        public async Task<int> Delete(string id)
        {
            return await UserDao.DeleteUser(id);
        }

        public async Task<User?> GetUserById(string id)
        {
            return await UserDao.GetUserById(id);
        }

        public async Task<IEnumerable<string>> GetAvailableCounters()
        {
            var availableCounters = await CounterDao.GetAvailableCountersv2();
            return availableCounters.Select(c => c.CounterId);
        }

        public async Task<bool> AssignCounterToUser(string useId, string counterId)
        {
            var counter = await CounterDao.GetCounterById(counterId);
            if (counter == null)
            {
                return false;
            }

            if (counter.IsOccupied)
            {
                return false;
            }

            await UserDao.UpdateCounterByUserId(useId, counterId);
            await CounterDao.UpdateCounterStatus(counterId, true);

            return true;
        }

        public async Task<bool> ReleaseCounterFromUser(User user)
        {
            if (!string.IsNullOrEmpty(user.CounterId))
            {
                var counter = await CounterDao.GetCounterById(user.CounterId);
                if (counter != null)
                {
                    await CounterDao.UpdateCounterStatus(counter.CounterId, false);
                }

                user.CounterId = null;
                await UserDao.UpdateCounterByUserId(user.UserId, user.CounterId);
            }

            return true;
        }
        public async Task<Customer> GetCustomer(Customer customer)
        {
            return await customerDao.CustomerLogin(customer.Phone, customer.Password);    
        }
        public async Task<string> GetUserIdByName(string name)
        {
            return await UserDao.GetUserIdByName(name);
        }
    }
}