using System.Collections.Generic;

namespace FinanceAccounting.Domain.Entities
{
    public class BookkeepingUser : BaseEntity
    {
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Operation> Operations { get; set; } = new List<Operation>();
    }
}
