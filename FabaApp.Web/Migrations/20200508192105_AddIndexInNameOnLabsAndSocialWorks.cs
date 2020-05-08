using Microsoft.EntityFrameworkCore.Migrations;

namespace FabaApp.Web.Migrations
{
    public partial class AddIndexInNameOnLabsAndSocialWorks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SocialWorks_Name",
                table: "SocialWorks",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Labs_Name",
                table: "Labs",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SocialWorks_Name",
                table: "SocialWorks");

            migrationBuilder.DropIndex(
                name: "IX_Labs_Name",
                table: "Labs");
        }
    }
}
