using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubmitYourIdea.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifIdea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Ideas_IdeaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdeaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdeaId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_UserId",
                table: "Ideas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_AspNetUsers_UserId",
                table: "Ideas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_AspNetUsers_UserId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_UserId",
                table: "Ideas");

            migrationBuilder.AddColumn<int>(
                name: "IdeaId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdeaId",
                table: "AspNetUsers",
                column: "IdeaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Ideas_IdeaId",
                table: "AspNetUsers",
                column: "IdeaId",
                principalTable: "Ideas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
