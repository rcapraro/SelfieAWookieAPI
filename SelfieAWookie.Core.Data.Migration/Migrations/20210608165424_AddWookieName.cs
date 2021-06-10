using Microsoft.EntityFrameworkCore.Migrations;

namespace SelfieAWookie.Core.Data.Migration.Migrations
{
    public partial class AddWookieName : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Wookie",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Wookie");
        }
    }
}
