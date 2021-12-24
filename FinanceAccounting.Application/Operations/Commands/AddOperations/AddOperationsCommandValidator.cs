using FinanceAccounting.Application.Common.Validators.Operation;
using FluentValidation;

namespace FinanceAccounting.Application.Operations.Commands.AddOperations
{
    public class AddOperationsCommandValidator : AbstractValidator<AddOperationsCommand>
    {
        public AddOperationsCommandValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0);
            RuleForEach(command => command.Operations).SetValidator(new CreateOperationDtoValidator());
        }
    }
}
