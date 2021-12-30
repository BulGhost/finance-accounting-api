using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.DataAccess.Exceptions;
using FinanceAccounting.Domain.Entities.Base;
using FinanceAccounting.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinanceAccounting.DataAccess.Repositories.Base
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IEntity<int>, new()
    {
        private readonly bool _disposeContext;
        private bool _isDisposed;

        public BookkeepingDbContext Context { get; }
        public DbSet<T> Table { get; }

        protected BaseRepository(BookkeepingDbContext context)
        {
            Context = context;
            Table = Context.Set<T>();
            _disposeContext = false;
        }

        protected BaseRepository(DbContextOptions<BookkeepingDbContext> options)
            : this(new BookkeepingDbContext(options))
        {
            _disposeContext = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (_disposeContext)
                {
                    Context.Dispose();
                }
            }

            _isDisposed = true;
        }

        ~BaseRepository()
        {
            Dispose(false);
        }

        public virtual async Task<int> AddAsync(T entity, bool persist = true, CancellationToken cancellationToken = default)
        {
            Table.Add(entity);
            return persist ? await SaveChangesAsync(cancellationToken) : 0;
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<T> entities, bool persist = true, CancellationToken cancellationToken = default)
        {
            Table.AddRange(entities);
            return persist ? await SaveChangesAsync(cancellationToken) : 0;
        }

        public virtual async Task<int> UpdateAsync(T entity, bool persist = true, CancellationToken cancellationToken = default)
        {
            Table.Update(entity);
            return persist ? await SaveChangesAsync(cancellationToken) : 0;
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<T> entities, bool persist = true, CancellationToken cancellationToken = default)
        {
            Table.UpdateRange(entities);
            return persist ? await SaveChangesAsync(cancellationToken) : 0;
        }

        public virtual async Task<int> DeleteAsync(int id, bool persist = true, CancellationToken cancellationToken = default)
        {
            var entity = new T { Id = id };
            Context.Entry(entity).State = EntityState.Deleted;
            return persist ? await SaveChangesAsync(cancellationToken) : 0;
        }

        public virtual async Task<int> DeleteAsync(T entity, bool persist = true, CancellationToken cancellationToken = default)
        {
            Table.Remove(entity);
            return persist ? await SaveChangesAsync(cancellationToken) : 0;
        }

        public virtual async Task<int> DeleteRangeAsync(IEnumerable<T> entities, bool persist = true, CancellationToken cancellationToken = default)
        {
            Table.RemoveRange(entities);
            return persist ? await SaveChangesAsync(cancellationToken) : 0;
        }

        public virtual Task<T> FindAsync(int? id, CancellationToken cancellationToken = default) =>
            Table.FindAsync(new object[] {id}, cancellationToken).AsTask();

        public virtual Task<T> FindAsNoTrackingAsync(int id, CancellationToken cancellationToken = default) =>
            Table.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await Table.ToListAsync(cancellationToken);

        public Task ExecuteQueryAsync(string sql, object[] sqlParametersObjects, CancellationToken cancellationToken = default)
            => Context.Database.ExecuteSqlRawAsync(sql, sqlParametersObjects, cancellationToken);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return Context.SaveChangesAsync(cancellationToken);
            }
            catch (FinanceAccountingException ex)
            {
                //TODO: Should handle intelligently - already logged
                throw;
            }
            catch (Exception ex)
            {
                //TODO: Should log and handle intelligently
                throw new FinanceAccountingException("An error occurred updating the database", ex);
            }
        }
    }
}
