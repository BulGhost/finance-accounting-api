using System;

namespace FinanceAccounting.Application.Common.Exceptions
{
    public class UserAuthenticationException : Exception
    {
        public UserAuthenticationException() : base("Unsuccessful login attempt")
        {
        }
    }
}
