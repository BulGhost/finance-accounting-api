using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.DataAccess.Exceptions;
using FinanceAccounting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinanceAccounting.DataAccess.DbContext
{
    public class BookkeepingDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookkeepingDbContext(DbContextOptions<BookkeepingDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<BookkeepingUser> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (RetryLimitExceededException ex)
            {
                //TODO: Log and handle
                throw new FinanceAccountingRetryLimitExceededException("There is a problem with SQl Server.", ex);
            }
            catch (DbUpdateException ex)
            {
                //TODO: Log and handle
                throw new FinanceAccountingDbUpdateException("An error occurred updating the database", ex);
            }
            catch (Exception ex)
            {
                //TODO: Log and handle
                throw new FinanceAccountingException("An error occurred updating the database", ex);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
