using Microsoft.EntityFrameworkCore.Migrations;

namespace Merchain.Data.Migrations
{
    public partial class CategoryImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BannerImage",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImage",
                table: "Categories");
        }
    }
}
