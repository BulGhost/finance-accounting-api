﻿// <auto-generated />
using System;
using FinanceAccounting.DataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinanceAccounting.DataAccess.Migrations
{
    [DbContext(typeof(BookkeepingDbContext))]
    partial class BookkeepingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookkeepingUserCategory", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UsersCategories");
                });

            modelBuilder.Entity("FinanceAccounting.Models.BookkeepingUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FinanceAccounting.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryName")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Salary",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Passive income",
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Gift",
                            Type = 0
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "Sale of property",
                            Type = 0
                        },
                        new
                        {
                            Id = 5,
                            CategoryName = "Part-time",
                            Type = 0
                        },
                        new
                        {
                            Id = 6,
                            CategoryName = "Inheritance",
                            Type = 0
                        },
                        new
                        {
                            Id = 7,
                            CategoryName = "Rent",
                            Type = 0
                        },
                        new
                        {
                            Id = 8,
                            CategoryName = "Subsidy",
                            Type = 0
                        },
                        new
                        {
                            Id = 9,
                            CategoryName = "Material aid",
                            Type = 0
                        },
                        new
                        {
                            Id = 10,
                            CategoryName = "Pension",
                            Type = 0
                        },
                        new
                        {
                            Id = 11,
                            CategoryName = "Scholarship",
                            Type = 0
                        },
                        new
                        {
                            Id = 12,
                            CategoryName = "Insurance",
                            Type = 0
                        },
                        new
                        {
                            Id = 13,
                            CategoryName = "Car",
                            Type = 1
                        },
                        new
                        {
                            Id = 14,
                            CategoryName = "Charity",
                            Type = 1
                        },
                        new
                        {
                            Id = 15,
                            CategoryName = "Utilities",
                            Type = 1
                        },
                        new
                        {
                            Id = 16,
                            CategoryName = "Furniture",
                            Type = 1
                        },
                        new
                        {
                            Id = 17,
                            CategoryName = "Medicine",
                            Type = 1
                        },
                        new
                        {
                            Id = 18,
                            CategoryName = "Clothing and Footwear",
                            Type = 1
                        },
                        new
                        {
                            Id = 19,
                            CategoryName = "Nutrition",
                            Type = 1
                        },
                        new
                        {
                            Id = 20,
                            CategoryName = "Gifts",
                            Type = 1
                        },
                        new
                        {
                            Id = 21,
                            CategoryName = "Entertainment",
                            Type = 1
                        },
                        new
                        {
                            Id = 22,
                            CategoryName = "Regular payments",
                            Type = 1
                        },
                        new
                        {
                            Id = 23,
                            CategoryName = "Repair",
                            Type = 1
                        },
                        new
                        {
                            Id = 24,
                            CategoryName = "Hygiene products",
                            Type = 1
                        },
                        new
                        {
                            Id = 25,
                            CategoryName = "Technique",
                            Type = 1
                        },
                        new
                        {
                            Id = 26,
                            CategoryName = "Transport",
                            Type = 1
                        },
                        new
                        {
                            Id = 27,
                            CategoryName = "Services",
                            Type = 1
                        },
                        new
                        {
                            Id = 28,
                            CategoryName = "Household goods",
                            Type = 1
                        },
                        new
                        {
                            Id = 29,
                            CategoryName = "Commission",
                            Type = 1
                        });
                });

            modelBuilder.Entity("FinanceAccounting.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Details")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("Type")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int")
                        .HasComputedColumnSql("dbo.DefineTransactionType([CategoryId])", true);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BookkeepingUserCategory", b =>
                {
                    b.HasOne("FinanceAccounting.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinanceAccounting.Models.BookkeepingUser", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinanceAccounting.Models.Transaction", b =>
                {
                    b.HasOne("FinanceAccounting.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinanceAccounting.Models.BookkeepingUser", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceAccounting.Models.BookkeepingUser", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
