using FinanceAccounting.Application.Common.DataTransferObjects.Operation;
using FluentValidation;

namespace FinanceAccounting.Application.Common.Validators.Operation
{
    public class CreateOperationDtoValidator : AbstractValidator<CreateOperationDto>
    {
        public CreateOperationDtoValidator()
        {
            RuleFor(o => o.Date).NotEmpty();
            RuleFor(o => o.CategoryId).GreaterThan(0);
            RuleFor(o => o.Sum).GreaterThan(0);
        }
    }
}
