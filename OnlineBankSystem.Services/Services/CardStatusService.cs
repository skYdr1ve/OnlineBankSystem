using OnlineBankSystem.Core.Repositories;
using OnlineBankSystem.Services.Interfaces;

namespace OnlineBankSystem.Services.Services
{
    public class CardStatusService : ICardStatusService
    {
        private readonly ICardStatusRepository _cardStatusRepository;

        public CardStatusService(ICardStatusRepository cardStatusRepository)
        {
            _cardStatusRepository = cardStatusRepository;
        }
    }
}
