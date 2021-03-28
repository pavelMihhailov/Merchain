using Microsoft.EntityFrameworkCore.Migrations;

namespace Merchain.Data.Migrations
{
    public partial class ProductPreviewImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviewImage",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewImage",
                table: "Products");
        }
    }
}
