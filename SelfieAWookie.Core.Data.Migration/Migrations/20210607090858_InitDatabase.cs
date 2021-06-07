using Microsoft.EntityFrameworkCore.Migrations;

namespace SelfieAWookie.Core.Data.Migration.Migrations
{
    public partial class InitDatabase : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wookie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wookie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Selfie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WookieId = table.Column<int>(type: "int", nullable: true),
                    PhotoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Selfie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Selfie_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Selfie_Wookie_WookieId",
                        column: x => x.WookieId,
                        principalTable: "Wookie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Selfie_PhotoId",
                table: "Selfie",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Selfie_WookieId",
                table: "Selfie",
                column: "WookieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Selfie");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Wookie");
        }
    }
}
