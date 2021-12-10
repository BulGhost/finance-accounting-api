using System;

namespace FinanceAccounting.DataAccess.Entities
{
    public class ExpenseRecord
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; }
        public decimal Sum { get; set; }
        public string Details { get; set; }
    }
}
