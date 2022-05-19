using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomStore.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.CategoriesId);
                });

            migrationBuilder.CreateTable(
                name: "titles",
                columns: table => new
                {
                    TitlesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_titles", x => x.TitlesId);
                });

            migrationBuilder.CreateTable(
                name: "tovars",
                columns: table => new
                {
                    TovarsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isValids = table.Column<bool>(type: "bit", nullable: false),
                    hight = table.Column<int>(type: "int", nullable: true),
                    width = table.Column<int>(type: "int", nullable: true),
                    count = table.Column<int>(type: "int", nullable: false),
                    materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    filler = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    TitleId = table.Column<int>(type: "int", nullable: false),
                    TitlesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tovars", x => x.TovarsId);
                    table.ForeignKey(
                        name: "FK_tovars_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "CategoriesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tovars_titles_TitlesId",
                        column: x => x.TitlesId,
                        principalTable: "titles",
                        principalColumn: "TitlesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tovars_CategoryId",
                table: "tovars",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tovars_TitlesId",
                table: "tovars",
                column: "TitlesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tovars");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "titles");
        }
    }
}
