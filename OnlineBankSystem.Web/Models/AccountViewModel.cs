using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Services.Models;

namespace OnlineBankSystem.Web.Models
{
    public class AccountViewModel
    {
        public Account Account { get; set; }
        public List<TransactionServiceModel> Transactions { get; set; }
        public string FullName { get; set; }
        public int MoneyTransfersCount { get; set; }
        [HiddenInput]
        [Display(Name = "Departament")]
        public Guid DepartamentId { get; set; }
        public List<Departament> Departaments { get; set; }
    }
}
