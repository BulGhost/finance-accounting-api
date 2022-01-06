using FinanceAccounting.Application.Tests.Stubs;
using FinanceAccounting.Application.Users.Queries.LogoutUser;
using FluentValidation.TestHelper;
using Xunit;

namespace FinanceAccounting.Application.Tests.Users.Queries.LogoutUser
{
    public class LogoutUserQueryValidatorTests
    {
        private readonly LogoutUserQueryValidator _validator;

        public LogoutUserQueryValidatorTests()
        {
            var userRepo = new UserRepoStub();
            _validator = new LogoutUserQueryValidator(userRepo);
        }

        [Fact]
        public void Should_have_error_when_user_with_specified_id_does_not_exist()
        {
            var query = new LogoutUserQuery(3);

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.UserId);
        }

        [Fact]
        public void Should_not_have_error_when_query_is_valid()
        {
            var query = new LogoutUserQuery(1);

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
