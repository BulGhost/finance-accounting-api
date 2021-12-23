using System.Collections.Generic;
using FinanceAccounting.Application.Common.DataTransferObjects;
using FinanceAccounting.Domain.Entities;
using MediatR;

namespace FinanceAccounting.Application.Categories.Queries.GetListOfCategories
{
    public record GetCategoriesQuery(int UserId, OperationType OperationType) : IRequest<IEnumerable<CategoryDto>>;
}
