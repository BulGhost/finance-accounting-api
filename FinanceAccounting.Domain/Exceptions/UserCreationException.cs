using FinanceAccounting.Domain.Exceptions.Base;

namespace FinanceAccounting.Domain.Exceptions
{
    public class UserCreationException : BadRequestException
    {
        public UserCreationException(string message) : base(message)
        {
        }
    }
}
