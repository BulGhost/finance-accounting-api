using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceAccounting.Application.Common.DataTransferObjects.CategoryDto;
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
            foreach (UpdateCategoryDto category in request.Categories)
            {
                Category categoryUpd = await _repo.FindAsync(category.Id, cancellationToken);

                if (categoryUpd == null || categoryUpd.UserId != request.UserId)
                {
                    throw new CategoryNotFoundException(category.Id, category.Name);
                }

                categoryUpd.CategoryName = category.Name;

                await _repo.UpdateAsync(categoryUpd, true, cancellationToken);
                updatedCategories.Add(categoryUpd);
            }

            return _mapper.Map<IEnumerable<CategoryDto>>(updatedCategories);
        }
    }
}
