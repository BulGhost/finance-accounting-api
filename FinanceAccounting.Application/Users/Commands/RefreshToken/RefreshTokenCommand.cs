using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using MediatR;

namespace FinanceAccounting.Application.Users.Commands.RefreshToken
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<UserAuthenticationResponse>;
}
