using Microsoft.EntityFrameworkCore.Migrations;

namespace BounSozluk.Data.Migrations
{
    public partial class models_updated_9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_BounSozlukUserId1",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_BounSozlukUserId1",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_BounSozlukUserId1",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Comment_BounSozlukUserId1",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "BounSozlukUserId1",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "BounSozlukUserId1",
                table: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "BounSozlukUserId",
                table: "Post",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "BounSozlukUserId",
                table: "Comment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Post_BounSozlukUserId",
                table: "Post",
                column: "BounSozlukUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_BounSozlukUserId",
                table: "Comment",
                column: "BounSozlukUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_BounSozlukUserId",
                table: "Comment",
                column: "BounSozlukUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_BounSozlukUserId",
                table: "Post",
                column: "BounSozlukUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_BounSozlukUserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_BounSozlukUserId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_BounSozlukUserId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Comment_BounSozlukUserId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "BounSozlukUserId",
                table: "Post",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BounSozlukUserId1",
                table: "Post",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BounSozlukUserId",
                table: "Comment",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BounSozlukUserId1",
                table: "Comment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_BounSozlukUserId1",
                table: "Post",
                column: "BounSozlukUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_BounSozlukUserId1",
                table: "Comment",
                column: "BounSozlukUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_BounSozlukUserId1",
                table: "Comment",
                column: "BounSozlukUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_BounSozlukUserId1",
                table: "Post",
                column: "BounSozlukUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
