using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Core.Repositories;
using OnlineBankSystem.Services.Interfaces;
using OnlineBankSystem.Services.Mappers;
using OnlineBankSystem.Services.Models;

namespace OnlineBankSystem.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionHelper _transactionHelper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountService _accountService;

        public TransactionService(ITransactionHelper transactionHelper, ITransactionRepository transactionRepository, IAccountRepository accountRepository, IAccountService accountService)
        {
            _transactionHelper = transactionHelper;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _accountService = accountService;
        }

        public async Task CreateTransactionAsync(Account fromAccount, Account toAccount, decimal exchangeRate, decimal amount, string description, string fromCurrency, string toCurrency)
        {
            using (var transaction = await _transactionRepository.BeginTransaction())
            {
                try
                {
                    var withdrawTransfer = new Transaction
                    {
                        Id = new Guid(),
                        FromAccountId = fromAccount.Id,
                        ToAccountId = null,
                        ExchangeRate = exchangeRate,
                        Amount = amount,
                        CardId = null,
                        Date = DateTime.Now,
                        Description = description,
                        StatusId = 1,
                        FromCurrency = fromCurrency,
                        ToCurrency = toCurrency,
                        Number = _transactionHelper.Number(),
                        Destination = toAccount.Number
                    };

                    if ((fromAccount.Balance - amount) < 0)
                    {
                        withdrawTransfer.StatusId = 2;

                        await _transactionRepository.Add(withdrawTransfer);
                    }
                    else
                    {
                        fromAccount.Balance -= amount;
                        await _transactionRepository.Add(withdrawTransfer);

                        var incomeTransfer = new Transaction
                        {
                            Id = new Guid(),
                            FromAccountId = fromAccount.Id,
                            ToAccountId = toAccount.Id,
                            ExchangeRate = exchangeRate,
                            Amount = amount,
                            CardId = null,
                            Date = DateTime.Now,
                            Description = description,
                            StatusId = 1,
                            FromCurrency = fromCurrency,
                            ToCurrency = toCurrency,
                            Number = _transactionHelper.Number(),
                            Destination = toAccount.Number
                        };

                        toAccount.Balance += amount;
                        await _transactionRepository.Add(incomeTransfer);

                        await _accountRepository.Update();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    await _transactionRepository.Add(new Transaction
                    {
                        Id = new Guid(),
                        FromAccountId = fromAccount.Id,
                        ToAccountId = null,
                        ExchangeRate = exchangeRate,
                        Amount = amount,
                        CardId = null,
                        Date = DateTime.Now,
                        Description = description,
                        StatusId = 2,
                        FromCurrency = fromCurrency,
                        ToCurrency = null,
                        Number = _transactionHelper.Number(),
                        Destination = toAccount.Number
                    });
                }
            }
        }

        public async Task<Transaction> GetTransactionAsync(Guid id)
        {
            return await _transactionRepository.Find(id, "Status");
        }

        public async Task<IEnumerable<TransactionServiceModel>> GetTransactionsAsync(Guid id)
        {
            Expression<Func<Transaction, bool>> filter = null;
            filter = x => x.ToAccountId == id || x.FromAccountId == id;
            var transactions = await _transactionRepository.List(filter, includeProperties: "FromAccount");
            return TransactionMapper.Map(transactions);
        }

        public async Task<IEnumerable<TransactionServiceModel>> GetAllTransactionsAsync(Guid id)
        {
            Expression<Func<Transaction, bool>> filter = null;
            filter = x => x.FromAccount.UserId == id || x.ToAccount.UserId == id;
            var transactions = await _transactionRepository.List(filter, includeProperties: "FromAccount,ToAccount");
            return TransactionMapper.Map(transactions);
        }

        public async Task<IEnumerable<TransactionServiceModel>> GetLast10TransactionsAsync(Guid id)
        {
            var transactions = await _transactionRepository.Last10(id);

            return TransactionMapper.Map(transactions);
        }

        public async Task<bool> WithdrawMoneyByCardAsync(WithdrawMoneyServiceModel model)
        {
            using (var transaction = await _transactionRepository.BeginTransaction())
            {
                try
                {
                    var withdrawTransfer = new Transaction
                    {
                        Id = new Guid(),
                        FromAccountId = null,
                        ToAccountId = model.Account.Id,
                        ExchangeRate = 1,
                        Amount = model.Amount,
                        CardId = null,
                        Date = DateTime.Now,
                        Description = "Withdraw Money",
                        StatusId = 1,
                        FromCurrency = model.Account.Currency.Code,
                        ToCurrency = model.Account.Currency.Code,
                        Number = _transactionHelper.Number(),
                        Destination = model.Account.Number
                    };

                    if ((model.Account.Balance - model.Amount) < 0)
                    {
                        withdrawTransfer.StatusId = 2;

                        await _transactionRepository.Add(withdrawTransfer);
                    }
                    else
                    {
                        model.Account.Balance -= model.Amount;
                        await _transactionRepository.Add(withdrawTransfer);

                        await _accountRepository.Update();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    await _transactionRepository.Add(new Transaction
                    {
                        Id = new Guid(),
                        FromAccountId = null,
                        ToAccountId = model.Account.Id,
                        ExchangeRate = 1,
                        Amount = model.Amount,
                        CardId = null,
                        Date = DateTime.Now,
                        Description = "Withdraw Money",
                        StatusId = 2,
                        FromCurrency = model.Account.Currency.Code,
                        ToCurrency = model.Account.Currency.Code,
                        Number = _transactionHelper.Number(),
                        Destination = model.Account.Number
                    });

                    return false;
                }
            }

            return true;
        }

        public async Task<bool> DepositMoneyByCardAsync(DepositMoneyServiceModel model)
        {
            using (var transaction = await _transactionRepository.BeginTransaction())
            {
                try
                {
                    var transfer = new Transaction
                    {
                        Id = new Guid(),
                        FromAccountId = null,
                        ToAccountId = model.Account.Id,
                        ExchangeRate = 1,
                        Amount = model.Amount,
                        CardId = null,
                        Date = DateTime.Now,
                        Description = "Deposit Money",
                        StatusId = 1,
                        FromCurrency = model.Account.Currency.Code,
                        ToCurrency = model.Account.Currency.Code,
                        Number = _transactionHelper.Number(),
                        Destination = model.Account.Number
                    };

                    model.Account.Balance += model.Amount;
                    await _transactionRepository.Add(transfer);

                    await _accountRepository.Update();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    await _transactionRepository.Add(new Transaction
                    {
                        Id = new Guid(),
                        FromAccountId = null,
                        ToAccountId = model.Account.Id,
                        ExchangeRate = 1,
                        Amount = model.Amount,
                        CardId = null,
                        Date = DateTime.Now,
                        Description = "Deposit Money",
                        StatusId = 2,
                        FromCurrency = model.Account.Currency.Code,
                        ToCurrency = model.Account.Currency.Code,
                        Number = _transactionHelper.Number(),
                        Destination = model.Account.Number
                    });

                    return false;
                }
            }

            return true;
        }
    }
}
