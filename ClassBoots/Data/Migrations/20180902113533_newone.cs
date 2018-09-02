using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassBoots.Data.Migrations
{
    public partial class newone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Subject",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Description",
                table: "Subject",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
