using Banking.Domain.Models;

namespace Banking.Application.Contracts.Interfaces
{
    public interface ITransactionRepository
    {
        Task CreateTransaction(Transaction transaction, Account account);
        Task<List<Transaction>> GetTransactions(DateTime fromDate, DateTime toDate);
    }
}
