using OnlineBankSystem.Core.Repositories;
using OnlineBankSystem.Services.Interfaces;

namespace OnlineBankSystem.Services.Services
{
    public class AccountStatusService : IAccountStatusService
    {
        private readonly IAccountStatusRepository _accountStatusRepository;

        public AccountStatusService(IAccountStatusRepository accountStatusRepository)
        {
            _accountStatusRepository = accountStatusRepository;
        }
    }
}
