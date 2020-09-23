using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayLibrary.DAL.Migrations
{
    public partial class birthday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BookAuthors",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookAuthors");
        }
    }
}
