using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceAccounting.Application.Common.DataTransferObjects.CategoryDto;
using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Exceptions;
using FinanceAccounting.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;

[assembly: InternalsVisibleTo("FinanceAccounting.Application.Tests")]

namespace FinanceAccounting.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserRegistrationResponse>
    {
        internal static readonly List<CreateCategoryDto> _baseCategories = new()
        {
            new CreateCategoryDto { Type = OperationType.Income, Name = "Salary" },
            new CreateCategoryDto { Type = OperationType.Income, Name = "Passive income" },
            new CreateCategoryDto { Type = OperationType.Income, Name = "Gift" },
            new CreateCategoryDto { Type = OperationType.Income, Name = "Sale of property" },
            new CreateCategoryDto { Type = OperationType.Income, Name = "Part-time" },
            new CreateCategoryDto { Type = OperationType.Income, Name = "Inheritance" },
            new CreateCategoryDto { Type = OperationType.Income, Name = "Rent" },
            new CreateCategoryDto { Type = OperationType.Income, Name = "Subsidy" },
            new CreateCategoryDto { Type = OperationType.Income, Name = "Material aid" },
            new CreateCategoryDto { Type = OperationType.Income, Name = "Pension" },
            new CreateCategoryDto { Type = OperationType.Income, Name = "Scholarship" },
            new CreateCategoryDto { Type = OperationType.Income, Name = "Insurance" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Car" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Charity" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Utilities" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Furniture" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Medicine" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Clothing and Footwear" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Nutrition" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Gifts" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Entertainment" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Regular payments" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Repair" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Hygiene products" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Technique" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Transport" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Services" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Household goods" },
            new CreateCategoryDto { Type = OperationType.Expense, Name = "Commission" }
        };

        private readonly UserManager<User> _userManager;
        private readonly ICategoryRepo _categoryRepo;

        public RegisterUserCommandHandler(UserManager<User> userManager, ICategoryRepo categoryRepo)
        {
            _userManager = userManager;
            _categoryRepo = categoryRepo;
        }

        public async Task<UserRegistrationResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User {Email = request.Email, UserName = request.UserName};
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                string errorMessage = GetErrorMessage(result.Errors.FirstOrDefault());
                throw new UserCreationException(errorMessage);
            }

            if (request.AddBaseCategories)
            {
                await AddBaseCategoriesToUser(user, cancellationToken);
            }

            return new UserRegistrationResponse
            {
                UserName = user.UserName,
                IsSucceeded = true
            };
        }

        private string GetErrorMessage(IdentityError error)
        {
            return error?.Code switch
            {
                nameof(IdentityErrorDescriber.DuplicateEmail) => "A user with this email already exists",
                nameof(IdentityErrorDescriber.DuplicateUserName) => "A user with this name already exists",
                nameof(IdentityErrorDescriber.PasswordTooShort) => "Password must be at least 6 characters long",
                nameof(IdentityErrorDescriber.PasswordRequiresDigit) => "Password must contain at least one numeric value",
                nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric) => "Password must contain at least one non-alphanumeric character",
                nameof(IdentityErrorDescriber.PasswordRequiresLower) => "Password must contain at least one lower case letter",
                nameof(IdentityErrorDescriber.PasswordRequiresUpper) => "Password must contain at least one upper case letter",
                _ => "Something went wrong"
            };
        }

        private Task AddBaseCategoriesToUser(User user, CancellationToken cancellationToken)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<CreateCategoryDto, Category>()
                    .ForMember(c => c.UserId, opt => opt.MapFrom(_ => user.Id))
                    .ForMember(c => c.CategoryName, opt => opt.MapFrom(dto => dto.Name)));

            IMapper mapper = config.CreateMapper();
            var baseCategories = mapper.Map<IEnumerable<Category>>(_baseCategories);
            return _categoryRepo.AddRangeAsync(baseCategories, true, cancellationToken);
        }
    }
}
