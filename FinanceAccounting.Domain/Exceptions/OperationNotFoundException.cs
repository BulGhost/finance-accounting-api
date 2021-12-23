using FinanceAccounting.Domain.Exceptions.Base;

namespace FinanceAccounting.Domain.Exceptions
{
    public sealed class OperationNotFoundException : NotFoundException
    {
        public OperationNotFoundException(int operationId)
            : base($"The operation with the identifier {operationId} was not found.")
        {
        }
    }
}
