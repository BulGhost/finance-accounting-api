using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.DataAccess.Repositories.Base;
using FinanceAccounting.Logic.Interfaces.Repository;
using FinanceAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceAccounting.DataAccess.Repositories
{
    public class CategoryRepo : BaseRepository<Category>, ICategoryRepo
    {
        public CategoryRepo(BookkeepingDbContext context) : base(context)
        {
        }

        internal CategoryRepo(DbContextOptions<BookkeepingDbContext> options) : base(options)
        {
        }

        public Task<bool> IsCategoryWasAddedEarlierAsync(int userId, string categoryName, CancellationToken cancellationToken = default)
        {
            return Context.Users
                .Where(u => u.Id == userId && u.Categories.Any(c => c.CategoryName == categoryName))
                .AnyAsync(cancellationToken);
        }

        public async Task AddCategoryToUser(int userId, Category category, CancellationToken cancellationToken)
        {
            BookkeepingUser user = await Context.Users.FindAsync(userId);
            Category existingCategory = await Table
                .Where(c => c.CategoryName == category.CategoryName)
                .SingleOrDefaultAsync(cancellationToken);

            user.Categories.Add(existingCategory ?? category);
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
