using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<IEnumerable<Account>> GetAccountsAsync(Guid userId);
        public Task<Account> GetAccountAsync(Guid accountId, bool track = false);
        public Task<Account> GetAccountByNumberAsync(string number, bool track = false);
        public Task<Account> GetAccountByCardNumberAsync(string number, bool track = false);
        public Task CreateAccountAsync(Account account, string name, string phoneNumber, string cardName);
        public Task<bool> ChangeAccountNameAsync(Guid accountId, string newName);
        public Task<bool> AddCardAsync(Account account, string name, string cardHolderName, string phoneNumber);
        public Task<string> SendDeactivateCodeAsync(string phoneNumber);
        public Task<bool> DeactivateAccountAsync(Guid accountId, string phoneNumber);
    }
}
