using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OnlineBankSystem.Infrastructure.Repositories
{
    public class AccountStatusRepository : Repository<AccountStatus>, IAccountStatusRepository
    {
        public AccountStatusRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<AccountStatus> Find(int id, string includeProperties = "", bool track = false)
        {
            var query = track ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AccountStatus>> List(Expression<Func<AccountStatus, bool>> predicate = null, Func<IQueryable<AccountStatus>, 
            IOrderedQueryable<AccountStatus>> orderBy = null, string includeProperties = "", bool track = false)
        {
            var query = track ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }
    }
}
