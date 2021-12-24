using FluentValidation;

namespace FinanceAccounting.Application.Categories.Commands.DeleteCategories
{
    public class DeleteCategoriesCommandValidator : AbstractValidator<DeleteCategoriesCommand>
    {
        public DeleteCategoriesCommandValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0);
            RuleForEach(command => command.CategoryIds).GreaterThan(0);
        }
    }
}
