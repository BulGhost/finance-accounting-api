using System;

namespace FinanceAccounting.DataAccess.Exceptions
{
    public class FinanceAccountingException : Exception
    {
        public FinanceAccountingException()
        {
        }

        public FinanceAccountingException(string message) : base(message)
        {
        }

        public FinanceAccountingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
