using FinanceAccounting.Domain.Exceptions.Base;

namespace FinanceAccounting.Domain.Exceptions
{
    public sealed class CategoryDoesNotBelongToUserException : BadRequestException
    {
        public CategoryDoesNotBelongToUserException(int userId, int categoryId)
            : base($"The category with the identifier {categoryId} does not belong to the user with the identifier {userId}")
        {
        }
    }
}
