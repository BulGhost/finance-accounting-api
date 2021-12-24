using FluentValidation;

namespace FinanceAccounting.Application.Operations.Queries.GetPeriodsOperationsReport
{
    public class GetPeriodsOperationsQueryValidator : AbstractValidator<GetPeriodsOperationsQuery>
    {
        public GetPeriodsOperationsQueryValidator()
        {
            RuleFor(query => query.UserId).GreaterThan(0);
            RuleFor(query => query.StartDate).NotEmpty();
            RuleFor(query => query.FinalDate).NotEmpty();
        }
    }
}
