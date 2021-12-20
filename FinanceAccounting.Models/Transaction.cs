using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceAccounting.Models
{
    public class Transaction : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public BookkeepingUser User { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public TransactionType Type
        {
            get => Category.Type;
            private set {}
        }

        [Required]
        public decimal Sum { get; set; }

        [StringLength(150)]
        public string Details { get; set; }
    }
}
