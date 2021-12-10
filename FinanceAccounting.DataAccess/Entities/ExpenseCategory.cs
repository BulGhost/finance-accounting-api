using System;

namespace FinanceAccounting.DataAccess.Entities
{
    public class ExpenseCategory : EntityBase
    {
        public Guid UserId { get; set; }
        public string CategoryName { get; set; }
    }
}
