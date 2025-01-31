using static Banking.Domain.Enums.Enum;

namespace Banking.Domain.Models
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public int AccountNumber { get; set; }
        public Guid UserId { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateCreated { get; set; }
        public User? User { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }
    }

}
