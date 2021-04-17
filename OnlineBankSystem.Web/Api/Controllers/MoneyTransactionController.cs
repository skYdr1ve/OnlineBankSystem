using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnlineBankSystem.Services.Interfaces;
using OnlineBankSystem.Services.Models;
using OnlineBankSystem.Web.Api.Models;

namespace OnlineBankSystem.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyTransactionController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly ICryptoHelper _cryptoHelper;

        public MoneyTransactionController(
            IAccountService accountService,
            ITransactionService transactionService,
            ICryptoHelper cryptoHelper)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            _cryptoHelper = cryptoHelper;
        }

        [HttpPost]
        public async Task<IActionResult> WithdrawMoneyByCard([FromBody] string data)
        {
            var model = JsonConvert.DeserializeObject<WithdrawMoneyModel>(data);
            if (!TryValidateModel(model))
            {
                return BadRequest();
            }

            var account =
                await _accountService.GetAccountByCardNumberAsync(model.CardNumber);
            if (account == null || account.Cards.Find(x=>x.Number == model.CardNumber).PinCode != _cryptoHelper.Hash(model.PinCode))
            {
                return BadRequest();
            }

            var serviceModel = new WithdrawMoneyServiceModel
            {
                Account = account,
                Amount = model.Amount,
            };

            var isSuccessful = await _transactionService.WithdrawMoneyByCardAsync(serviceModel);
            if (!isSuccessful)
            {
                return NoContent();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DepositMoney([FromBody] string data)
        {
            var model = JsonConvert.DeserializeObject<DepositMoneyModel>(data);
            if (!TryValidateModel(model))
            {
                return BadRequest();
            }

            var account =
                await _accountService.GetAccountByCardNumberAsync(model.CardNumber);
            if (account == null)
            {
                return BadRequest();
            }

            var serviceModel = new DepositMoneyServiceModel
            {
                Account = account,
                Amount = model.Amount,
            };

            var isSuccessful = await _transactionService.DepositMoneyByCardAsync(serviceModel);
            if (!isSuccessful)
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
