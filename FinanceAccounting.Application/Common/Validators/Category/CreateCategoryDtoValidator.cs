using FinanceAccounting.Application.Common.DataTransferObjects.Category;
using FluentValidation;

namespace FinanceAccounting.Application.Common.Validators.Category
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(30);
            RuleFor(c => c.Type).IsInEnum();
        }
    }
}
