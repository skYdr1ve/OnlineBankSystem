using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Core.Repositories;
using OnlineBankSystem.Services.Interfaces;

namespace OnlineBankSystem.Services.Services
{
    public class CountryCurrencyCodeService : ICountryCurrencyCodeService
    {
        private readonly ICountryCurrencyCodeRepository _countryCurrencyCodeRepository;
        public CountryCurrencyCodeService(ICountryCurrencyCodeRepository countryCurrencyCodeRepository)
        {
            _countryCurrencyCodeRepository = countryCurrencyCodeRepository;
        }

        public async Task<IEnumerable<CountryCurrencyCode>> GetCurrenciesAsync()
        {
            Expression<Func<CountryCurrencyCode, bool>> filter = null;
            return await _countryCurrencyCodeRepository.List(filter);
        }
    }
}
