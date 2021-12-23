using System;
using System.Collections.Generic;
using FinanceAccounting.Application.Common.DataTransferObjects;
using MediatR;

namespace FinanceAccounting.Application.Operations.Queries.GetDaysOperationsReport
{
    public record GetDaysOperationsReportCommand(int UserId, DateTime Date) : IRequest<IEnumerable<OperationDto>>;
}
