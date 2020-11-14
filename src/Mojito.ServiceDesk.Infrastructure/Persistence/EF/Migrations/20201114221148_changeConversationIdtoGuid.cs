using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.Migrations
{
    public partial class changeConversationIdtoGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerOrganizationId",
                schema: "ticketing",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ConversationId",
                schema: "ticketing",
                table: "TicketAttachments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "ticketing",
                table: "Conversations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerOrganizationId",
                schema: "ticketing",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "ConversationId",
                schema: "ticketing",
                table: "TicketAttachments",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "ticketing",
                table: "Conversations",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
