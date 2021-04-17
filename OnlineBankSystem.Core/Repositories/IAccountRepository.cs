using System;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Core.Repositories
{
    public interface IAccountRepository : IRepository<Account, Guid>
    {
        Task<Account> FindByNumber(string number, string includeProperties = "", bool track = false);
        Task<Account> FindByCardNumber(string number, string includeProperties = "", bool track = false);
        Task Update();
    }
}
