using System.Collections.Generic;
using FinanceAccounting.Application.Common.DataTransferObjects;
using MediatR;

namespace FinanceAccounting.Application.Categories.Commands.UpdateCategories
{
    public record UpdateCategoriesCommand(int UserId, (int Id, string Name)[] Categories)
        : IRequest<IEnumerable<CategoryDto>>;
}
