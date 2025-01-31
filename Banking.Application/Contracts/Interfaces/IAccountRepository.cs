using Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Application.Contracts.Interfaces
{
    public interface IAccountRepository
    {
        Task<int> CreateAccount(Account account);
        Task<Account> GetAccountById(Guid accountId);
        int GenerateNewAccountNumberAsync();
    }
}
