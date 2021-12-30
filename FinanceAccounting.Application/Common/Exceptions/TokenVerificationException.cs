using System;

namespace FinanceAccounting.Application.Common.Exceptions
{
    public class TokenVerificationException : Exception
    {
        public TokenVerificationException(string message) : base(message)
        {
        }
    }
}
