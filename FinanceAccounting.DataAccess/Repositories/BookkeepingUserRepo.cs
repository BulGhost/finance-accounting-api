using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.DataAccess.Repositories.Base;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Repository;
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

        public override Task<BookkeepingUser> FindAsync(int? id, CancellationToken cancellationToken = default) =>
            Table
                .Where(user => user.Id == id)
                .Include(user => user.Categories)
                .Include(user => user.Operations)
                .FirstOrDefaultAsync(cancellationToken);

        public override Task<BookkeepingUser> FindAsNoTrackingAsync(int id, CancellationToken cancellationToken = default) =>
            Table
                .Where(user => user.Id == id)
                .Include(user => user.Categories)
                .Include(user => user.Operations)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(cancellationToken);

        public override async Task<IEnumerable<BookkeepingUser>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await Table
                .Include(user => user.Categories)
                .Include(user => user.Operations)
                .ToListAsync(cancellationToken);
    }
}
