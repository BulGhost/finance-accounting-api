using System;
using System.Collections.Generic;
using FinanceAccounting.Application.Common.DataTransferObjects.OperationDto;
using MediatR;

namespace FinanceAccounting.Application.Operations.Queries.GetDaysOperationsReport
{
    public record GetDaysOperationsQuery(int UserId, DateTime Date) : IRequest<IEnumerable<OperationDto>>;
}
