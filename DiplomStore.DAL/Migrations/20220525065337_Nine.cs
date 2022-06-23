using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomStore.DAL.Migrations
{
    public partial class Nine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imgPath",
                table: "tovars",
                newName: "NameImg");

            migrationBuilder.RenameColumn(
                name: "imgPath",
                table: "titles",
                newName: "NameImg");

            migrationBuilder.RenameColumn(
                name: "imgPath",
                table: "categories",
                newName: "NameImg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameImg",
                table: "tovars",
                newName: "imgPath");

            migrationBuilder.RenameColumn(
                name: "NameImg",
                table: "titles",
                newName: "imgPath");

            migrationBuilder.RenameColumn(
                name: "NameImg",
                table: "categories",
                newName: "imgPath");
        }
    }
}
