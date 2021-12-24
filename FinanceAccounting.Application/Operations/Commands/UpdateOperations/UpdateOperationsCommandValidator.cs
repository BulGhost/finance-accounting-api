using FinanceAccounting.Application.Common.Validators.Operation;
using FluentValidation;

namespace FinanceAccounting.Application.Operations.Commands.UpdateOperations
{
    public class UpdateOperationsCommandValidator : AbstractValidator<UpdateOperationsCommand>
    {
        public UpdateOperationsCommandValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0);
            RuleForEach(command => command.Operations).SetValidator(new UpdateOperationDtoValidator());
        }
    }
}
