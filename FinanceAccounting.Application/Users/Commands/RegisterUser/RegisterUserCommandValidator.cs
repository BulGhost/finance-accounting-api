using FinanceAccounting.Application.Common.Validators;
using FluentValidation;

namespace FinanceAccounting.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(command => command.UserName).Matches("^[a-zA-Z][a-zA-Z0-9]{3,20}$")
                .WithMessage("Username must begin with a letter, can contain letters and numbers and must be 3 to 20 characters long");
            RuleFor(command => command.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.Password).Password();
            RuleFor(command => command.ConfirmPassword).Equal(command => command.Password);
        }
    }
}
