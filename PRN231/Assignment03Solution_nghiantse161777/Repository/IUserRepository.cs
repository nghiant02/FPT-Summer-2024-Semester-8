using BussiniseObject.Models;

namespace Repository;

public interface IUserRepository
{
    Task<IEnumerable<AspNetUser>> GetUsers();
    Task<string> CreateToken(string email, string password);
}