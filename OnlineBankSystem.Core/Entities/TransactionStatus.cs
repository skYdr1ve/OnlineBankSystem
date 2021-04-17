using System.Collections.Generic;

namespace OnlineBankSystem.Core.Entities
{
    public class TransactionStatus : Entity<int>
    {
        public string Name { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
