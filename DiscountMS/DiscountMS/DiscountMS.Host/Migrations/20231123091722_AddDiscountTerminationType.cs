using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiscountMS.Host.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountTerminationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscountTerminationTypes",
                columns: table => new
                {
                    DiscountTerminationTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountTerminationTypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTerminationTypes", x => x.DiscountTerminationTypeId);
                });

            migrationBuilder.InsertData(
                table: "DiscountTerminationTypes",
                columns: new[] { "DiscountTerminationTypeId", "DiscountTerminationTypeName" },
                values: new object[,]
                {
                    { 1, "SpecificDate" },
                    { 2, "OutOfStock" },
                    { 3, "Never" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountTerminationTypes");
        }
    }
}
