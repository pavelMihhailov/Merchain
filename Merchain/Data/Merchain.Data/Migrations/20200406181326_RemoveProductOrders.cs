using Microsoft.EntityFrameworkCore.Migrations;

namespace Merchain.Data.Migrations
{
    public partial class RemoveProductOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orders",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Orders",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
