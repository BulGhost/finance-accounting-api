using System.Collections.Generic;
using FinanceAccounting.Application.Common.DataTransferObjects;
using FinanceAccounting.Domain.Entities;
using MediatR;

namespace FinanceAccounting.Application.Categories.Commands.CreateCategories
{
    public record CreateCategoriesCommand(int UserId, (OperationType Type, string Name)[] Categories)
        : IRequest<IEnumerable<CategoryDto>>;
}
