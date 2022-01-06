using FinanceAccounting.Application.Common.DataTransferObjects.CategoryDto;
using FluentValidation;

namespace FinanceAccounting.Application.Common.Validators.Category
{
    internal class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        internal UpdateCategoryDtoValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(30);
        }
    }
}
