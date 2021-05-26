using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Core.Repositories
{
    public interface ICardRepository : IRepository<Card, Guid>
    {
        public Task<Card> FindByCardNumber(string number, string includeProperties = "", bool track = true);
        public Task<List<string>> ListCardsNumber();
        Task Update();
    }
}
