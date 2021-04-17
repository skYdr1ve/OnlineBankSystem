using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Web.Models
{
    public class AccountEditViewModel
    {
        [HiddenInput] 
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CardName { get; set; }

        public int CurrencyNumber { get; set; }

        public List<CountryCurrencyCode> Currencies { get; set; }
    }
}
