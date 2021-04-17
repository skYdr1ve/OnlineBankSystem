using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBankSystem.Web.Api.Models
{
    public class DepositMoneyModel
    {
        [Required]
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
