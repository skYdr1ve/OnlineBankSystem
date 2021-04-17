using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Services.Interfaces
{
    public interface ICountryCurrencyCodeService
    {
        public Task<IEnumerable<CountryCurrencyCode>> GetCurrenciesAsync();
    }
}
