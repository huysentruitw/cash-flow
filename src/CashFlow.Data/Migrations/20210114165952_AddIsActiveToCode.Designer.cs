﻿// <auto-generated />
using System;
using CashFlow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CashFlow.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210114165952_AddIsActiveToCode")]
    partial class AddIsActiveToCode
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("CashFlow.Data.Abstractions.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9de4b69a-79c4-4613-b2c6-c2145979a158"),
                            DateCreated = new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Name = "Cash",
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("4612dc6d-708f-441f-bd29-50d955221d88"),
                            DateCreated = new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Name = "Zicht",
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("6fa8f317-11bc-40c5-8c3b-c5895cf5e9f4"),
                            DateCreated = new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Name = "Spaar",
                            Type = 3
                        });
                });

            modelBuilder.Entity("CashFlow.Data.Abstractions.Entities.Code", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("Name");

                    b.ToTable("Codes");

                    b.HasData(
                        new
                        {
                            Name = "6000 aankopen",
                            DateCreated = new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            IsActive = false
                        },
                        new
                        {
                            Name = "6100 diensten en diverse goederen",
                            DateCreated = new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            IsActive = false
                        },
                        new
                        {
                            Name = "6560 bankkosten",
                            DateCreated = new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            IsActive = false
                        },
                        new
                        {
                            Name = "6600 uitzonderlijke kosten",
                            DateCreated = new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            IsActive = false
                        },
                        new
                        {
                            Name = "7000 verkopen",
                            DateCreated = new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            IsActive = false
                        },
                        new
                        {
                            Name = "7400 diverse opbrengsten",
                            DateCreated = new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            IsActive = false
                        },
                        new
                        {
                            Name = "7510 ontvangen bankintresten",
                            DateCreated = new DateTimeOffset(new DateTime(2019, 1, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            IsActive = false
                        });
                });

            modelBuilder.Entity("CashFlow.Data.Abstractions.Entities.FinancialYear", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("FinancialYears");
                });

            modelBuilder.Entity("CashFlow.Data.Abstractions.Entities.StartingBalance", b =>
                {
                    b.Property<Guid>("FinancialYearId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("StartingBalanceInCents")
                        .HasColumnType("bigint");

                    b.HasKey("FinancialYearId", "AccountId");

                    b.ToTable("StartingBalances");
                });

            modelBuilder.Entity("CashFlow.Data.Abstractions.Entities.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("CashFlow.Data.Abstractions.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("AmountInCents")
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("EvidenceNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("FinancialYearId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsInternalTransfer")
                        .HasColumnType("bit");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("TransactionDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("EvidenceNumber")
                        .IsUnique()
                        .HasFilter("[EvidenceNumber] IS NOT NULL");

                    b.HasIndex("SupplierId");

                    b.HasIndex("TransactionDate");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CashFlow.Data.Abstractions.Entities.TransactionCode", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset>("DateAssigned")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("TransactionId", "CodeName");

                    b.HasIndex("CodeName");

                    b.ToTable("TransactionCodes");
                });

            modelBuilder.Entity("CashFlow.Data.Abstractions.Entities.TransactionCode", b =>
                {
                    b.HasOne("CashFlow.Data.Abstractions.Entities.Code", null)
                        .WithMany()
                        .HasForeignKey("CodeName")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("CashFlow.Data.Abstractions.Entities.Transaction", null)
                        .WithMany("Codes")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CashFlow.Data.Abstractions.Entities.Transaction", b =>
                {
                    b.Navigation("Codes");
                });
#pragma warning restore 612, 618
        }
    }
}
