using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Application.Abstractions.Repo;
using FinanceAccounting.Application.Common.Models;
using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FinanceAccounting.DataAccess.Repositories
{
    public class RefreshTokenRepo : BaseRepository<RefreshToken>, IRefreshTokenRepo
    {
        public RefreshTokenRepo(BookkeepingDbContext context) : base(context)
        {
        }

        internal RefreshTokenRepo(DbContextOptions<BookkeepingDbContext> options) : base(options)
        {
        }

        public Task<RefreshToken> FindByTokenString(string token, CancellationToken cancellationToken = default)
        {
            return Table.FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        }
    }
}
