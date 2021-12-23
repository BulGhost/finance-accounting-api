using System.Collections.Generic;
using System.Linq;
using FinanceAccounting.DataAccess.Exceptions;
using FinanceAccounting.DataAccess.Initialization;
using FinanceAccounting.DataAccess.Repositories;
using FinanceAccounting.DataAccess.Tests.Base;
using FinanceAccounting.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace FinanceAccounting.DataAccess.Tests.IntegrationTests
{
    [Collection("Integration Tests")]
    public class UserTests : BaseTest, IClassFixture<EnsureFinanceAccountingDatabaseTestFixture>
    {
        private readonly IBookkeepingUserRepo _repo;

        public UserTests()
        {
            _repo = new BookkeepingUserRepo(Context);
        }

        public override void Dispose()
        {
            _repo.Dispose();
        }

        [Fact]
        public void ShouldGetAllOfTheUsers()
        {
            var users = Context.Users.ToList();
            Assert.Equal(TestData.Users.Count, users.Count);
        }

        [Fact]
        public void ShouldGetUsersWithIdBiggerThan2()
        {
            var query = Context.Users
                .Where(x => x.Id.CompareTo(2).Equals(1));
            var users = query.ToList();
            Assert.Empty(users);
        }
    }
}
