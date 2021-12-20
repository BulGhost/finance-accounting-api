using FinanceAccounting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceAccounting.DataAccess.EntityTypeConfigurations
{
    internal class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(c => c.CategoryName).IsUnique();

            builder.HasData(
                new Category { Id = 1, Type = TransactionType.Income, CategoryName = "Salary" },
                new Category { Id = 2, Type = TransactionType.Income, CategoryName = "Passive income" },
                new Category { Id = 3, Type = TransactionType.Income, CategoryName = "Gift" },
                new Category { Id = 4, Type = TransactionType.Income, CategoryName = "Sale of property" },
                new Category { Id = 5, Type = TransactionType.Income, CategoryName = "Part-time" },
                new Category { Id = 6, Type = TransactionType.Income, CategoryName = "Inheritance" },
                new Category { Id = 7, Type = TransactionType.Income, CategoryName = "Rent" },
                new Category { Id = 8, Type = TransactionType.Income, CategoryName = "Subsidy" },
                new Category { Id = 9, Type = TransactionType.Income, CategoryName = "Material aid" },
                new Category { Id = 10, Type = TransactionType.Income, CategoryName = "Pension" },
                new Category { Id = 11, Type = TransactionType.Income, CategoryName = "Scholarship" },
                new Category { Id = 12, Type = TransactionType.Income, CategoryName = "Insurance" },
                new Category { Id = 13, Type = TransactionType.Expense, CategoryName = "Car" },
                new Category { Id = 14, Type = TransactionType.Expense, CategoryName = "Charity" },
                new Category { Id = 15, Type = TransactionType.Expense, CategoryName = "Utilities" },
                new Category { Id = 16, Type = TransactionType.Expense, CategoryName = "Furniture" },
                new Category { Id = 17, Type = TransactionType.Expense, CategoryName = "Medicine" },
                new Category { Id = 18, Type = TransactionType.Expense, CategoryName = "Clothing and Footwear" },
                new Category { Id = 19, Type = TransactionType.Expense, CategoryName = "Nutrition" },
                new Category { Id = 20, Type = TransactionType.Expense, CategoryName = "Gifts" },
                new Category { Id = 21, Type = TransactionType.Expense, CategoryName = "Entertainment" },
                new Category { Id = 22, Type = TransactionType.Expense, CategoryName = "Regular payments" },
                new Category { Id = 23, Type = TransactionType.Expense, CategoryName = "Repair" },
                new Category { Id = 24, Type = TransactionType.Expense, CategoryName = "Hygiene products" },
                new Category { Id = 25, Type = TransactionType.Expense, CategoryName = "Technique" },
                new Category { Id = 26, Type = TransactionType.Expense, CategoryName = "Transport" },
                new Category { Id = 27, Type = TransactionType.Expense, CategoryName = "Services" },
                new Category { Id = 28, Type = TransactionType.Expense, CategoryName = "Household goods" },
                new Category { Id = 29, Type = TransactionType.Expense, CategoryName = "Commission" });
        }
    }
}
