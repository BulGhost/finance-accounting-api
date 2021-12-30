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
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthenticateUserQueryHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserAuthenticationResponse> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new UserNotFoundException(request.UserName);
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded) throw new UserAuthenticationException();

            return await _jwtGenerator.CreateTokensAsync(user);
        }
    }
}
