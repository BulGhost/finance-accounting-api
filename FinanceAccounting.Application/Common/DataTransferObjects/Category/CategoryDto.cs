using FinanceAccounting.Application.Common.Mappings;
using FinanceAccounting.Domain.Entities;

namespace FinanceAccounting.Application.Common.DataTransferObjects.Category
{
    public class CategoryDto : IMapFrom<Domain.Entities.Category>
    {
        public int Id { get; set; }
        public OperationType Type { get; set; }
        public string CategoryName { get; set; }
    }
}