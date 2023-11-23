using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiscountMS.Host.Migrations
{
    /// <inheritdoc />
    public partial class AddedDiscountsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountTypeId = table.Column<int>(type: "integer", nullable: false),
                    DiscountAmountTypeId = table.Column<int>(type: "integer", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DiscountTerminationTypeId = table.Column<int>(type: "integer", nullable: false),
                    SpecificDiscountTableKey = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_Discounts_DiscountAmountTypes_DiscountAmountTypeId",
                        column: x => x.DiscountAmountTypeId,
                        principalTable: "DiscountAmountTypes",
                        principalColumn: "DiscountAmountTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Discounts_DiscountTerminationTypes_DiscountTerminationTypeId",
                        column: x => x.DiscountTerminationTypeId,
                        principalTable: "DiscountTerminationTypes",
                        principalColumn: "DiscountTerminationTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Discounts_DiscountTypes_DiscountTypeId",
                        column: x => x.DiscountTypeId,
                        principalTable: "DiscountTypes",
                        principalColumn: "DiscountTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DiscountAmountTypeId",
                table: "Discounts",
                column: "DiscountAmountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DiscountTerminationTypeId",
                table: "Discounts",
                column: "DiscountTerminationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DiscountTypeId",
                table: "Discounts",
                column: "DiscountTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");
        }
    }
}
