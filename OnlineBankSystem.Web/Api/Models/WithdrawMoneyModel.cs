using System.ComponentModel.DataAnnotations;

namespace OnlineBankSystem.Web.Api.Models
{
    public class WithdrawMoneyModel
    {
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string PinCode { get; set; }
    }
}
