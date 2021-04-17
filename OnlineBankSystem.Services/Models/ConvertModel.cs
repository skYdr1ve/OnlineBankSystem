using System;
using System.Text.Json.Serialization;

namespace OnlineBankSystem.Services.Models
{
    public class ConvertModel
    {
        public ConvertModel(string from, string to, decimal rate)
        {
            From = from;
            To = to;
            Rate = rate;
        }

        public string From { get; }

        public string To { get; }

        public decimal Rate { get; }

        public decimal Convert(decimal amount)
        {
            return amount * Rate;
        }
    }
}
