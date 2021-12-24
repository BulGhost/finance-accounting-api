using FluentValidation;

namespace FinanceAccounting.Application.Operations.Commands.DeleteOperations
{
    public class DeleteOperationsCommandValidator : AbstractValidator<DeleteOperationsCommand>
    {
        public DeleteOperationsCommandValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0);
            RuleForEach(command => command.OperationIds).GreaterThan(0);
        }
    }
}
