using FluentValidation;

namespace FinanceAccounting.Application.Common.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .Length(6, 20).WithMessage("Password must be 6 to 20 characters long")
                .Matches("[a-z]+").WithMessage("Password should contain At least one lower case letter")
                .Matches("[A-Z]+").WithMessage("Password should contain At least one upper case letter")
                .Matches("[0-9]+").WithMessage("Password should contain At least one numeric value");
        }
    }
}
