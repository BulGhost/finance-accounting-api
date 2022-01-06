﻿using System.Threading;
using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using FinanceAccounting.Application.Users.Commands.RegisterUser;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace FinanceAccounting.Application.Tests.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandlerTests
    {
        private readonly Mock<UserManager<User>> _userManagerMock = new(
            new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);

        private readonly RegisterUserCommand _command =
            new("UserName", "user@mail.com", "password", "password");

        [Fact]
        public void Fail_user_creation_due_to_duplicate_email()
        {
            IdentityError[] errors = { new() {Code = "DuplicateEmail" } };
            _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(errors));
            var commandHandler = new RegisterUserCommandHandler(_userManagerMock.Object);

            commandHandler.Invoking(h => h.Handle(_command, CancellationToken.None))
                .Should().ThrowAsync<UserCreationException>()
                .WithMessage("A user with this email already exists");
        }

        [Fact]
        public void Fail_user_creation_due_to_duplicate_username()
        {
            IdentityError[] errors = { new() { Code = "DuplicateUserName" } };
            _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(errors));
            var commandHandler = new RegisterUserCommandHandler(_userManagerMock.Object);

            commandHandler.Invoking(h => h.Handle(_command, CancellationToken.None))
                .Should().ThrowAsync<UserCreationException>()
                .WithMessage("A user with this name already exists");
        }

        [Theory]
        [InlineData("PasswordTooShort", "Password must be at least 6 characters long")]
        [InlineData("PasswordRequiresDigit", "Password must contain at least one numeric value")]
        [InlineData("PasswordRequiresNonAlphanumeric", "Password must contain at least one non-alphanumeric character")]
        [InlineData("PasswordRequiresLower", "Password must contain at least one lower case letter")]
        [InlineData("PasswordRequiresUpper", "Password must contain at least one upper case letter")]
        public void Fail_user_creation_due_to_invalid_password(string errorCode, string errorMessage)
        {
            IdentityError[] errors = { new() { Code = errorCode } };
            _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(errors));
            var commandHandler = new RegisterUserCommandHandler(_userManagerMock.Object);

            commandHandler.Invoking(h => h.Handle(_command, CancellationToken.None))
                .Should().ThrowAsync<UserCreationException>()
                .WithMessage(errorMessage);
        }

        [Fact]
        public void Create_new_user_if_command_is_correct()
        {
            _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            var commandHandler = new RegisterUserCommandHandler(_userManagerMock.Object);
            var expectedResult = new UserRegistrationResponse {UserName = "UserName", IsSucceeded = true};

            UserRegistrationResponse actualResult = commandHandler.Handle(_command, CancellationToken.None).Result;

            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}