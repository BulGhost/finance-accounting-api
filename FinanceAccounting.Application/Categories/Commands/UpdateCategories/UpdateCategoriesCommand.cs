using System.Collections.Generic;
using FinanceAccounting.Application.Common.DataTransferObjects.Category;
using MediatR;

namespace FinanceAccounting.Application.Categories.Commands.UpdateCategories
{
    public record UpdateCategoriesCommand(int UserId, UpdateCategoryDto[] Categories)
        : IRequest<IEnumerable<CategoryDto>>;
}
