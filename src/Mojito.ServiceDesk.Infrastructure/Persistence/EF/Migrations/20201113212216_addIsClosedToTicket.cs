using Microsoft.EntityFrameworkCore.Migrations;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.Migrations
{
    public partial class addIsClosedToTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                schema: "ticketing",
                table: "Tickets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                schema: "ticketing",
                table: "Tickets");
        }
    }
}
