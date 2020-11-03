using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.Migrations
{
    public partial class AddCustomerAndProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "proprietary");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "ticketing",
                table: "UserIssueUrl",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                schema: "ticketing",
                table: "UserIssueUrl",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "ticketing",
                table: "UserIssueUrl",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "ticketing",
                table: "UserIssueUrl",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                schema: "ticketing",
                table: "UserIssueUrl",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                schema: "ticketing",
                table: "UserIssueUrl",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                schema: "ticketing",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                schema: "ticketing",
                table: "IssueUrls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                schema: "ticketing",
                table: "Conversations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CustomerOrganizationId",
                schema: "identity",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployee",
                schema: "identity",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CustomerOrganizations",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrganizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "proprietary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_CustomerOrganizations_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "identity",
                        principalTable: "CustomerOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketLabels",
                schema: "ticketing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    CustomerOrganizationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketLabels_CustomerOrganizations_CustomerOrganizationId",
                        column: x => x.CustomerOrganizationId,
                        principalSchema: "identity",
                        principalTable: "CustomerOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTicketLabel",
                schema: "ticketing",
                columns: table => new
                {
                    TicketLabelId = table.Column<int>(nullable: false),
                    TicketId = table.Column<long>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTicketLabel", x => new { x.TicketId, x.TicketLabelId });
                    table.ForeignKey(
                        name: "FK_TicketTicketLabel_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "ticketing",
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketTicketLabel_TicketLabels_TicketLabelId",
                        column: x => x.TicketLabelId,
                        principalSchema: "ticketing",
                        principalTable: "TicketLabels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueUrls_ProductId",
                schema: "ticketing",
                table: "IssueUrls",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CustomerOrganizationId",
                schema: "identity",
                table: "AspNetUsers",
                column: "CustomerOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId",
                schema: "proprietary",
                table: "Products",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                schema: "proprietary",
                table: "Products",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_TicketLabels_CustomerOrganizationId",
                schema: "ticketing",
                table: "TicketLabels",
                column: "CustomerOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketTicketLabel_TicketLabelId",
                schema: "ticketing",
                table: "TicketTicketLabel",
                column: "TicketLabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CustomerOrganizations_CustomerOrganizationId",
                schema: "identity",
                table: "AspNetUsers",
                column: "CustomerOrganizationId",
                principalSchema: "identity",
                principalTable: "CustomerOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueUrls_Products_ProductId",
                schema: "ticketing",
                table: "IssueUrls",
                column: "ProductId",
                principalSchema: "proprietary",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CustomerOrganizations_CustomerOrganizationId",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueUrls_Products_ProductId",
                schema: "ticketing",
                table: "IssueUrls");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "proprietary");

            migrationBuilder.DropTable(
                name: "TicketTicketLabel",
                schema: "ticketing");

            migrationBuilder.DropTable(
                name: "TicketLabels",
                schema: "ticketing");

            migrationBuilder.DropTable(
                name: "CustomerOrganizations",
                schema: "identity");

            migrationBuilder.DropIndex(
                name: "IX_IssueUrls_ProductId",
                schema: "ticketing",
                table: "IssueUrls");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CustomerOrganizationId",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "ticketing",
                table: "UserIssueUrl");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "ticketing",
                table: "UserIssueUrl");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "ticketing",
                table: "UserIssueUrl");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "ticketing",
                table: "UserIssueUrl");

            migrationBuilder.DropColumn(
                name: "LastModified",
                schema: "ticketing",
                table: "UserIssueUrl");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                schema: "ticketing",
                table: "UserIssueUrl");

            migrationBuilder.DropColumn(
                name: "Identifier",
                schema: "ticketing",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "ticketing",
                table: "IssueUrls");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                schema: "ticketing",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "CustomerOrganizationId",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsEmployee",
                schema: "identity",
                table: "AspNetUsers");
        }
    }
}
