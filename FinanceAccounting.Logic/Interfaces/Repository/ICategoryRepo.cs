using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Models;

namespace FinanceAccounting.Logic.Interfaces.Repository
{
    public interface ICategoryRepo : IRepository<Category>
    {
        Task<bool> IsCategoryWasAddedEarlierAsync(int userId, string categoryName, CancellationToken cancellationToken = default);
        Task AddCategoryToUser(int userId, Category category, CancellationToken cancellationToken);
    }
}
