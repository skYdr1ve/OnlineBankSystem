using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Core.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : Entity<TKey>
    {
        Task<TEntity> Find(TKey id, string includeProperties = "", bool track = false);
        Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", bool track = false);

        Task Add(TEntity item);
        Task Remove(TEntity entity);

        Task<IDbContextTransaction> BeginTransaction();
        Task EndTransaction();
        Task Rollback();
    }
}
