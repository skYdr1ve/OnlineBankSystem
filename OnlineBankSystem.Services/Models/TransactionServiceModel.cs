using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Services.Models
{
    public class TransactionServiceModel
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ToCurrency { get; set; }
        public string FromCurrency { get; set; }
        public decimal ExchangeRate { get; set; }

        public int StatusId { get; set; }
        public TransactionStatus Status { get; set; }
        public Guid? FromAccountId { get; set; }
        public Account FromAccount { get; set; }
        public Guid? ToAccountId { get; set; }
        public Account ToAccount { get; set; }
        public Guid? CardId { get; set; }
        public Card Card { get; set; }

        public bool Income { get; set; }
        public string Destination { get; set; }
    }
}
