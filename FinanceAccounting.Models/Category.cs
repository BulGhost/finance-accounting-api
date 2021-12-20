using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinanceAccounting.Models
{
    public class Category : BaseEntity
    {
        [EnumDataType(typeof(TransactionType))]
        public TransactionType Type { get; set; }

        [Required, StringLength(30)]
        public string CategoryName { get; set; }

        [JsonIgnore]
        public ICollection<BookkeepingUser> Users { get; set; } = new List<BookkeepingUser>();
    }
}
