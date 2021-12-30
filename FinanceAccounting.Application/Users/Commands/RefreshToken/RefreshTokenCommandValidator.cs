using FluentValidation;

namespace FinanceAccounting.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(command => command.Token).NotEmpty();
            RuleFor(command => command.RefreshToken).NotEmpty();
        }
    }
}
