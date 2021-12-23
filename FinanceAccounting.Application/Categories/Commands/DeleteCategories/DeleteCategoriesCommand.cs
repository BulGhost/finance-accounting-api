using System.Collections.Generic;
using MediatR;

namespace FinanceAccounting.Application.Categories.Commands.DeleteCategories
{
    public record DeleteCategoriesCommand(int UserId, int[] CategoryIds) : IRequest<IEnumerable<int>>;
}
