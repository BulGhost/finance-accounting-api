using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceAccounting.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserRegistrationResponse>
    {
        private readonly UserManager<User> _userManager;

        public RegisterUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserRegistrationResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                string errorMessage = GetErrorMessage(result.Errors.FirstOrDefault());
                throw new UserCreationException(errorMessage);
            }

            return new UserRegistrationResponse
            {
                UserName = user.UserName,
                IsSucceeded = true
            };
        }

        private string GetErrorMessage(IdentityError error)
        {
            return error?.Code switch
            {
                nameof(IdentityErrorDescriber.DuplicateEmail) => "A user with this email already exists",
                nameof(IdentityErrorDescriber.DuplicateUserName) => "A user with this name already exists",
                nameof(IdentityErrorDescriber.PasswordTooShort) => "Password must be at least 6 characters long",
                nameof(IdentityErrorDescriber.PasswordRequiresDigit) => "Password must contain at least one numeric value",
                nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric) => "Password must contain at least one non-alphanumeric character",
                nameof(IdentityErrorDescriber.PasswordRequiresLower) => "Password must contain at least one lower case letter",
                nameof(IdentityErrorDescriber.PasswordRequiresUpper) => "Password must contain at least one upper case letter",
                _ => "Something went wrong"
            };
        }
    }
}
