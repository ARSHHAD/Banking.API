using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;

namespace Banking.Application.Contracts.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseViewModel<bool>> CreateAccount(Account account);
    }
}