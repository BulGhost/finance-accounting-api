using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Application.Common.DataTransferObjects.Operation;
using FinanceAccounting.Application.Operations.Commands.AddOperations;
using FinanceAccounting.Application.Operations.Commands.DeleteOperations;
using FinanceAccounting.Application.Operations.Commands.UpdateOperations;
using FinanceAccounting.Application.Operations.Queries.GetDaysOperationsReport;
using FinanceAccounting.Application.Operations.Queries.GetPeriodsOperationsReport;
using FinanceAccounting.WebApi.Controllers.Base;
using FinanceAccounting.WebApi.ViewModels;
using FinanceAccounting.WebApi.ViewModels.HelperClasses;
using MediatR;

namespace FinanceAccounting.WebApi.Controllers
{
    public class OperationController : BaseCrudController
    {
        public OperationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetDaysOperationsReport(
            [FromQuery] DateTime date, CancellationToken cancellationToken = default)
        {
            var query = new GetDaysOperationsQuery(UserId, date);
            var operations = await Mediator.Send(query, cancellationToken);
            OperationsReport report = new ReportBuilder().BuildOperationsReport(operations);
            return Ok(report);
        }

        [HttpGet]
        public async Task<IActionResult> GetPeriodsOperationsReport(
            [FromQuery] DateTime startDate, [FromQuery] DateTime finalDdate, CancellationToken cancellationToken = default)
        {
            var query = new GetPeriodsOperationsQuery(UserId, startDate, finalDdate);
            var operations = await Mediator.Send(query, cancellationToken);
            OperationsReport report = new ReportBuilder().BuildOperationsReport(operations);
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> AddOperations(
            [FromBody] CreateOperationDto[] operations, CancellationToken cancellationToken = default)
        {
            var command = new AddOperationsCommand(UserId, operations);
            var addedOperations = await Mediator.Send(command, cancellationToken);
            return StatusCode(201, addedOperations);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOperations(
            [FromBody] UpdateOperationDto[] operations, CancellationToken cancellationToken = default)
        {
            var command = new UpdateOperationsCommand(UserId, operations);
            var updatedOperations = await Mediator.Send(command, cancellationToken);
            return StatusCode(202, updatedOperations);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOperations(
            [FromBody] int[] operationIds, CancellationToken cancellationToken = default)
        {
            var command = new DeleteOperationsCommand(UserId, operationIds);
            var deletedOperationIds = await Mediator.Send(command, cancellationToken);
            return StatusCode(202, deletedOperationIds);
        }
    }
}
