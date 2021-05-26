using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Services.Interfaces;
using OnlineBankSystem.Web.Models;

namespace OnlineBankSystem.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly ICountryCurrencyCodeService _countryCurrencyCodeService;
        private readonly IDepartamentService _departamentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IAccountService accountService, ITransactionService transactionService, 
            ICountryCurrencyCodeService countryCurrencyCodeService, IDepartamentService departamentService,
            UserManager<ApplicationUser> userManager)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            _userManager = userManager;
            _countryCurrencyCodeService = countryCurrencyCodeService;
            _departamentService = departamentService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new AccountEditViewModel
            {
                Currencies = (List<CountryCurrencyCode>) await _countryCurrencyCodeService.GetCurrenciesAsync(),
                Departaments = (List<Departament>) await _departamentService.GetDepartamentsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetCurrentUserId();

            var account = new Account
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                CreatedOn = DateTime.Now,
                Balance = 0,
                Name = string.IsNullOrEmpty(model.Name) ? "Account" : model.Name,
                StatusId = 1,
                CurrencyId = model.CurrencyNumber,
                Cards = new List<Card>()
            };

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            await _accountService.CreateAccountAsync(account, user.FullName, user.PhoneNumber, model.CardName);
            
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Details(Guid id, int pageIndex = 1)
        {
            var account = await _accountService.GetAccountAsync(id);
            if (account == null ||
                account.UserId != Guid.Parse(GetCurrentUserId()))
            {
                return Forbid();
            }

            if (account.StatusId == 2)
            {
                RedirectToHome();
            }

            var transactionsResult = await _transactionService.GetTransactionsAsync(account.Id);

            var transactions = transactionsResult.Where(x => x.ToAccountId == null || x.ToAccountId == id).OrderByDescending(x => x.Date).ToList();

            account.Cards = account.Cards.Where(x => x.StatusId != 2).ToList();

            var model = new AccountViewModel
            {
                Account = account,
                MoneyTransfersCount = transactions.Count,
                Transactions = transactions,
                Departaments = (List<Departament>)await _departamentService.GetDepartamentsAsync()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeactivateAccount(Guid accountId)
        {
            var account = await _accountService.GetAccountAsync(accountId);
            if (account == null ||
                account.UserId != Guid.Parse(GetCurrentUserId()))
            {
                return Ok(new
                {
                    success = false
                });
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var code = await _accountService.SendDeactivateCodeAsync(user.PhoneNumber);

            if (string.IsNullOrEmpty(code))
            {
                return Ok(new
                {
                    success = false
                });
            }

            TempData["code"] = code;

            return Ok(new
            {
                success = true
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeactivateAccount(Guid accountId, string code)
        {
            var account = await _accountService.GetAccountAsync(accountId);
            
            if (account == null ||
                account.UserId != Guid.Parse(GetCurrentUserId()) ||
                TempData["code"] == null)
            {
                return Ok(new
                {
                    success = false
                });
            }

            var correctCode = TempData["code"] as string;

            if (correctCode != code)
            {
                return Ok(new
                {
                    success = false
                });
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var isSuccessful = await _accountService.DeactivateAccountAsync(accountId, user.PhoneNumber);

            return Ok(new
            {
                success = isSuccessful
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAccountName(Guid accountId, string name)
        {
            if (name == null)
            {
                return Ok(new
                {
                    success = false
                });
            }

            var account = await _accountService.GetAccountAsync(accountId);
            if (account == null ||
                account.UserId != Guid.Parse(GetCurrentUserId()))
            {
                return Ok(new
                {
                    success = false
                });
            }

            var isSuccessful = await _accountService.ChangeAccountNameAsync(accountId, name);

            return Ok(new
            {
                success = isSuccessful
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(Guid accountId, string name, string cardHolderName, Guid departamentId)
        {
            if (name == null)
            {
                return Ok(new
                {
                    success = false
                });
            }

            var account = await _accountService.GetAccountAsync(accountId, true);
            if (account == null ||
                account.UserId != Guid.Parse(GetCurrentUserId()))
            {
                return Ok(new
                {
                    success = false
                });
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var isSuccessful = await _accountService.AddCardAsync(account, name, cardHolderName, user.PhoneNumber);

            return Ok(new
            {
                success = isSuccessful
            });
        }
    }
}
