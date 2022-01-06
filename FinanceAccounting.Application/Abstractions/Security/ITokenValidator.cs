using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Application.Users.Commands.RefreshToken;

namespace FinanceAccounting.Application.Abstractions.Security
{
    public interface ITokenValidator
    {
        Task<(bool result, string errorMessage)> IsTokenValid(RefreshTokenCommand refreshTokenCommand, CancellationToken cancellationToken);
    }
}
