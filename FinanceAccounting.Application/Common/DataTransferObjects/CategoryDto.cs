using FinanceAccounting.Application.Common.Mappings;
using FinanceAccounting.Domain.Entities;

namespace FinanceAccounting.Application.Common.DataTransferObjects
{
    public class CategoryDto : IMapFrom<Category>
    {
        public int Id { get; set; }
        public OperationType Type { get; set; }
        public string CategoryName { get; set; }
    }
}