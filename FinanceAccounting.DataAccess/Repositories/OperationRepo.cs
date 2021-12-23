using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.DataAccess.Repositories.Base;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinanceAccounting.DataAccess.Repositories
{
    public class OperationRepo : BaseRepository<Operation>, IOperationRepo
    {
        public OperationRepo(BookkeepingDbContext context) : base(context)
        {
        }

        internal OperationRepo(DbContextOptions<BookkeepingDbContext> options) : base(options)
        {
        }

        public async Task<OperationType> GetOperationTypeByCategoryId(int categoryId)
        {
            Category category = await Context.Categories.FindAsync(categoryId);
            return category.Type;
        }

        public async Task<IEnumerable<Operation>> GetUserOperationsOnDate(int userId, DateTime date)
        {
            return await Table.Where(operation => operation.Date == date).ToListAsync();
        }

        public async Task<IEnumerable<Operation>> GetUserOperationsOnDateRange(int userId, DateTime startDate, DateTime finalDate)
        {
            return await Table.Where(operation => operation.Date >= startDate && operation.Date <= finalDate)
                .ToListAsync();
        }
    }
}
