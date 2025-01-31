using Banking.Application.Contracts.Interfaces;
using Banking.Domain;
using Banking.Domain.Models;
using Banking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Banking.Infrastructure.Repositories
{
    public class AccountRepositry : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepositry(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAccount(Account account)
        {
            _context.Accounts.Add(account);
            return await _context.SaveChangesAsync();
        }

        public int GenerateNewAccountNumberAsync()
        {
            if (_context.Accounts.Count() > 0)
            {
                return _context.Accounts.Max(x => x.AccountNumber);
            }
            return Constants.DefaultAccountNumber;
        }

    }
}
