using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Core.Repositories
{
    public interface ICountryCurrencyCodeRepository
    {
        Task<CountryCurrencyCode> Find(int number, bool track = false);
        Task<IEnumerable<CountryCurrencyCode>> List(Expression<Func<CountryCurrencyCode, bool>> predicate = null,
            Func<IQueryable<CountryCurrencyCode>, IOrderedQueryable<CountryCurrencyCode>> orderBy = null, bool track = false);

        Task Add(CountryCurrencyCode item);
        Task Remove(CountryCurrencyCode entity);
    }
}
