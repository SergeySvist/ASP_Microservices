﻿// <auto-generated />
using DiscountMS.Host.Domain.DbCtx;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiscountMS.Host.Migrations
{
    [DbContext(typeof(DiscountServiceDbContext))]
    [Migration("20231123091722_AddDiscountTerminationType")]
    partial class AddDiscountTerminationType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DiscountMS.Host.Domain.Model.DiscountAmountType", b =>
                {
                    b.Property<int>("DiscountAmountTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DiscountAmountTypeId"));

                    b.Property<string>("DiscountAmontTypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DiscountAmountTypeId");

                    b.ToTable("DiscountAmountTypes");

                    b.HasData(
                        new
                        {
                            DiscountAmountTypeId = 1,
                            DiscountAmontTypeName = "FixedAmount"
                        },
                        new
                        {
                            DiscountAmountTypeId = 2,
                            DiscountAmontTypeName = "Percentage"
                        });
                });

            modelBuilder.Entity("DiscountMS.Host.Domain.Model.DiscountTerminationType", b =>
                {
                    b.Property<int>("DiscountTerminationTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DiscountTerminationTypeId"));

                    b.Property<string>("DiscountTerminationTypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DiscountTerminationTypeId");

                    b.ToTable("DiscountTerminationTypes");

                    b.HasData(
                        new
                        {
                            DiscountTerminationTypeId = 1,
                            DiscountTerminationTypeName = "SpecificDate"
                        },
                        new
                        {
                            DiscountTerminationTypeId = 2,
                            DiscountTerminationTypeName = "OutOfStock"
                        },
                        new
                        {
                            DiscountTerminationTypeId = 3,
                            DiscountTerminationTypeName = "Never"
                        });
                });

            modelBuilder.Entity("DiscountMS.Host.Domain.Model.DiscountType", b =>
                {
                    b.Property<int>("DiscountTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DiscountTypeId"));

                    b.Property<string>("DiscountTypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DiscountTypeId");

                    b.ToTable("DiscountTypes");

                    b.HasData(
                        new
                        {
                            DiscountTypeId = 1,
                            DiscountTypeName = "Personal"
                        },
                        new
                        {
                            DiscountTypeId = 2,
                            DiscountTypeName = "InventoryItem"
                        },
                        new
                        {
                            DiscountTypeId = 3,
                            DiscountTypeName = "FromInvoiceTotal"
                        },
                        new
                        {
                            DiscountTypeId = 4,
                            DiscountTypeName = "Sale"
                        },
                        new
                        {
                            DiscountTypeId = 5,
                            DiscountTypeName = "InventoryItemBasedOnInvoiceAmount"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
