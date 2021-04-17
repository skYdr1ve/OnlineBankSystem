using System.Collections.Generic;

namespace OnlineBankSystem.Core.Entities
{
    public class AccountStatus : Entity<int>
    {
        public string Name { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
