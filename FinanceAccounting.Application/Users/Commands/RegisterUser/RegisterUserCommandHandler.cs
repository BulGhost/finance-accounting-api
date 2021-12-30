using System;
using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Exceptions;
using FinanceAccounting.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceAccounting.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserRegistrationResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepo _repo;

        public RegisterUserCommandHandler(UserManager<User> userManager, IUserRepo repo)
        {
            _userManager = userManager;
            _repo = repo;
        }

        public async Task<UserRegistrationResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await _repo.IsUserWithTheSameEmailAlreadyExists(request.Email))
            {
                throw new UserAlreadyExistsException("Email already exists");
            }

            if (await _repo.IsUserWithTheSameUserNameAlreadyExists(request.UserName))
            {
                throw new UserAlreadyExistsException("UserName already exists");
            }

            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new UserRegistrationResponse
                {
                    UserName = user.UserName,
                    IsSucceeded = true
                };
            }

            throw new Exception("User creation failed");
        }
    }
}
