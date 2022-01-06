using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Application.Abstractions.Security;
using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using FinanceAccounting.Application.Common.Exceptions;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceAccounting.Application.Users.Queries.AuthenticateUser
{
    public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, UserAuthenticationResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthenticateUserQueryHandler(UserManager<User> userManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<UserAuthenticationResponse> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new UserNotFoundException(request.UserName);
            }

            bool isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isCorrectPassword) throw new UserAuthenticationException();

            return await _tokenGenerator.CreateTokensAsync(user, cancellationToken);
        }
    }
}
