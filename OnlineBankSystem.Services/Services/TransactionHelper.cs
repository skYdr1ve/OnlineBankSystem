using System;
using OnlineBankSystem.Services.Interfaces;

namespace OnlineBankSystem.Services.Services
{
    public class TransactionHelper : ITransactionHelper
    {
        public string Number()
        {
            return DateTime.Now.ToString("yyMMddHHmmssff");
        }
    }
}
