using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Core.Repositories;

namespace OnlineBankSystem.Infrastructure.Repositories
{
    public class CountryCurrencyCodeRepository : Repository<CountryCurrencyCode>, ICountryCurrencyCodeRepository
    {
        public CountryCurrencyCodeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CountryCurrencyCode> Find(int number, bool track = false)
        {
            var query = track ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();

            return await query.SingleOrDefaultAsync(x => x.Number == number);
        }

        public async Task<IEnumerable<CountryCurrencyCode>> List(Expression<Func<CountryCurrencyCode, bool>> predicate = null, Func<IQueryable<CountryCurrencyCode>, IOrderedQueryable<CountryCurrencyCode>> orderBy = null, bool track = false)
        {
            var query = track ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }
    }
}
