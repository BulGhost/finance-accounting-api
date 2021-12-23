using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceAccounting.Domain.Entities;

namespace FinanceAccounting.Domain.Repository
{
    public interface IOperationRepo : IRepository<Operation>
    {
        Task<OperationType> GetOperationTypeByCategoryId(int categoryId);
        Task<IEnumerable<Operation>> GetUserOperationsOnDate(int userId, DateTime date);
        Task<IEnumerable<Operation>> GetUserOperationsOnDateRange(int userId, DateTime startDate, DateTime finalDate);
    }
}
