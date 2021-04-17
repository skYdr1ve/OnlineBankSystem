using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Services.Interfaces;

namespace OnlineBankSystem.Web.Controllers
{
    public class CardController : BaseController
    {
        private readonly ICardService _cardService;
        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CardController(ICardService cardService, IAccountService accountService, UserManager<ApplicationUser> userManager)
        {
            _cardService = cardService;
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> BlockCard(Guid accountId, Guid cardId)
        {
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

            var isSuccessful = await _cardService.BlockCardAsync(cardId, user.PhoneNumber);

            return Ok(new
            {
                success = isSuccessful
            });
        }

        [HttpPost]
        public async Task<IActionResult> UnblockCard(Guid accountId, Guid cardId, string number, string cvv, string pinCode)
        {
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

            var isSuccessful = await _cardService.UnblockCardAsync(cardId, user.PhoneNumber, number, cvv, pinCode);

            return Ok(new
            {
                success = isSuccessful
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCardName(Guid accountId, Guid cardId, string name)
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
                account.UserId != Guid.Parse(GetCurrentUserId()) ||
                cardId == Guid.Empty)
            {
                return Ok(new
                {
                    success = false
                });
            }

            var isSuccessful = await _cardService.ChangeCardNameAsync(cardId, name);

            return Ok(new
            {
                success = isSuccessful
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePinCode(Guid accountId, Guid cardId)
        {
            var account = await _accountService.GetAccountAsync(accountId);
            if (account == null ||
                account.UserId != Guid.Parse(GetCurrentUserId()) ||
                cardId == Guid.Empty)
            {
                return Ok(new
                {
                    success = false
                });
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var isSuccessful = await _cardService.ChangePinCodeAsync(cardId, user.PhoneNumber);

            return Ok(new
            {
                success = isSuccessful
            });
        }
    }
}
