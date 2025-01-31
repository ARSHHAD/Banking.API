
using Banking.Domain.Models;

namespace Banking.Application.Contracts.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUserNameAndPassword(string username, string password);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(Guid id);
        Task<User?> GetUserByUserName(string username);
        Task<int> AddUser(User user);
    }
}