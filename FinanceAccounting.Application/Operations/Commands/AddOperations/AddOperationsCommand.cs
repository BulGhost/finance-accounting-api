using System.Collections.Generic;
using FinanceAccounting.Application.Common.DataTransferObjects.Operation;
using MediatR;

namespace FinanceAccounting.Application.Operations.Commands.AddOperations
{
    public record AddOperationsCommand(int UserId, CreateOperationDto[] Operations)
        : IRequest<IEnumerable<OperationDto>>;
}
