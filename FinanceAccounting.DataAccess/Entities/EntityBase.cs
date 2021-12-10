using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceAccounting.DataAccess.Entities
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
