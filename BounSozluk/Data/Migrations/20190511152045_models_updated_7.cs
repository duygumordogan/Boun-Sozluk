using Microsoft.EntityFrameworkCore.Migrations;

namespace BounSozluk.Data.Migrations
{
    public partial class models_updated_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_BounSozlukUserId1",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_BounSozlukUserId1",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "BounSozlukUserId1",
                table: "Post");

            migrationBuilder.AlterColumn<string>(
                name: "BounSozlukUserId",
                table: "Post",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PostGroupId",
                table: "Post",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BounSozlukUserId",
                table: "Comment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Comment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_BounSozlukUserId",
                table: "Post",
                column: "BounSozlukUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_PostGroupId",
                table: "Post",
                column: "PostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_BounSozlukUserId",
                table: "Comment",
                column: "BounSozlukUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_BounSozlukUserId",
                table: "Comment",
                column: "BounSozlukUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_BounSozlukUserId",
                table: "Post",
                column: "BounSozlukUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_PostGroup_PostGroupId",
                table: "Post",
                column: "PostGroupId",
                principalTable: "PostGroup",
                principalColumn: "PostGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_BounSozlukUserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_BounSozlukUserId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_PostGroup_PostGroupId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_BounSozlukUserId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_PostGroupId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Comment_BounSozlukUserId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_PostId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "PostGroupId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "BounSozlukUserId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "PostId",
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
    }
}
