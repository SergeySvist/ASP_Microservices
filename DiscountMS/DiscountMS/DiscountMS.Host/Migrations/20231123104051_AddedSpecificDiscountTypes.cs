using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiscountMS.Host.Migrations
{
    /// <inheritdoc />
    public partial class AddedSpecificDiscountTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountAmountTypeId",
                table: "PersonalDiscounts",
                newName: "PersonalDiscountId");

            migrationBuilder.CreateTable(
                name: "InventoryItemDiscounts",
                columns: table => new
                {
                    InventoryItemDiscountId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InventoryItemId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItemDiscounts", x => x.InventoryItemDiscountId);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItemOnInvoiceTotalDiscounts",
                columns: table => new
                {
                    InventoryItemOnInvoiceTotalDiscountId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InventoryId = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    InventoryItemPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItemOnInvoiceTotalDiscounts", x => x.InventoryItemOnInvoiceTotalDiscountId);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTotalDiscounts",
                columns: table => new
                {
                    InvoiceTotalDiscountId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceTotal = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTotalDiscounts", x => x.InvoiceTotalDiscountId);
                });

            migrationBuilder.CreateTable(
                name: "SaleDiscounts",
                columns: table => new
                {
                    SaleDiscountId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SaleTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDiscounts", x => x.SaleDiscountId);
                    table.ForeignKey(
                        name: "FK_SaleDiscounts_SaleTypes_SaleTypeId",
                        column: x => x.SaleTypeId,
                        principalTable: "SaleTypes",
                        principalColumn: "SaleTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleDiscounts_SaleTypeId",
                table: "SaleDiscounts",
                column: "SaleTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryItemDiscounts");

            migrationBuilder.DropTable(
                name: "InventoryItemOnInvoiceTotalDiscounts");

            migrationBuilder.DropTable(
                name: "InvoiceTotalDiscounts");

            migrationBuilder.DropTable(
                name: "SaleDiscounts");

            migrationBuilder.RenameColumn(
                name: "PersonalDiscountId",
                table: "PersonalDiscounts",
                newName: "DiscountAmountTypeId");
        }
    }
}
