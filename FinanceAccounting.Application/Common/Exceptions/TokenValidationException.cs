using System;

namespace FinanceAccounting.Application.Common.Exceptions
{
    public class TokenValidationException : Exception
    {
        public TokenValidationException(string message) : base(message)
        {
        }
    }
}
