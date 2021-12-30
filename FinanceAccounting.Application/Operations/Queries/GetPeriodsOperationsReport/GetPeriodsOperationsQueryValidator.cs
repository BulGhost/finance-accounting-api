using System;
using FluentValidation;

namespace FinanceAccounting.Application.Operations.Queries.GetPeriodsOperationsReport
{
    public class GetPeriodsOperationsQueryValidator : AbstractValidator<GetPeriodsOperationsQuery>
    {
        public GetPeriodsOperationsQueryValidator()
        {
            RuleFor(query => query.UserId).GreaterThan(0);
            RuleFor(query => query.StartDate).NotEmpty().LessThan(query => query.FinalDate);
            RuleFor(query => query.FinalDate).NotEmpty().LessThan(DateTime.Today.AddDays(1));
        }
    }
}
