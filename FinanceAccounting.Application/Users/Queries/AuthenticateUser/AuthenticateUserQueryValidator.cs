﻿using FluentValidation;

namespace FinanceAccounting.Application.Users.Queries.AuthenticateUser
{
    public class AuthenticateUserQueryValidator : AbstractValidator<AuthenticateUserQuery>
    {
        public AuthenticateUserQueryValidator()
        {
            RuleFor(query => query.UserName).NotEmpty();
            RuleFor(query => query.Password).NotEmpty();
        }
    }
}
