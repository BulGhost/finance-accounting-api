using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Application.Abstractions.Repo;
using FinanceAccounting.Application.Abstractions.Security;
using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using FinanceAccounting.Application.Common.Exceptions;
using FinanceAccounting.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceAccounting.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, UserAuthenticationResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IJwtVerifier _jwtVerifier;
        private readonly IRefreshTokenRepo _refreshTokenRepo;

        public RefreshTokenCommandHandler(UserManager<User> userManager, IJwtGenerator jwtGenerator,
            IJwtVerifier jwtVerifier, IRefreshTokenRepo refreshTokenRepo)
        {
            _jwtGenerator = jwtGenerator;
            _jwtVerifier = jwtVerifier;
            _refreshTokenRepo = refreshTokenRepo;
            _userManager = userManager;
        }

        public async Task<UserAuthenticationResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            (bool result, string errorMessage) verificationResult = await _jwtVerifier.IsTokenValid(request);
            if (!verificationResult.result)
            {
                throw new TokenVerificationException(verificationResult.errorMessage);
            }

            var storedToken = await _refreshTokenRepo.FindByTokenString(request.RefreshToken, cancellationToken);
            storedToken.IsActive = true;
            await _refreshTokenRepo.SaveChangesAsync(cancellationToken);

            User user = await _userManager.FindByIdAsync(storedToken.UserId.ToString());
            return await _jwtGenerator.CreateTokensAsync(user);
        }
    }
}
