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
    public class DepartamentRepository : Repository<Departament>, IDepartamentRepository
    {
        public DepartamentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Departament> Find(Guid id, string includeProperties = "", bool track = false)
        {
            var query = track ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Departament>> List(Expression<Func<Departament, bool>> predicate = null, Func<IQueryable<Departament>, IOrderedQueryable<Departament>> orderBy = null, string includeProperties = "", bool track = false)
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
