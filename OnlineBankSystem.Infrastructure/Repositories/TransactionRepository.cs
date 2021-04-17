using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Core.Repositories;

namespace OnlineBankSystem.Infrastructure.Repositories
{
    public sealed class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<Transaction> Find(Guid id, string includeProperties = "", bool track = true)
        {
            var query = track ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Transaction>> List(Expression<Func<Transaction, bool>> predicate = null,
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> orderBy = null,
            string includeProperties = "", bool track = true)
        {
            var query = track ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }

        public (IEnumerable<Transaction>, int) PagingList(int offset, int pageSize,
            List<Expression<Func<Transaction, bool>>> predicateList = null,
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> orderBy = null,
            string includeProperties = "", bool track = true)
        {
            var query = track ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();

            foreach (var predicate in predicateList)
            {
                if (predicate != null) query = query.Where(predicate);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null) query = orderBy(query);

            var count = query.Count();
            var items = query.Skip((offset - 1) * pageSize).Take(pageSize).ToList();

            return (items, count);
        }

        public async Task<IEnumerable<Transaction>> Last10(Guid userId)
        {
            var query = Context.Transactions.AsNoTracking()
                .Include(x => x.FromAccount)
                .Include(x => x.ToAccount)
                .Include(x => x.Status)
                .Where(x => x.FromAccount.UserId == userId || x.ToAccount.UserId == userId)
                .OrderByDescending(x => x.Date)
                .Take(10);
            return await query.ToListAsync();
        }
    }
}
