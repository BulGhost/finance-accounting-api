using FinanceAccounting.Application.Common.Validators.Category;
using FluentValidation;

namespace FinanceAccounting.Application.Categories.Commands.UpdateCategories
{
    public class UpdateCategoriesCommandValidator : AbstractValidator<UpdateCategoriesCommand>
    {
        public UpdateCategoriesCommandValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0);
            RuleForEach(command => command.Categories).SetValidator(new UpdateCategoryDtoValidator());
        }
    }
}
