using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryMS.Host.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInventoryItemDbSetAddStockField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "InventoryItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "InventoryItems");
        }
    }
}
