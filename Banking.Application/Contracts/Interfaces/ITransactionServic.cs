using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Application.Contracts.Interfaces
{
    public interface ITransactionService
    {
        Task<ResponseViewModel<Guid>> CreateTransaction(Transaction transaction);
        Task<ResponseViewModel<List<Transaction>>> GetTransactions(DateTime fromDate, DateTime toDate);
    }
}
