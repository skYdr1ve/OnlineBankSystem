using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineBankSystem.Web.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OnlineBankSystem.Services.Interfaces;

namespace OnlineBankSystem.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public HomeController(ILogger<HomeController> logger, IAccountService accountService, ITransactionService transactionService)
        {
            _logger = logger;
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("IndexGuest");
            }

            var userId = GetCurrentUserId();

            var accounts = await _accountService.GetAccountsAsync(Guid.Parse(userId));
            var transactions = await _transactionService.GetLast10TransactionsAsync(Guid.Parse(userId));

            var viewModel = new HomeViewModel
            {
                Accounts = accounts,
                Transactions = transactions
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
