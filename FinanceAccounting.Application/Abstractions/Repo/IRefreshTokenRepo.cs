using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Application.Common.Models;
using FinanceAccounting.Domain.Repository;

namespace FinanceAccounting.Application.Abstractions.Repo
{
    public interface IRefreshTokenRepo : IRepository<RefreshToken>
    {
        Task<RefreshToken> FindByTokenString(string token, CancellationToken cancellationToken = default);
    }
}
