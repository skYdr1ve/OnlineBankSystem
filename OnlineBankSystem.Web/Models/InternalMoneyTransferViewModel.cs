using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Web.Models
{
    public class InternalMoneyTransferViewModel
    {
        [Required]
        [Display(Name = "Source account")]
        public Guid? AccountId { get; set; }

        [Required]
        [Display(Name = "Destination account")]
        public Guid? ToAccountId { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01",
            "79228162514264337593543950335", ErrorMessage =
                "The amount cannot be lower than 0.01", ConvertValueInInvariantCulture = true)]
        public decimal Amount { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
