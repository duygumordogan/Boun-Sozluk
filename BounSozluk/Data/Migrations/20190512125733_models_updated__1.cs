using Microsoft.EntityFrameworkCore.Migrations;

namespace BounSozluk.Data.Migrations
{
    public partial class models_updated__1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PostGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "PostGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Post",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PostGroup");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "PostGroup");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Post");
        }
    }
}
