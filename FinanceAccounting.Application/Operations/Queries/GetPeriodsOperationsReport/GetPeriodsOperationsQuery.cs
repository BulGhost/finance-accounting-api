using System;
using System.Collections.Generic;
using FinanceAccounting.Application.Common.DataTransferObjects.Operation;
using MediatR;

namespace FinanceAccounting.Application.Operations.Queries.GetPeriodsOperationsReport
{
    public record GetPeriodsOperationsQuery(int UserId, DateTime StartDate, DateTime FinalDate)
        : IRequest<IEnumerable<OperationDto>>;
}
