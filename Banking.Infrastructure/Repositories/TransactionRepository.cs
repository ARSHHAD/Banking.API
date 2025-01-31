using Banking.Application.Contracts.Interfaces;
using Banking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Models = Banking.Domain.Models;

namespace Banking.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TransactionRepository> _logger;

        public TransactionRepository(ApplicationDbContext context, ILogger<TransactionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateTransaction(Models.Transaction transaction, Models.Account account)
        {
            using (var dbTransaction = _context.Database.BeginTransaction())
            {
                _logger.LogInformation("Creating a transaction");

                try
                {
                    await _context.Transactions.AddAsync(transaction);
                    _context.Accounts.Update(account);
                    await _context.SaveChangesAsync();
                    await dbTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await dbTransaction.RollbackAsync();
                    _logger.LogError("Error during the transaction: {message}", ex.Message);
                }
            }
        }

        public async Task<List<Models.Transaction>> GetTransactions(DateTime fromDate, DateTime toDate)
        {
            return await _context.Transactions.
                 Where(t => t.TransactionDate >= fromDate && t.TransactionDate <= toDate).ToListAsync();
        }
    }
}
