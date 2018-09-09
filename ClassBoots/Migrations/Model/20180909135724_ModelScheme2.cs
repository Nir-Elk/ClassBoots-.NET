using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassBoots.Migrations.Model
{
    public partial class ModelScheme2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Refference",
                table: "Video",
                newName: "URL");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Lecture",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Institution",
                newName: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Video",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Video");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Video",
                newName: "Refference");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Lecture",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Institution",
                newName: "Adress");
        }
    }
}
