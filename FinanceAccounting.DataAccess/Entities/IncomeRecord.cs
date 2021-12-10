using System;

namespace FinanceAccounting.DataAccess.Entities
{
    public class IncomeRecord
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public IncomeCategory Category { get; set; }
        public decimal Sum { get; set; }
        public string Details { get; set; }
    }
}
