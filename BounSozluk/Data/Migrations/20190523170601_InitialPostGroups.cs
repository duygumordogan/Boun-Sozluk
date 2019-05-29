using Microsoft.EntityFrameworkCore.Migrations;

namespace BounSozluk.Data.Migrations
{
    public partial class InitialPostGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PostGroup",
                columns: new[] { "Name", "Description", "PhotoUrl" },
                values: new object[] { "Events", "Events around the school.", "events.jpg" });

            migrationBuilder.InsertData(
                table: "PostGroup",
                columns: new[] { "Name", "Description", "PhotoUrl" },
                values: new object[] { "Career", "Career opportunities for students.", "career.jpg" });

            migrationBuilder.InsertData(
                table: "PostGroup",
                columns: new[] { "Name", "Description", "PhotoUrl" },
                values: new object[] { "Housing", "Housing posts around the school.", "housing.jpg" });

            migrationBuilder.InsertData(
                table: "PostGroup",
                columns: new[] { "Name", "Description", "PhotoUrl" },
                values: new object[] { "Lost & Found", "Lost and found items are here.", "lostandfound.jpg" });

            migrationBuilder.InsertData(
                table: "PostGroup",
                columns: new[] { "Name", "Description", "PhotoUrl" },
                values: new object[] { "Academic", "Share everything about the lessons and teachers.", "academic.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
