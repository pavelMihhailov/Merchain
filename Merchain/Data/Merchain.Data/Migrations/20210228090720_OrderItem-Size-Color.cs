using Microsoft.EntityFrameworkCore.Migrations;

namespace Merchain.Data.Migrations
{
    public partial class OrderItemSizeColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "OrderedItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "OrderedItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderedItems");
        }
    }
}
