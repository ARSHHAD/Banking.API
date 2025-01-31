using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Domain.Enums
{
    public class Enum
    {
        public enum UserRole
        {
            Admin = 1,
            Customer
        }

        public enum AccountType
        {
            Savings = 1,
            Business
        }

        public enum TransactionType
        {
            Credit = 1,
            Debit
        }

        public enum TransactionStatus
        {
            Failed,
            Success
        }
    }
}
