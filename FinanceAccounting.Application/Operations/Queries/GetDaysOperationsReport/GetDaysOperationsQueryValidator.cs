using FluentValidation;

namespace FinanceAccounting.Application.Operations.Queries.GetDaysOperationsReport
{
    public class GetDaysOperationsQueryValidator : AbstractValidator<GetDaysOperationsQuery>
    {
        public GetDaysOperationsQueryValidator()
        {
            RuleFor(query => query.UserId).GreaterThan(0);
            RuleFor(query => query.Date).NotEmpty();
        }
    }
}
