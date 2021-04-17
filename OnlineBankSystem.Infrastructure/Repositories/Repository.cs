using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Core.Exceptions;

namespace OnlineBankSystem.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> where TEntity : Entity
    {
        public readonly ApplicationDbContext Context;
        public readonly DbSet<TEntity> DbSet;

        public IDbContextTransaction Transaction;

        private bool _isDisposed;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await Commit();
        }

        public async Task Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            await Commit();
        }

        public async Task Commit()
        {
            if (Context == null) return;

            if (_isDisposed) throw new ObjectDisposedException("Repository");

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Update error", ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Commit error", ex);
            }
        }

        public async Task Dispose()
        {
            if (Context == null) return;

            if (!_isDisposed) await Context.DisposeAsync();

            _isDisposed = true;
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            Transaction = await Context.Database.BeginTransactionAsync();
            return Transaction;
        }

        public async Task EndTransaction()
        {
            await Transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await Transaction.RollbackAsync();
        }
    }
}
