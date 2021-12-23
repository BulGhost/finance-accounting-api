using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceAccounting.Application.Common.DataTransferObjects;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Exceptions;
using FinanceAccounting.Domain.Repository;
using MediatR;

namespace FinanceAccounting.Application.Categories.Commands.UpdateCategories
{
    public class UpdateCategoriesCommandHandler : IRequestHandler<UpdateCategoriesCommand, IEnumerable<CategoryDto>>
    {
        private readonly ICategoryRepo _repo;
        private readonly IMapper _mapper;

        public UpdateCategoriesCommandHandler(ICategoryRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(UpdateCategoriesCommand request, CancellationToken cancellationToken)
        {
            var updatedCategories = new List<Category>();
            foreach ((int categoryId, string categoryName) in request.Categories)
            {
                Category category = await _repo.FindAsync(categoryId, cancellationToken);

                if (category == null || category.UserId != request.UserId)
                {
                    throw new CategoryNotFoundException(categoryId, categoryName);
                }

                category.CategoryName = categoryName;

                await _repo.UpdateAsync(category, true, cancellationToken);
                updatedCategories.Add(category);
            }

            return _mapper.Map<IEnumerable<CategoryDto>>(updatedCategories);
        }
    }
}
