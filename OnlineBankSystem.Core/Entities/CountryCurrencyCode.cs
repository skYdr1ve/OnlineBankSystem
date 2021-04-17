using System.Collections.Generic;

namespace OnlineBankSystem.Core.Entities
{
    public class CountryCurrencyCode : Entity
    {
        public int Number { get; set; }
        public string Code { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
