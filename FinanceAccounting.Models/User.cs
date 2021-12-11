using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceAccounting.Models
{
    public class User : EntityBase
    {
        [Required, StringLength(30)]
        public string Name { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public ICollection<IncomeCategory> IncomeCategories { get; set; }
        public ICollection<ExpenseCategory> ExpenseCategories { get; set; }
        public ICollection<IncomeRecord> IncomeRecords { get; set; }
        public ICollection<ExpenseRecord> ExpenseRecords { get; set; }
    }
}
