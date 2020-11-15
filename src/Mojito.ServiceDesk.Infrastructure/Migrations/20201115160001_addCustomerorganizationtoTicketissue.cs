using Microsoft.EntityFrameworkCore.Migrations;

namespace Mojito.ServiceDesk.Infrastructure.Migrations
{
    public partial class addCustomerorganizationtoTicketissue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmployee",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CustomerOrganizationId",
                schema: "ticketing",
                table: "TicketIssues",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIntraOrganizational",
                schema: "ticketing",
                table: "TicketIssues",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompanyMember",
                schema: "identity",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TicketIssues_CustomerOrganizationId",
                schema: "ticketing",
                table: "TicketIssues",
                column: "CustomerOrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketIssues_CustomerOrganizations_CustomerOrganizationId",
                schema: "ticketing",
                table: "TicketIssues",
                column: "CustomerOrganizationId",
                principalSchema: "identity",
                principalTable: "CustomerOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketIssues_CustomerOrganizations_CustomerOrganizationId",
                schema: "ticketing",
                table: "TicketIssues");

            migrationBuilder.DropIndex(
                name: "IX_TicketIssues_CustomerOrganizationId",
                schema: "ticketing",
                table: "TicketIssues");

            migrationBuilder.DropColumn(
                name: "CustomerOrganizationId",
                schema: "ticketing",
                table: "TicketIssues");

            migrationBuilder.DropColumn(
                name: "IsIntraOrganizational",
                schema: "ticketing",
                table: "TicketIssues");

            migrationBuilder.DropColumn(
                name: "IsCompanyMember",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployee",
                schema: "identity",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
