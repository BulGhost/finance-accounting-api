using System;
using FinanceAccounting.Models;
using FinanceAccounting.DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinanceAccounting.DataAccess.EF
{
    public class BookkeepingContext : DbContext
    {
        public BookkeepingContext(DbContextOptions<BookkeepingContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<IncomeRecord> IncomeRecords { get; set; }
        public DbSet<ExpenseRecord> ExpenseRecords { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
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
            //TODO: Set up delete behaviour??
        }
    }
}
