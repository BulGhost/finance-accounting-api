using FinanceAccounting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceAccounting.DataAccess.EntityTypeConfigurations
{
    internal class BookkeepingUserConfig : IEntityTypeConfiguration<BookkeepingUser>
    {
        public void Configure(EntityTypeBuilder<BookkeepingUser> builder)
        {
            builder.HasMany(u => u.Categories)
                .WithMany(c => c.Users)
                .UsingEntity(j => j.ToTable("UsersCategories"));

            builder.HasMany(u => u.Transactions)
                .WithOne(t => t.User);
        }
    }
}
