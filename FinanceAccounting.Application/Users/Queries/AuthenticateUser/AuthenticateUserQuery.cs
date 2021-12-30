using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using MediatR;

namespace FinanceAccounting.Application.Users.Queries.AuthenticateUser
{
    public record AuthenticateUserQuery(string UserName, string Password) : IRequest<UserAuthenticationResponse>;
}
