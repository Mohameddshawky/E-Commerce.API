using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnToProductName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Product_Description",
                table: "orderItems",
                newName: "Product_ProductName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Product_ProductName",
                table: "orderItems",
                newName: "Product_Description");
        }
    }
}
