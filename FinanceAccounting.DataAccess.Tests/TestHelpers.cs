using System.IO;
using FinanceAccounting.DataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace FinanceAccounting.DataAccess.Tests
{
    public static class TestHelpers
    {
        public static IConfiguration GetConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

        public static BookkeepingDbContext GetContext(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookkeepingDbContext>();
            string connectionString = configuration.GetConnectionString("FinanceAccounting");
            optionsBuilder.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure());
            return new BookkeepingDbContext(optionsBuilder.Options);
        }

        public static BookkeepingDbContext GetSecondContext(BookkeepingDbContext oldContext, IDbContextTransaction trans)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookkeepingDbContext>();
            optionsBuilder.UseSqlServer(oldContext.Database.GetDbConnection(),
                sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
            var context = new BookkeepingDbContext(optionsBuilder.Options);
            context.Database.UseTransaction(trans.GetDbTransaction());
            return context;
        }
    }
}
