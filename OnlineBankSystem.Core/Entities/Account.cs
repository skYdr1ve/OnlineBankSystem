using System;
using System.Collections.Generic;

namespace OnlineBankSystem.Core.Entities
{
    public class Account : Entity<Guid>
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Balance { get; set; }
        public int StatusId { get; set; }
        public AccountStatus Status { get; set; }
        public int CurrencyId { get; set; }
        public CountryCurrencyCode Currency { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<Card> Cards { get; set; }
        public List<Transaction> FromTransactions { get; set; }
        public List<Transaction> ToTransactions { get; set; }
    }
}
