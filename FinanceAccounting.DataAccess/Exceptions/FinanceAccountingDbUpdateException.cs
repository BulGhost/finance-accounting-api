using Microsoft.EntityFrameworkCore;

namespace FinanceAccounting.DataAccess.Exceptions
{
    public class FinanceAccountingDbUpdateException : FinanceAccountingException
    {
        public FinanceAccountingDbUpdateException()
        {
        }

        public FinanceAccountingDbUpdateException(string message) : base(message)
        {
        }

        public FinanceAccountingDbUpdateException(string message, DbUpdateException innerException)
            : base(message, innerException)
        {
        }
    }
}
