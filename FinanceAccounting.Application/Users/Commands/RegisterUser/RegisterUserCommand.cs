using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using MediatR;

namespace FinanceAccounting.Application.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(string UserName, string Email, string Password, string ConfirmPassword,
        bool AddBaseCategories = false) : IRequest<UserRegistrationResponse>;
}
