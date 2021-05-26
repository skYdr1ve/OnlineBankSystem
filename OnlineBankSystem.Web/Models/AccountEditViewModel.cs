using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Web.Models
{
    public class AccountEditViewModel
    {
        [HiddenInput] 
        public Guid Id { get; set; }

        [HiddenInput]
        [Display(Name = "Departament")]
        public Guid DepartamentId { get; set; }

        public string Name { get; set; }

        public string CardName { get; set; }

        public int CurrencyNumber { get; set; }

        public List<CountryCurrencyCode> Currencies { get; set; }

        public List<Departament> Departaments { get; set; }
    }
}
