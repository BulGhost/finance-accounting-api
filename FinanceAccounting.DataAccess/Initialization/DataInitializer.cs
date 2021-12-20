using System;
using System.Collections.Generic;
using System.Linq;
using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinanceAccounting.DataAccess.Initialization
{
    public static class DataInitializer
    {
        public static void InitializeData(BookkeepingDbContext context)
        {
            DropAndCreateDatabase(context);
            SeedData(context);
        }

        public static void ClearAndReseedDatabase(BookkeepingDbContext context)
        {
            ClearData(context);
            SeedData(context);
        }

        internal static void DropAndCreateDatabase(BookkeepingDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        internal static void ClearData(BookkeepingDbContext context)
        {
            string[] entities =
            {
                typeof(Transaction).FullName,
                typeof(Category).FullName,
                typeof(BookkeepingUser).FullName
            };

            foreach (string entityName in entities)
            {
                IEntityType entity = context.Model.FindEntityType(entityName);
                string tableName = entity.GetTableName();
                string schemaName = entity.GetSchema();
                string fullyQualifiedTableName = schemaName == null ? tableName : $"{schemaName}.{tableName}";
                context.Database.ExecuteSqlRaw($"DELETE FROM {fullyQualifiedTableName}");
                context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT (\'{fullyQualifiedTableName}\', RESEED, 1);");
            }
        }

        internal static void SeedData(BookkeepingDbContext context)
        {
            try
            {
                ProcessInsert(context, context.Users, TestData.Users);
                ProcessInsert(context, context.Transactions, TestData.Transactions);

                AddCategoriesToUsers(context);
            }
            catch (Exception ex)
            {
                //TODO: Log and handle ex
            }
        }

        private static void ProcessInsert<TEntity>(BookkeepingDbContext context, DbSet<TEntity> table,
            List<TEntity> records) where TEntity : BaseEntity //TODO: Is transaction needed??
        {
            if (table.Any()) return;

            IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using IDbContextTransaction transaction = context.Database.BeginTransaction();
                try
                {
                    IEntityType metaData = context.Model.FindEntityType(typeof(TEntity).FullName!);
                    string schemaName = metaData.GetSchema() ?? "dbo";
                    string tableName = metaData.GetTableName();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {schemaName}.{tableName} ON");
                    table.AddRange(records);
                    context.SaveChanges();
                    context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {schemaName}.{tableName} OFF");
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            });
        }

        private static void AddCategoriesToUsers(BookkeepingDbContext context)
        {
            foreach (BookkeepingUser user in context.Users
                .Include(u => u.Categories))
            {
                var categories = context.Categories;
                foreach (Category category in categories)
                {
                    user.Categories.Add(category);
                }
            }

            context.SaveChanges();
        }
    }
}
