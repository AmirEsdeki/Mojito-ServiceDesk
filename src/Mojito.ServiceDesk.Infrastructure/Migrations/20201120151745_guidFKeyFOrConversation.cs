using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mojito.ServiceDesk.Infrastructure.Migrations
{
    public partial class guidFKeyFOrConversation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Tickets_TicketId1",
                schema: "ticketing",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_TicketId1",
                schema: "ticketing",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "TicketId1",
                schema: "ticketing",
                table: "Conversations");

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketId",
                schema: "ticketing",
                table: "Conversations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_TicketId",
                schema: "ticketing",
                table: "Conversations",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Tickets_TicketId",
                schema: "ticketing",
                table: "Conversations",
                column: "TicketId",
                principalSchema: "ticketing",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Tickets_TicketId",
                schema: "ticketing",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_TicketId",
                schema: "ticketing",
                table: "Conversations");

            migrationBuilder.AlterColumn<string>(
                name: "TicketId",
                schema: "ticketing",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "TicketId1",
                schema: "ticketing",
                table: "Conversations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_TicketId1",
                schema: "ticketing",
                table: "Conversations",
                column: "TicketId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Tickets_TicketId1",
                schema: "ticketing",
                table: "Conversations",
                column: "TicketId1",
                principalSchema: "ticketing",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
