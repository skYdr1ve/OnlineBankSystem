using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Services.Models;

namespace OnlineBankSystem.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Account> Accounts { get; set; }

        public IEnumerable<TransactionServiceModel> Transactions { get; set; }
    }
}
