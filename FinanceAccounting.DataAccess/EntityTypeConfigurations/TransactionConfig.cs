using FinanceAccounting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceAccounting.DataAccess.EntityTypeConfigurations
{
    internal class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasIndex(t => t.UserId);

            builder.Property(t => t.Date)
                .HasColumnType("date");

            builder.Property(t => t.Type)
                .HasComputedColumnSql("dbo.DefineTransactionType([CategoryId])");
        }
    }
}
