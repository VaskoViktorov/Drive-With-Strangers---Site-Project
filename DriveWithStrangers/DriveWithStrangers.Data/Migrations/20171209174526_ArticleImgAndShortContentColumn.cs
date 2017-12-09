using Microsoft.EntityFrameworkCore.Migrations;

namespace DriveWithStrangers.Data.Migrations
{
    public partial class ArticleImgAndShortContentColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortContent",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ShortContent",
                table: "Articles");
        }
    }
}
