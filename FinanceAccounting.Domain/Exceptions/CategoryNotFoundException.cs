using FinanceAccounting.Domain.Exceptions.Base;

namespace FinanceAccounting.Domain.Exceptions
{
    public sealed class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(int categoryId, string categoryName = "")
            : base($"Category \"{categoryName}\" with the id={categoryId} was not found.")
        {
        }
    }
}
