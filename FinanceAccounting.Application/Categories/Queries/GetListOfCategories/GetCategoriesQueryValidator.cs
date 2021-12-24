using FluentValidation;

namespace FinanceAccounting.Application.Categories.Queries.GetListOfCategories
{
    public class GetCategoriesQueryValidator : AbstractValidator<GetCategoriesQuery>
    {
        public GetCategoriesQueryValidator()
        {
            RuleFor(query => query.UserId).GreaterThan(0);
            RuleFor(query => query.OperationType).IsInEnum();
        }
    }
}
