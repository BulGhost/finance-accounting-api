using System.Collections.Generic;
using MediatR;

namespace FinanceAccounting.Application.Operations.Commands.DeleteOperations
{
    public record DeleteOperationsCommand(int UserId, int[] OperationIds) : IRequest<IEnumerable<int>>;
}
