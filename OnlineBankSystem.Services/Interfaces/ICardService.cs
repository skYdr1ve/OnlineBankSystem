using System;
using System.Threading.Tasks;

namespace OnlineBankSystem.Services.Interfaces
{
    public interface ICardService
    {
        public Task<bool> BlockCardAsync(Guid id, string phoneNumber);
        public Task<bool> UnblockCardAsync(Guid id, string phoneNumber, string number, string cvv, string pinCode);
        public Task<bool> ChangeCardNameAsync(Guid cardId, string newName);
        public Task<bool> ChangePinCodeAsync(Guid cardId, string phoneNumber);
    }
}
