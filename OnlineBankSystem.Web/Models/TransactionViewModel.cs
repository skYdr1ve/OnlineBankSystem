using System.Collections.Generic;
using OnlineBankSystem.Services.Models;

namespace OnlineBankSystem.Web.Models
{
    public class TransactionViewModel
    {
        public IEnumerable<TransactionServiceModel> Transactions { get; set; }
    }
}
