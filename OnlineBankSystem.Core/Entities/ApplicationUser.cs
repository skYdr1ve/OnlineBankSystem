using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OnlineBankSystem.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
