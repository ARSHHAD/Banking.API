
using Banking.Application.Models.Dto;
using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;

namespace Banking.Application.Contracts.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseViewModel<string>?> AuthenticateAsync(LoginDto login);
    }
}