using Banking.Application.Contracts.Interfaces;
using Banking.Domain.Models;
using Banking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByUserNameAndPassword(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User?> GetUserByUserName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<int> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _context.Users
                .Include(u=>u.Accounts)
                .ThenInclude(a=>a.Transactions.OrderByDescending(t=>t.TransactionDate).Take(5)) // last 5 transactions
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
