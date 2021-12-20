using System.Collections.Generic;

namespace FinanceAccounting.Models
{
    public class BookkeepingUser : BaseEntity
    {
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
