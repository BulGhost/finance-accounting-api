using MediatR;

namespace FinanceAccounting.Application.Users.Queries.LogoutUser
{
    public record LogoutUserQuery(int UserId) : IRequest; //TODO: Is parameter needed?
}
