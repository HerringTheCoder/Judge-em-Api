using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class UpdateSummaryRelationToOneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Summaries_GameId",
                table: "Summaries");

            migrationBuilder.CreateIndex(
                name: "IX_Summaries_GameId",
                table: "Summaries",
                column: "GameId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Summaries_GameId",
                table: "Summaries");

            migrationBuilder.CreateIndex(
                name: "IX_Summaries_GameId",
                table: "Summaries",
                column: "GameId");
        }
    }
}
