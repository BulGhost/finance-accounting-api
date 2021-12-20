using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Logic.Interfaces.Repository;
using MediatR;

namespace FinanceAccounting.Logic.Commands.Create.IncomeCategory
{
    public class CreateIncomeCategoryCommandHandler : IRequestHandler<CreateIncomeCategoryCommand, int>
    {
        private readonly ICategoryRepo _repo;

        public CreateIncomeCategoryCommandHandler(ICategoryRepo repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(CreateIncomeCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _repo.IsCategoryWasAddedEarlierAsync(request.UserId, request.CategoryName, cancellationToken))
            {
                return 0;
            }

            var category = new Models.Category { CategoryName = request.CategoryName };

            await _repo.AddCategoryToUser(request.UserId, category, cancellationToken);

            return 1; //TODO: Response??
        }
    }
}
