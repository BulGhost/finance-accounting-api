using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using MediatR;

namespace FinanceAccounting.Application.Users.Commands.RefreshToken
{
    public record RefreshTokenCommand(string Token, string RefreshToken) : IRequest<UserAuthenticationResponse>;
}
