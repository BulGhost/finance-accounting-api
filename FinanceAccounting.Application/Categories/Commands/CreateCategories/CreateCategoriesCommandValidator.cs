using FinanceAccounting.Application.Common.Validators.Category;
using FluentValidation;

namespace FinanceAccounting.Application.Categories.Commands.CreateCategories
{
    public class CreateCategoriesCommandValidator : AbstractValidator<CreateCategoriesCommand>
    {
        public CreateCategoriesCommandValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0);
            RuleForEach(command => command.Categories).SetValidator(new CreateCategoryDtoValidator());
        }
    }
}
