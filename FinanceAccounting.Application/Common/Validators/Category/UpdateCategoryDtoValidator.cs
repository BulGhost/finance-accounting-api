using FinanceAccounting.Application.Common.DataTransferObjects.CategoryDto;
using FluentValidation;

namespace FinanceAccounting.Application.Common.Validators.Category
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
            RuleFor(c => c.Name).NotEmpty().MaximumLength(30);
        }
    }
}
