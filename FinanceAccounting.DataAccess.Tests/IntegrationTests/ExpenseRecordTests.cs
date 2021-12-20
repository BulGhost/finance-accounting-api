using System.Collections.Generic;
using System.Linq;
using FinanceAccounting.DataAccess.Exceptions;
using FinanceAccounting.DataAccess.Repositories;
using FinanceAccounting.DataAccess.Tests.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace FinanceAccounting.DataAccess.Tests.IntegrationTests
{
    [Collection("Integration Tests")]
    public class ExpenseRecordTests : BaseTest, IClassFixture<EnsureFinanceAccountingDatabaseTestFixture>
    {
    }
}
