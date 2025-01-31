using Banking.Application.Contracts.Interfaces;
using Banking.Domain.Models;
using Banking.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            var response = await _transactionService.CreateTransaction(transaction);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost("getbydate")]
        public async Task<IActionResult> GetTransactionsByDate([FromBody] DateFilter filter)
        {
            var response = await _transactionService.GetTransactions(filter.FromDate, filter.ToDate);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
