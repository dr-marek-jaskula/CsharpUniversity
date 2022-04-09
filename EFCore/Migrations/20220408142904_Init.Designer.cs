﻿// <auto-generated />
using System;
using EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCore.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20220408142904_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EFCore.Data_models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte?>("Building")
                        .HasColumnType("TINYINT");

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Country")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte?>("Flat")
                        .HasColumnType("TINYINT");

                    b.Property<string>("Street")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("DATE");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("VARCHAR(7)")
                        .HasComment("Male, Female or Unknown");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("VARCHAR(8)")
                        .HasDefaultValue("Standard");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasFilter("[AddressId] IS NOT NULL");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("DATE");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("VARCHAR(7)")
                        .HasComment("Male, Female or Unknown");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("DATE");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<int?>("SalaryId")
                        .HasColumnType("int");

                    b.Property<int?>("ShopId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasFilter("[AddressId] IS NOT NULL");

                    b.HasIndex("ManagerId");

                    b.HasIndex("SalaryId")
                        .IsUnique()
                        .HasFilter("[SalaryId] IS NOT NULL");

                    b.HasIndex("ShopId");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("INT");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("DATE");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ShopId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("VARCHAR(10)")
                        .HasComment("Received, InProgress, Done or Rejected");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PaymentId")
                        .IsUnique()
                        .HasFilter("[PaymentId] IS NOT NULL");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShopId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("DATE");

                    b.Property<decimal?>("Discount")
                        .HasPrecision(3, 2)
                        .HasColumnType("decimal(3,2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasComment("Received, InProgress, Done or Rejected");

                    b.Property<decimal>("Total")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id");

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Product_Amount", b =>
                {
                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ShopId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("INT");

                    b.HasKey("ProductId", "ShopId");

                    b.HasIndex("ShopId");

                    b.ToTable("Product_Amount", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Product_Tag", b =>
                {
                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("TagId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("Product_Tag", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<byte>("Stars")
                        .HasColumnType("TINYINT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProductId");

                    b.ToTable("Review", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Salary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BaseSalary")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasDefaultValue(0);

                    b.Property<int>("DiscretionaryBonus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasDefaultValue(0);

                    b.Property<int>("IncentiveBonus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasDefaultValue(0);

                    b.Property<int>("TaskBonus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("Salary", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Salary_Transfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("DATE");

                    b.Property<bool>("IsDiscretionaryBonus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsIncentiveBonus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsTaskBonus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false);

                    b.Property<int?>("SalaryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SalaryId");

                    b.ToTable("Salary_Transfer", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasFilter("[AddressId] IS NOT NULL");

                    b.ToTable("Shop", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ProductTag")
                        .IsRequired()
                        .HasColumnType("VARCHAR(9)");

                    b.HasKey("Id");

                    b.ToTable("Tag", (string)null);
                });

            modelBuilder.Entity("EFCore.Data_models.Customer", b =>
                {
                    b.HasOne("EFCore.Data_models.Address", "Address")
                        .WithOne("Customer")
                        .HasForeignKey("EFCore.Data_models.Customer", "AddressId");

                    b.HasOne("EFCore.Data_models.Employee", null)
                        .WithMany("Customers")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("EFCore.Data_models.Employee", b =>
                {
                    b.HasOne("EFCore.Data_models.Address", "Address")
                        .WithOne("Employee")
                        .HasForeignKey("EFCore.Data_models.Employee", "AddressId");

                    b.HasOne("EFCore.Data_models.Employee", "Manager")
                        .WithMany("Subordinates")
                        .HasForeignKey("ManagerId");

                    b.HasOne("EFCore.Data_models.Salary", "Salary")
                        .WithOne("Employee")
                        .HasForeignKey("EFCore.Data_models.Employee", "SalaryId");

                    b.HasOne("EFCore.Data_models.Shop", "Shop")
                        .WithMany("Employees")
                        .HasForeignKey("ShopId");

                    b.Navigation("Address");

                    b.Navigation("Manager");

                    b.Navigation("Salary");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("EFCore.Data_models.Order", b =>
                {
                    b.HasOne("EFCore.Data_models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("EFCore.Data_models.Payment", "Payment")
                        .WithOne("Order")
                        .HasForeignKey("EFCore.Data_models.Order", "PaymentId");

                    b.HasOne("EFCore.Data_models.Product", "Product")
                        .WithMany("Order")
                        .HasForeignKey("ProductId");

                    b.HasOne("EFCore.Data_models.Shop", "Shop")
                        .WithMany("Orders")
                        .HasForeignKey("ShopId");

                    b.Navigation("Customer");

                    b.Navigation("Payment");

                    b.Navigation("Product");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("EFCore.Data_models.Product_Amount", b =>
                {
                    b.HasOne("EFCore.Data_models.Product", "Product")
                        .WithMany("ProductAmounts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCore.Data_models.Shop", "Shop")
                        .WithMany("ProductAmounts")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("EFCore.Data_models.Product_Tag", b =>
                {
                    b.HasOne("EFCore.Data_models.Product", "Product")
                        .WithMany("Product_Tags")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCore.Data_models.Tag", "Tag")
                        .WithMany("Product_Tags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("EFCore.Data_models.Review", b =>
                {
                    b.HasOne("EFCore.Data_models.Employee", "Employee")
                        .WithMany("Reviews")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("EFCore.Data_models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId");

                    b.Navigation("Employee");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EFCore.Data_models.Salary_Transfer", b =>
                {
                    b.HasOne("EFCore.Data_models.Salary", "Salary")
                        .WithMany("SalaryTransfer")
                        .HasForeignKey("SalaryId");

                    b.Navigation("Salary");
                });

            modelBuilder.Entity("EFCore.Data_models.Shop", b =>
                {
                    b.HasOne("EFCore.Data_models.Address", "Address")
                        .WithOne("Shop")
                        .HasForeignKey("EFCore.Data_models.Shop", "AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("EFCore.Data_models.Address", b =>
                {
                    b.Navigation("Customer");

                    b.Navigation("Employee");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("EFCore.Data_models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("EFCore.Data_models.Employee", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Reviews");

                    b.Navigation("Subordinates");
                });

            modelBuilder.Entity("EFCore.Data_models.Payment", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("EFCore.Data_models.Product", b =>
                {
                    b.Navigation("Order");

                    b.Navigation("ProductAmounts");

                    b.Navigation("Product_Tags");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("EFCore.Data_models.Salary", b =>
                {
                    b.Navigation("Employee");

                    b.Navigation("SalaryTransfer");
                });

            modelBuilder.Entity("EFCore.Data_models.Shop", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Orders");

                    b.Navigation("ProductAmounts");
                });

            modelBuilder.Entity("EFCore.Data_models.Tag", b =>
                {
                    b.Navigation("Product_Tags");
                });
#pragma warning restore 612, 618
        }
    }
}