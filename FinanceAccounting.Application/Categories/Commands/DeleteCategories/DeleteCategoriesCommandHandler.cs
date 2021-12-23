using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Repository;
using MediatR;

namespace FinanceAccounting.Application.Categories.Commands.DeleteCategories
{
    public class DeleteCategoriesCommandHandler : IRequestHandler<DeleteCategoriesCommand, IEnumerable<int>>
    {
        private readonly ICategoryRepo _repo;
        private readonly IMapper _mapper;

        public DeleteCategoriesCommandHandler(ICategoryRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<int>> Handle(DeleteCategoriesCommand request, CancellationToken cancellationToken)
        {
            var deletedCategoryIds = new List<int>();
            foreach (int categoryId in request.CategoryIds)
            {
                Category category = await _repo.FindAsync(categoryId, cancellationToken);

                if (category == null || category.UserId != request.UserId)
                {
                    continue;
                }

                await _repo.DeleteAsync(category, true, cancellationToken);
                deletedCategoryIds.Add(categoryId);
            }

            return deletedCategoryIds;
        }
    }
}
