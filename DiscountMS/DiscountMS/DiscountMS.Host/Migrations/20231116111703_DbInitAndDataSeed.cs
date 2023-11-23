using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiscountMS.Host.Migrations
{
    /// <inheritdoc />
    public partial class DbInitAndDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscountAmountTypes",
                columns: table => new
                {
                    DiscountAmountTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountAmontTypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountAmountTypes", x => x.DiscountAmountTypeId);
                });

            migrationBuilder.CreateTable(
                name: "DiscountTypes",
                columns: table => new
                {
                    DiscountTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountTypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTypes", x => x.DiscountTypeId);
                });

            migrationBuilder.InsertData(
                table: "DiscountAmountTypes",
                columns: new[] { "DiscountAmountTypeId", "DiscountAmontTypeName" },
                values: new object[,]
                {
                    { 1, "FixedAmount" },
                    { 2, "Percentage" }
                });

            migrationBuilder.InsertData(
                table: "DiscountTypes",
                columns: new[] { "DiscountTypeId", "DiscountTypeName" },
                values: new object[,]
                {
                    { 1, "Personal" },
                    { 2, "InventoryItem" },
                    { 3, "FromInvoiceTotal" },
                    { 4, "Sale" },
                    { 5, "InventoryItemBasedOnInvoiceAmount" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountAmountTypes");

            migrationBuilder.DropTable(
                name: "DiscountTypes");
        }
    }
}
