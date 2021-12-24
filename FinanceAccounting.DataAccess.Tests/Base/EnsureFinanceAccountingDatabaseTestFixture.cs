using System;
using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.DataAccess.Initialization;
using Microsoft.Extensions.Configuration;

namespace FinanceAccounting.DataAccess.Tests.Base
{
    public sealed class EnsureFinanceAccountingDatabaseTestFixture : IDisposable
    {
        public EnsureFinanceAccountingDatabaseTestFixture()
        {
            IConfiguration configuration = TestHelpers.GetConfiguration();
            BookkeepingDbContext context = TestHelpers.GetContext(configuration);
            DbInitializer.ClearAndReseedDatabase(context);
            context.Dispose();
        }

        public void Dispose()
        {
        }
    }
}
