﻿using System;
using FinanceAccounting.BusinessLogic.Common.DataTransferObjects.OperationDto;
using FluentValidation;

namespace FinanceAccounting.BusinessLogic.Common.Validators.Operation
{
    internal class CreateOperationDtoValidator : AbstractValidator<CreateOperationDto>
    {
        internal CreateOperationDtoValidator()
        {
            RuleFor(o => o.Date).NotEmpty().LessThan(DateTime.Today.AddDays(1))
                .WithMessage(Resourses.OperationsValidators.DateInFuture);
            RuleFor(o => o.Sum).GreaterThan(0);
        }
    }
}
