using FinanceAccounting.Domain.Exceptions.Base;

namespace FinanceAccounting.Domain.Exceptions
{
    public sealed class OperationDoesNotApplyToUserException : BadRequestException
    {
        public OperationDoesNotApplyToUserException(int userId, int operationId)
            : base($"The operation with the identifier {operationId} does not apply to the user with the identifier {userId}")
        {
        }
    }
}   
