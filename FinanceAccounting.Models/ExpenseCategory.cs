using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FinanceAccounting.Models
{
    [Index(nameof(CategoryName), IsUnique = true, Name = "IX_ExpenseCategory_CategoryName")]
    public class ExpenseCategory : EntityBase
    {
        [Required, StringLength(30)]
        public string CategoryName { get; set; }

        [JsonIgnore]
        public ICollection<User> Users { get; set; }
    }
}
