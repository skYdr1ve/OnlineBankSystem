using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Core.Repositories;
using OnlineBankSystem.Services.Interfaces;

namespace OnlineBankSystem.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IAccountHelper _accountHelper;
        private readonly ICardHelper _cardHelper;
        private readonly ICryptoHelper _cryptoHelper;
        private readonly ISmsService _smsService;

        public AccountService(IAccountRepository accountRepository, ICardRepository cardRepository, 
            IAccountHelper accountHelper, ICardHelper cardHelper, ISmsService smsService, ICryptoHelper cryptoHelper)
        {
            _accountRepository = accountRepository;
            _cardRepository = cardRepository;
            _accountHelper = accountHelper;
            _cardHelper = cardHelper;
            _cryptoHelper = cryptoHelper;
            _smsService = smsService;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(Guid userId)
        {
            Expression<Func<Account, bool>> filter = null;
            filter = x => x.UserId == userId && x.StatusId == 1;
            return await _accountRepository.List(filter, includeProperties: "Cards,Currency,FromTransactions,ToTransactions");
        }

        public async Task<Account> GetAccountAsync(Guid accountId, bool track = false)
        {
            return await _accountRepository.Find(accountId, "Cards,Cards.Status,Currency,Status", track);
        }

        public async Task<Account> GetAccountWithTransactionAsync(Guid accountId)
        {
            return await _accountRepository.Find(accountId, includeProperties: "Transaction");
        }

        public async Task CreateAccountAsync(Account account, string name, string phoneNumber, string cardName)
        {
            var accounts = await _accountRepository.ListAccountsNumbers();
            var number = _accountHelper.GenerateIban();
            while (accounts.Contains(number))
            {
                number = _accountHelper.GenerateIban();
            }

            account.Number = number;

            var cards = await _cardRepository.ListCardsNumber();
            var cardNumber = _cardHelper.Generate16DigitNumber();
            while (cards.Contains(_cryptoHelper.Hash(cardNumber)))
            {
                cardNumber = _cardHelper.Generate16DigitNumber();
            }

            var securityCode = _cardHelper.Generate3DigitSecurityCode();
            var pinCode = _cardHelper.GeneratePinCode();

            var card = new Card
            {
                Id = new Guid(),
                Number = _cryptoHelper.Hash(cardNumber),
                Last4Digits = cardNumber.Substring(12),
                SecurityCode = _cryptoHelper.Hash(securityCode),
                PinCode = _cryptoHelper.Hash(pinCode),
                ExpireTime = DateTime.Now.AddYears(3),
                Name = string.IsNullOrEmpty(cardName) ? "Card Name" : cardName,
                CardHolderName = name,
                StatusId = 1,
                AccountId = account.Id
            };

            account.Cards.Add(card);

            await _smsService.SendAsync(phoneNumber, 
                $"Do not share your data with anyone\nCard Number: {cardNumber}\nName: {card.CardHolderName}\nExpiration date: {card.ExpireTime}\nCVV: {securityCode}\nPin-Code: {pinCode}");

            await _accountRepository.Add(account);
        }

        public async Task<bool> ChangeAccountNameAsync(Guid accountId, string newName)
        {
            var account = await _accountRepository.Find(accountId, track: true);

            if (account == null)
            {
                return false;
            }

            account.Name = newName;

            await _accountRepository.Update();

            return true;
        }

        public async Task<bool> AddCardAsync(Account account, string name, string cardHolderName, string phoneNumber)
        {
            var cards = await _cardRepository.ListCardsNumber();
            var cardNumber = _cardHelper.Generate16DigitNumber();
            while (cards.Contains(_cryptoHelper.Hash(cardNumber)))
            {
                cardNumber = _cardHelper.Generate16DigitNumber();
            }

            var securityCode = _cardHelper.Generate3DigitSecurityCode();
            var pinCode = _cardHelper.GeneratePinCode();

            var card = new Card
            {
                Id = new Guid(),
                Number = _cryptoHelper.Hash(cardNumber),
                Last4Digits = cardNumber.Substring(12),
                SecurityCode = _cryptoHelper.Hash(securityCode),
                PinCode = _cryptoHelper.Hash(pinCode),
                ExpireTime = DateTime.Now.AddYears(3),
                Name = name,
                CardHolderName = cardHolderName,
                StatusId = 1,
                AccountId = account.Id
            };

            account.Cards.Add(card);

            try
            {
                //await _cardRepository.Add(card);
                await _accountRepository.Update();
            }
            catch
            {
                return false;
            }

            await _smsService.SendAsync(phoneNumber, 
                $"Do not share your data with anyone\nCard Number: {cardNumber}\nName: {card.CardHolderName}\nExpiration date: {card.ExpireTime}\nCVV: {securityCode}\nPin-Code: {pinCode}");

            return true;
        }

        public async Task<string> SendDeactivateCodeAsync(string phoneNumber)
        {
            var code = _cardHelper.Generate3DigitSecurityCode() + _cardHelper.Generate3DigitSecurityCode();

            await _smsService.SendAsync(phoneNumber,
                $"Someone is trying to deactivate your account, if it is you then enter this code: {code}");

            return code;
        }

        public async Task<bool> DeactivateAccountAsync(Guid accountId, string phoneNumber)
        {
            var account = await _accountRepository.Find(accountId, track: true, includeProperties: "Cards");

            if (account == null)
            {
                return false;
            }

            account.StatusId = 2;

            foreach (var card in account.Cards)
            {
                card.StatusId = 2;
            }

            try
            {
                await _accountRepository.Update();
            }
            catch
            {
                return false;
            }

            await _smsService.SendAsync(phoneNumber,
                "Your account has been deactivated, you can pick up the remaining funds at the nearest bank branch");

            return true;
        }

        public async Task<Account> GetAccountByNumberAsync(string number, bool track = false)
        {
            return await _accountRepository.FindByNumber(number, "Cards,Cards.Status,Currency,Status", track);
        }

        public async Task<Account> GetAccountByCardNumberAsync(string number, bool track = false)
        {
            var card = await _cardRepository.FindByCardNumber(number);
            return await _accountRepository.Find(card.AccountId, "Cards,Cards.Status,Currency,Status", track);
        }
    }
}
