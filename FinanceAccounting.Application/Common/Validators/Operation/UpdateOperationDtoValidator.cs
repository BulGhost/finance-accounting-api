using System;
using FinanceAccounting.Application.Common.DataTransferObjects.OperationDto;
using FluentValidation;

namespace FinanceAccounting.Application.Common.Validators.Operation
{
    public class UpdateOperationDtoValidator : AbstractValidator<UpdateOperationDto>
    {
        public UpdateOperationDtoValidator()
        {
            RuleFor(o => o.Id).GreaterThan(0);
            RuleFor(o => o.Date).NotEmpty().LessThan(DateTime.Today.AddDays(1));
            RuleFor(o => o.CategoryId).GreaterThan(0);
            RuleFor(o => o.Sum).GreaterThan(0);
        }
    }
}
