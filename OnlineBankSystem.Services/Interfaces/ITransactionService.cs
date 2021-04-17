using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Services.Models;

namespace OnlineBankSystem.Services.Interfaces
{
    public interface ITransactionService
    {
        public Task<IEnumerable<TransactionServiceModel>> GetTransactionsAsync(Guid id);
        public Task<IEnumerable<TransactionServiceModel>> GetLast10TransactionsAsync(Guid id);
        public Task<IEnumerable<TransactionServiceModel>> GetAllTransactionsAsync(Guid id);
        public Task<Transaction> GetTransactionAsync(Guid id);
        public Task CreateTransactionAsync(Account fromAccount, Account toAccount, decimal exchangeRate, decimal amount, string description, string fromCurrency, string toCurrency);
        public Task<bool> WithdrawMoneyByCardAsync(WithdrawMoneyServiceModel model);
        public Task<bool> DepositMoneyByCardAsync(DepositMoneyServiceModel model);
    }
}
