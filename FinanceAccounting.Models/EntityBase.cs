using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceAccounting.Models
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
