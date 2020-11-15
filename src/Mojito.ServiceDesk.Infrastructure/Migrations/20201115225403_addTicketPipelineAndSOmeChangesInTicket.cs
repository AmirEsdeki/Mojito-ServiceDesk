using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mojito.ServiceDesk.Infrastructure.Migrations
{
    public partial class addTicketPipelineAndSOmeChangesInTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Groups_GroupId",
                schema: "ticketing",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_GroupId",
                schema: "ticketing",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "ticketing",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "CurrentStep",
                schema: "ticketing",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumSteps",
                schema: "ticketing",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NomineeGroupId",
                schema: "ticketing",
                table: "Tickets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketManagingPipelines",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    CustomerOrganizationId = table.Column<int>(nullable: false),
                    TicketIssueId = table.Column<int>(nullable: false),
                    NomineeGroupId = table.Column<int>(nullable: false),
                    SetToNomineeGroupBasedOnIssueUrl = table.Column<bool>(nullable: false),
                    SetToNomineePersonBasedOnIssueUrl = table.Column<bool>(nullable: false),
                    Step = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketManagingPipelines", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_NomineeGroupId",
                schema: "ticketing",
                table: "Tickets",
                column: "NomineeGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Groups_NomineeGroupId",
                schema: "ticketing",
                table: "Tickets",
                column: "NomineeGroupId",
                principalSchema: "identity",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Groups_NomineeGroupId",
                schema: "ticketing",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketManagingPipelines",
                schema: "identity");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_NomineeGroupId",
                schema: "ticketing",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CurrentStep",
                schema: "ticketing",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MaximumSteps",
                schema: "ticketing",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "NomineeGroupId",
                schema: "ticketing",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                schema: "ticketing",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_GroupId",
                schema: "ticketing",
                table: "Tickets",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Groups_GroupId",
                schema: "ticketing",
                table: "Tickets",
                column: "GroupId",
                principalSchema: "identity",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
