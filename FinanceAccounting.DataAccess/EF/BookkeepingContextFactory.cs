using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinanceAccounting.DataAccess.EF
{
    public class BookkeepingContextFactory : IDesignTimeDbContextFactory<BookkeepingContext>
    {
        public BookkeepingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookkeepingContext>();
            const string connectionString = @"Server=.;Database=FinanceAccounting;Trusted_Connection=True;MultipleActiveResultSets=True";
            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            return new BookkeepingContext(optionsBuilder.Options);
        }
    }
}
