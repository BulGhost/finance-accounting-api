using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories(OperationType operationType)
        {
            var query = new GetCategoriesQuery(UserId, operationType);
            var viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> CreateCategories(
            [FromBody] CreateCategoryDto[] categories)
        {
            var command = new CreateCategoriesCommand(UserId, categories);
            var addedCategories = await Mediator.Send(command);
            return Ok(addedCategories);
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> UpdateCategories(
            [FromBody] UpdateCategoryDto[] categories)
        {
            var command = new UpdateCategoriesCommand(UserId, categories);
            var updatedCategories = await Mediator.Send(command);
            return Ok(updatedCategories);
        }

        [HttpDelete]
        public async Task<ActionResult<IEnumerable<int>>> DeleteCategories([FromBody] int[] categoryIds)
        {
            var command = new DeleteCategoriesCommand(UserId, categoryIds);
            var deletedCategoryIds = await Mediator.Send(command);
            return Ok(deletedCategoryIds);
        }
    }
}
