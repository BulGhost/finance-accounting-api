using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Application.Abstractions.Repo;
using FinanceAccounting.Application.Common.Models;
using MediatR;

namespace FinanceAccounting.Application.Users.Queries.LogoutUser
{
    public class LogoutUserQueryHandler : IRequestHandler<LogoutUserQuery>
    {
        private readonly IRefreshTokenRepo _refreshTokenRepo;

        public LogoutUserQueryHandler(IRefreshTokenRepo refreshTokenRepo)
        {
            _refreshTokenRepo = refreshTokenRepo;
        }

        public async Task<Unit> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
        {
            var activeTokens = await _refreshTokenRepo.FindAllActiveTokensByUserIdAsync(request.UserId, cancellationToken);
            foreach (RefreshToken token in activeTokens)
            {
                token.IsRevoked = true;
            }

            await _refreshTokenRepo.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
