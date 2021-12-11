using Microsoft.EntityFrameworkCore.Storage;

namespace FinanceAccounting.DataAccess.Exceptions
{
    public class FinanceAccountingRetryLimitExceededException : FinanceAccountingException
    {
        public FinanceAccountingRetryLimitExceededException()
        {
        }

        public FinanceAccountingRetryLimitExceededException(string message) : base(message)
        {
        }

        public FinanceAccountingRetryLimitExceededException(string message, RetryLimitExceededException innerException)
            : base(message, innerException)
        {
        }
    }
}
