using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<OperationsReport>> GetDaysOperationsReport([FromQuery] DateTime date)
        {
            var query = new GetDaysOperationsQuery(UserId, date);
            var operations = await Mediator.Send(query);
            OperationsReport report = new ReportBuilder().BuildOperationsReport(operations);
            return Ok(report);
        }

        [HttpGet]
        public async Task<ActionResult<OperationsReport>> GetPeriodsOperationsReport(
            [FromQuery] DateTime startDate, [FromQuery] DateTime finalDdate)
        {
            var query = new GetPeriodsOperationsQuery(UserId, startDate, finalDdate);
            var operations = await Mediator.Send(query);
            OperationsReport report = new ReportBuilder().BuildOperationsReport(operations);
            return Ok(report);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<OperationDto>>> AddOperations([FromBody] CreateOperationDto[] operations)
        {
            var command = new AddOperationsCommand(UserId, operations);
            var addedOperations = await Mediator.Send(command);
            return Ok(addedOperations);
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<OperationDto>>> UpdateOperations([FromBody] UpdateOperationDto[] operations)
        {
            var command = new UpdateOperationsCommand(UserId, operations);
            var updatedOperations = await Mediator.Send(command);
            return Ok(updatedOperations);
        }

        [HttpDelete]
        public async Task<ActionResult<IEnumerable<int>>> DeleteOperations([FromBody] int[] operationIds)
        {
            var command = new DeleteOperationsCommand(UserId, operationIds);
            var deletedOperationIds = await Mediator.Send(command);
            return Ok(deletedOperationIds);
        }
    }
}
