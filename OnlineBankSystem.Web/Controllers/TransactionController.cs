using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBankSystem.Services.Interfaces;
using OnlineBankSystem.Web.Models;

namespace OnlineBankSystem.Web.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly ICurrencyService _currencyService;
        private readonly ITransactionService _transactionService;

        public TransactionController(IAccountService accountService, ICurrencyService currencyService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _currencyService = currencyService;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateInternal()
        {
            var accounts =
                (List<Core.Entities.Account>) await _accountService.GetAccountsAsync(Guid.Parse(GetCurrentUserId()));

            var model = new InternalMoneyTransferViewModel
            {
                Accounts = accounts
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInternal(InternalMoneyTransferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Accounts =
                    (List<Core.Entities.Account>) await _accountService.GetAccountsAsync(
                        Guid.Parse(GetCurrentUserId()));
                return View(model);
            }

            var fromAccount = await _accountService.GetAccountAsync(model.AccountId.Value, true);
            var toAccount = await _accountService.GetAccountAsync(model.ToAccountId.Value, true);

            var currency = await _currencyService.Convert(fromAccount.Currency.Code, toAccount.Currency.Code, model.Amount);

            await _transactionService.CreateTransactionAsync(fromAccount, toAccount, currency.Rate, model.Amount,
                model.Description, fromAccount.Currency.Code, toAccount.Currency.Code);

            return RedirectToHome();
        }

        [HttpGet]
        public async Task<IActionResult> CreateGlobal()
        {
            var accounts =
                (List<Core.Entities.Account>)await _accountService.GetAccountsAsync(Guid.Parse(GetCurrentUserId()));

            var model = new GlobalMoneyTransferViewModel()
            {
                Accounts = accounts
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGlobal(GlobalMoneyTransferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Accounts =
                    (List<Core.Entities.Account>)await _accountService.GetAccountsAsync(
                        Guid.Parse(GetCurrentUserId()));
                return View(model);
            }

            model.ToAccountNumber = model.ToAccountNumber.ToUpperInvariant();

            var fromAccount = await _accountService.GetAccountAsync(model.AccountId.Value, true);
            var toAccount = await _accountService.GetAccountByNumberAsync(model.ToAccountNumber, true);

            if (toAccount == null)
            {
                ModelState.AddModelError("ToAccountNumber", "This account does not exist");
                model.Accounts =
                    (List<Core.Entities.Account>)await _accountService.GetAccountsAsync(
                        Guid.Parse(GetCurrentUserId()));
                return View(model);
            }

            var currency = await _currencyService.Convert(fromAccount.Currency.Code, toAccount.Currency.Code, model.Amount);

            await _transactionService.CreateTransactionAsync(fromAccount, toAccount, currency.Rate, model.Amount,
                model.Description, fromAccount.Currency.Code, toAccount.Currency.Code);

            return RedirectToHome();
        }

        public async Task<IActionResult> All()
        {
            var userId = GetCurrentUserId();

            var transactions = await _transactionService.GetAllTransactionsAsync(Guid.Parse(userId));

            var viewModel = new TransactionViewModel
            {
                Transactions = transactions
            };

            return View(viewModel);
        }
    }
}
