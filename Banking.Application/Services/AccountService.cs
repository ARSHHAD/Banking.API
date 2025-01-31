using Banking.Application.Contracts.Interfaces;
using Banking.Domain;
using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;
using Microsoft.Extensions.Logging;

namespace Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository accountRepository, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<ResponseViewModel<bool>> CreateAccount(Account account)
        {
            var response = new ResponseViewModel<bool>();
            try
            {
                account.AccountNumber = _accountRepository.GenerateNewAccountNumberAsync() + 1;
                account.Balance = Constants.DefaultOpeningBalance;
                account.DateCreated = DateTime.UtcNow;

                await _accountRepository.CreateAccount(account);

                response.Message = "Account created successfully";
                response.Success = true;
                _logger.LogInformation("Account created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Account creation failed: {message}", ex.Message);
                response.Message = "Account creation failed";
            }

            return response;
        }


    }
}
