using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Application.Common.Models;
using FinanceAccounting.DataAccess.Exceptions;
using FinanceAccounting.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinanceAccounting.DataAccess.DbContext
{
    public class BookkeepingDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public BookkeepingDbContext(DbContextOptions<BookkeepingDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (RetryLimitExceededException ex)
            {
                //TODO: Log
                throw new DataAccessException("There is a problem with SQl Server.", ex);
            }
            catch (DbUpdateException ex)
            {
                //TODO: Log
                throw new DataAccessException("An error occurred updating the database", ex);
            }
            catch (Exception ex)
            {
                //TODO: Log
                throw new DataAccessException("An error occurred while saving changes to the database.", ex);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
