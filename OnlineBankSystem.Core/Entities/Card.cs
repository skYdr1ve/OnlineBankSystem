using System;
using System.Collections.Generic;

namespace OnlineBankSystem.Core.Entities
{
    public class Card : Entity<Guid>
    {
        public string Number { get; set; }
        public string Last4Digits { get; set; }
        public string PinCode { get; set; }
        public string Name { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpireTime { get; set; }
        public string SecurityCode { get; set; }
        public int StatusId { get; set; }
        public CardStatus Status { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
