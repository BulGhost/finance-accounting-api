using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.DataAccess.Repositories.Base;
using FinanceAccounting.Logic.Interfaces.Repository;
using FinanceAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceAccounting.DataAccess.Repositories
{
    public class TransactionRepo : BaseRepository<Transaction>, ITransactionRepo
    {
        public TransactionRepo(BookkeepingDbContext context) : base(context)
        {
        }

        internal TransactionRepo(DbContextOptions<BookkeepingDbContext> options) : base(options)
        {
        }

        public override Task<Transaction> FindAsync(int? id, CancellationToken cancellationToken = default) =>
            Table
                .Where(rec => rec.Id == id)
                .Include(rec => rec.Category)
                .FirstOrDefaultAsync(cancellationToken);

        public override Task<Transaction> FindAsNoTrackingAsync(int id, CancellationToken cancellationToken = default) =>
            Table
                .Where(rec => rec.Id == id)
                .Include(rec => rec.Category)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(cancellationToken);

        public override async Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await Table
                .Include(rec => rec.Category)
                .ToListAsync(cancellationToken);
    }
}
