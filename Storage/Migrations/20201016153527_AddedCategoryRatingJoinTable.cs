using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class AddedCategoryRatingJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Categories_CategoryId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_CategoryId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Ratings");

            migrationBuilder.AddColumn<int>(
                name: "TotalScore",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryRatings",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    RatingId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRatings", x => new { x.CategoryId, x.RatingId });
                    table.ForeignKey(
                        name: "FK_CategoryRatings_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryRatings_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRatings_RatingId",
                table: "CategoryRatings",
                column: "RatingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryRatings");

            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "Ratings");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CategoryId",
                table: "Ratings",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Categories_CategoryId",
                table: "Ratings",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
