using Microsoft.EntityFrameworkCore.Migrations;

namespace Fieldr.Infrastructure.Persistence.Migrations
{
    public partial class PhotoForStringBase64IsNowPhotoUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "FieldRecords");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "FieldRecords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "FieldRecords");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "FieldRecords",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
