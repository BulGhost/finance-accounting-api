using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FinanceAccounting.Models
{
    [Index(nameof(UserId), Name = "IX_IncomeRecords_UserId")]
    public class IncomeRecord : EntityBase
    {
        [Required]
        public Guid UserId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        public Guid IncomeCategoryId { get; set; }

        [ForeignKey(nameof(IncomeCategoryId))]
        public IncomeCategory CategoryNavigation { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [StringLength(150)]
        public string Details { get; set; }
    }
}
