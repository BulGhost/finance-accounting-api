using System.Collections.Generic;
using FinanceAccounting.Application.Common.DataTransferObjects;
using MediatR;

namespace FinanceAccounting.Application.Operations.Commands.UpdateOperations
{
    public record UpdateOperationsCommand(int UserId, UpdateOperationDto[] Operations)
        : IRequest<IEnumerable<OperationDto>>;
}
