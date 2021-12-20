using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.DataAccess.Repositories.Base;
using FinanceAccounting.Logic.Interfaces.Repository;
using FinanceAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceAccounting.DataAccess.Repositories
{
    public class BookkeepingUserRepo : BaseRepository<BookkeepingUser>, IBookkeepingUserRepo
    {
        public BookkeepingUserRepo(BookkeepingDbContext context) : base(context)
        {
        }

        internal BookkeepingUserRepo(DbContextOptions<BookkeepingDbContext> options) : base(options)
        {
        }
    }
}
