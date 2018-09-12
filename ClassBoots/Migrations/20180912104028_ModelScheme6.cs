using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassBoots.Migrations
{
    public partial class ModelScheme6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Lecturer",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Lecturer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
