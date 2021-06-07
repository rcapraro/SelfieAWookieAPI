using Microsoft.EntityFrameworkCore.Migrations;

namespace SelfieAWookie.Core.Data.Migration.Migrations
{
    public partial class AddDescription : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Selfie",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Selfie");
        }
    }
}
