using System;
using FinanceAccounting.Application.Common.DataTransferObjects.OperationDto;
using FluentValidation;

namespace FinanceAccounting.Application.Common.Validators.Operation
{
    internal class UpdateOperationDtoValidator : AbstractValidator<UpdateOperationDto>
    {
        internal UpdateOperationDtoValidator()
        {
            RuleFor(o => o.Date).NotEmpty().LessThan(DateTime.Today.AddDays(1))
                .WithMessage(Resourses.OperationsValidators.DateInFuture);
            RuleFor(o => o.Sum).GreaterThan(0);
        }
    }
}
