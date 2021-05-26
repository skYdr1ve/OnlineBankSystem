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
    public sealed class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Card> Find(Guid id, string includeProperties = "", bool track = true)
        {
            var query = track ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Card>> List(Expression<Func<Card, bool>> predicate = null,
            Func<IQueryable<Card>, IOrderedQueryable<Card>> orderBy = null,
            string includeProperties = "", bool track = true)
        {
            var query = track ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }

        public async Task<Card> FindByCardNumber(string number, string includeProperties = "", bool track = true)
        {
            var query = track ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return await query.SingleOrDefaultAsync(x => x.Number == number);
        }

        public async Task<List<string>> ListCardsNumber()
        {
            var query = DbSet.AsQueryable().AsNoTracking();

            return await query.Select(x => x.Number).ToListAsync();
        }

        public async Task Update()
        {
            await Commit();
        }
    }
}
