using Microsoft.EntityFrameworkCore.Migrations;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.Migrations
{
    public partial class addcontenttypetoprofileimageentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                schema: "identity",
                table: "ProfileImage",
                maxLength: 2000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                schema: "identity",
                table: "ProfileImage");
        }
    }
}
