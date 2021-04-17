using System;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Core.Repositories
{
    public interface ICardRepository : IRepository<Card, Guid>
    {
        Task Update();
    }
}
