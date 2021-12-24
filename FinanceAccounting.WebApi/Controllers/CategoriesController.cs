using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Application.Categories.Commands.CreateCategories;
using FinanceAccounting.Application.Categories.Commands.DeleteCategories;
using FinanceAccounting.Application.Categories.Commands.UpdateCategories;
using FinanceAccounting.Application.Categories.Queries.GetListOfCategories;
using FinanceAccounting.Application.Common.DataTransferObjects.Category;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.WebApi.Controllers.Base;
using MediatR;

namespace FinanceAccounting.WebApi.Controllers
{
    public class CategoriesController : BaseCrudController
    {
        public CategoriesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{operationType}")]
        public async Task<IActionResult> GetCategories(
            OperationType operationType, CancellationToken cancellationToken = default)
        {
            var query = new GetCategoriesQuery(UserId, operationType);
            var viewModel = await Mediator.Send(query, cancellationToken);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategories(
            [FromBody] CreateCategoryDto[] categories, CancellationToken cancellationToken = default)
        {
            var command = new CreateCategoriesCommand(UserId, categories);
            var addedCategories = await Mediator.Send(command, cancellationToken);
            return StatusCode(201, addedCategories);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategories(
            [FromBody] UpdateCategoryDto[] categories, CancellationToken cancellationToken = default)
        {
            var command = new UpdateCategoriesCommand(UserId, categories);
            var updatedCategories = await Mediator.Send(command, cancellationToken);
            return StatusCode(202, updatedCategories);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategories(
            [FromBody] int[] categoryIds, CancellationToken cancellationToken = default)
        {
            var command = new DeleteCategoriesCommand(UserId, categoryIds);
            var deletedCategoryIds = await Mediator.Send(command, cancellationToken);
            return StatusCode(202, deletedCategoryIds);
        }
    }
}
