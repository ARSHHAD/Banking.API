using Banking.Application.Models.Dto;
using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;

namespace Banking.Application.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<ResponseViewModel<bool>> RegisterAsync(RegisterDto registerModel);
        Task<ResponseViewModel<User>> GetUserInfo(string userId);
    }
}
