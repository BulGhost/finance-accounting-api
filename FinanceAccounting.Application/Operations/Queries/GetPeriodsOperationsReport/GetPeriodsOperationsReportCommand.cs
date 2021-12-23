using System;
using System.Collections.Generic;
using FinanceAccounting.Application.Common.DataTransferObjects;
using MediatR;

namespace FinanceAccounting.Application.Operations.Queries.GetPeriodsOperationsReport
{
    public record GetPeriodsOperationsReportCommand(int UserId, DateTime StartDate, DateTime FinalDate) : IRequest<IEnumerable<OperationDto>>;
}
