using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class AddGameSummariesTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Summaries_GameId",
                table: "Summaries",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Summaries_Games_GameId",
                table: "Summaries",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Summaries_Games_GameId",
                table: "Summaries");

            migrationBuilder.DropIndex(
                name: "IX_Summaries_GameId",
                table: "Summaries");
        }
    }
}
