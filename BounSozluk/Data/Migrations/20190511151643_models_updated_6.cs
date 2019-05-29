using Microsoft.EntityFrameworkCore.Migrations;

namespace BounSozluk.Data.Migrations
{
    public partial class models_updated_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BounSozlukUserId",
                table: "Post",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BounSozlukUserId1",
                table: "Post",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_BounSozlukUserId1",
                table: "Post",
                column: "BounSozlukUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_BounSozlukUserId1",
                table: "Post",
                column: "BounSozlukUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_BounSozlukUserId1",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_BounSozlukUserId1",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "BounSozlukUserId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "BounSozlukUserId1",
                table: "Post");
        }
    }
}
