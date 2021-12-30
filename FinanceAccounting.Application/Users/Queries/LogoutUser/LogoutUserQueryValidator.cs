using FluentValidation;

namespace FinanceAccounting.Application.Users.Queries.LogoutUser
{
    public class LogoutUserQueryValidator : AbstractValidator<LogoutUserQuery>
    {
        public LogoutUserQueryValidator()
        {
            RuleFor(query => query.UserId).GreaterThan(0);
        }
    }
}
