using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Services.Models
{
    public class WithdrawMoneyServiceModel
    {
        public Account Account { get; set; }
        public decimal Amount { get; set; }
    }
}
