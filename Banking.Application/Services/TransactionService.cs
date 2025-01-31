using Banking.Application.Contracts.Interfaces;
using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using static Banking.Domain.Enums.Enum;

namespace Banking.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountService _accountService;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(ITransactionRepository transactionRepository, IAccountService accountService, ILogger<TransactionService> logger)
        {
            _transactionRepository = transactionRepository;
            _accountService = accountService;
            _logger = logger;
        }

        public async Task<ResponseViewModel<Guid>> CreateTransaction(Transaction transaction)
        {
            var response = new ResponseViewModel<Guid>();

            try
            {
                var accountData = await _accountService.GetAccountById(transaction.AccountId);

                if (accountData.Success)
                {
                    var account = accountData.Data;

                    switch (transaction.TransactionType)
                    {
                        case TransactionType.Credit:
                            account.Balance += transaction.Amount;
                            break;

                        case TransactionType.Debit:
                            if (transaction.Amount > account.Balance)
                            {
                                throw new Exception("Not enough fund available..");
                            }
                            else
                            {
                                account.Balance -= transaction.Amount;
                            }
                            break;
                    }
                    await _transactionRepository.CreateTransaction(transaction, account);

                    response.Data = transaction.TransactionId;
                    response.Success = true;

                    _logger.LogInformation("Transaction created successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Creating Transaction failed: {message}", ex.Message);
                response.Message = "Creating Transaction failed";
            }

            return response;
        }

        public async Task<ResponseViewModel<List<Transaction>>> GetTransactions(DateTime fromDate, DateTime toDate)
        {
            var response = new ResponseViewModel<List<Transaction>>();

            try
            {
                response.Data = await _transactionRepository.GetTransactions(fromDate, toDate);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "An error occured while fetching transactions";
                _logger.LogError("An error occured while fetching transactions: {message}", ex.Message);
            }

            return response;
        }
    }
}
