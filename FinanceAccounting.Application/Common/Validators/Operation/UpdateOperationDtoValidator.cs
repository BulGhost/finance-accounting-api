using FinanceAccounting.Application.Common.DataTransferObjects.Operation;
using FluentValidation;

namespace FinanceAccounting.Application.Common.Validators.Operation
{
    public class UpdateOperationDtoValidator : AbstractValidator<UpdateOperationDto>
    {
        public UpdateOperationDtoValidator()
        {
            RuleFor(o => o.Id).GreaterThan(0);
            RuleFor(o => o.Date).NotEmpty();
            RuleFor(o => o.CategoryId).GreaterThan(0);
            RuleFor(o => o.Sum).GreaterThan(0);
        }
    }
}
