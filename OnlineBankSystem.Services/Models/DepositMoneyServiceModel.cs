using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Services.Models
{
    public class DepositMoneyServiceModel
    {
        public Account Account { get; set; }
        public decimal Amount { get; set; }
    }
}
