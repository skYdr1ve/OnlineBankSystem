using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Services.Models;

namespace OnlineBankSystem.Services.Mappers
{
    public static class TransactionMapper
    {
        public static IEnumerable<TransactionServiceModel> Map(IEnumerable<Transaction> model)
        {
            return model.Select(Map);
        }

        public static TransactionServiceModel Map(Transaction model)
        {
            return new TransactionServiceModel
            {
                Id = model.Id,
                Number = model.Number,
                Description = model.Description,
                Date = model.Date,
                Amount = model.Amount,
                FromCurrency = model.FromCurrency,
                ToCurrency = model.ToCurrency,
                StatusId = model.StatusId,
                Status = model.Status,
                CardId = model.CardId,
                Card = model.Card,
                ExchangeRate = model.ExchangeRate,
                ToAccountId = model.ToAccountId,
                FromAccountId = model.FromAccountId,
                FromAccount = model.FromAccount,
                ToAccount = model.ToAccount,
                Income = !(model.StatusId == 1 && model.ToAccountId == null),
                Destination = model.Destination
            };
        }
	}
}
