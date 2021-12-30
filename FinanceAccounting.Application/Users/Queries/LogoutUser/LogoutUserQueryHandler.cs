using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceAccounting.Application.Users.Queries.LogoutUser
{
    public class LogoutUserQueryHandler : IRequestHandler<LogoutUserQuery>
    {
        private readonly SignInManager<User> _signInManager;

        public LogoutUserQueryHandler(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Unit> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync(); //TODO: Delete tokens
            return Unit.Value;
        }
    }
}
