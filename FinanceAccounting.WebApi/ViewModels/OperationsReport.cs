using System.Collections.Generic;
using FinanceAccounting.Application.Common.DataTransferObjects.Operation;

namespace FinanceAccounting.WebApi.ViewModels
{
    public class OperationsReport
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public IEnumerable<OperationDto> Operations { get; set; }
    }
}
