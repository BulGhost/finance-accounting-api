using System.Collections.Generic;
using FinanceAccounting.Application.Common.DataTransferObjects.CategoryDto;
using MediatR;

namespace FinanceAccounting.Application.Categories.Commands.CreateCategories
{
    public record CreateCategoriesCommand(int UserId, CreateCategoryDto[] Categories)
        : IRequest<IEnumerable<CategoryDto>>;
}
