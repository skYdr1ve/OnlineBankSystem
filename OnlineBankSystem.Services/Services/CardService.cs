using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Repositories;
using OnlineBankSystem.Services.Interfaces;

namespace OnlineBankSystem.Services.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly ISmsService _smsService;
        private readonly ICardHelper _cardHelper;
        private readonly ICryptoHelper _cryptoHelper;

        public CardService(ICardRepository cardRepository, ISmsService smsService,
            ICardHelper cardHelper, ICryptoHelper cryptoHelper)
        {
            _cardRepository = cardRepository;
            _smsService = smsService;
            _cardHelper = cardHelper;
            _cryptoHelper = cryptoHelper;
        }

        public async Task<bool> BlockCardAsync(Guid id, string phoneNumber)
        {
            var card = await _cardRepository.Find(id, track: true);

            if (card == null)
            {
                return false;
            }

            card.StatusId = 4;

            try
            {
                await _cardRepository.Update();
            }
            catch (Exception e)
            {
                return false;
            }

            await _smsService.SendAsync(phoneNumber, $"Your card was blocked: ************{card.Number.Substring(12)}");

            return true;
        }

        public async Task<bool> UnblockCardAsync(Guid id, string phoneNumber, string number, string cvv, string pinCode)
        {
            var card = await _cardRepository.Find(id, track: true);

            if (card == null || card.Number != _cryptoHelper.Hash(number) || 
                card.SecurityCode != _cryptoHelper.Hash(cvv) || card.PinCode != _cryptoHelper.Hash(pinCode))
            {
                return false;
            }

            card.StatusId = 1;

            try
            {
                await _cardRepository.Update();
            }
            catch (Exception e)
            {
                return false;
            }

            await _smsService.SendAsync(phoneNumber, $"Your card was unblocked: ************{card.Number.Substring(12)}");

            return true;
        }

        public async Task<bool> ChangeCardNameAsync(Guid cardId, string newName)
        {
            var card = await _cardRepository.Find(cardId, track: true);

            if (card == null)
            {
                return false;
            }

            card.Name = newName;

            try
            {
                await _cardRepository.Update();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ChangePinCodeAsync(Guid cardId, string phoneNumber)
        {
            var card = await _cardRepository.Find(cardId, track: true);

            if (card == null)
            {
                return false;
            }
            var pinCode = _cardHelper.GeneratePinCode();
            card.PinCode = _cryptoHelper.Hash(pinCode);

            try
            {
                await _cardRepository.Update();
            }
            catch (Exception e)
            {
                return false;
            }

            await _smsService.SendAsync(phoneNumber, $"Your new pin-code: {pinCode}");

            return true;
        }
    }
}
