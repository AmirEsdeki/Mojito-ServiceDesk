﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mojito.ServiceDesk.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.EnsureSchema(
                name: "proprietary");

            migrationBuilder.EnsureSchema(
                name: "ticketing");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

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
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrganizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupTypes",
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
                    Title = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
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
                    Title = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Priorities",
                schema: "ticketing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketStatus",
                schema: "ticketing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "identity",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
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
                name: "TicketIssues",
                schema: "ticketing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    IsIntraOrganizational = table.Column<bool>(nullable: false),
                    CustomerOrganizationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketIssues_CustomerOrganizations_CustomerOrganizationId",
                        column: x => x.CustomerOrganizationId,
                        principalSchema: "identity",
                        principalTable: "CustomerOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Color = table.Column<string>(maxLength: 255, nullable: true),
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
                name: "Groups",
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
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    GroupTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_GroupTypes_GroupTypeId",
                        column: x => x.GroupTypeId,
                        principalSchema: "identity",
                        principalTable: "GroupTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    IsCompanyMember = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PostId = table.Column<int>(nullable: true),
                    CustomerOrganizationId = table.Column<int>(nullable: true),
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_CustomerOrganizations_CustomerOrganizationId",
                        column: x => x.CustomerOrganizationId,
                        principalSchema: "identity",
                        principalTable: "CustomerOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "identity",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IssueUrls",
                schema: "ticketing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Url = table.Column<string>(maxLength: 800, nullable: true),
                    GroupId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueUrls_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "identity",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueUrls_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "proprietary",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "identity",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "identity",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "identity",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileImage",
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
                    Image = table.Column<byte[]>(nullable: true),
                    ImageThumbnail = table.Column<byte[]>(nullable: true),
                    ContentType = table.Column<string>(maxLength: 2000, nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileImage_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(nullable: true),
                    Expires = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedByIp = table.Column<string>(nullable: true),
                    Revoked = table.Column<DateTime>(nullable: true),
                    RevokedByIp = table.Column<string>(nullable: true),
                    ReplacedByToken = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                schema: "identity",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserGroup_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "identity",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "ticketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Identifier = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    IsClosed = table.Column<bool>(nullable: false),
                    CustomerOrganizationId = table.Column<int>(nullable: true),
                    CurrentStep = table.Column<int>(nullable: false),
                    MaximumSteps = table.Column<int>(nullable: false),
                    OpenedById = table.Column<string>(nullable: true),
                    AssigneeId = table.Column<string>(nullable: true),
                    NomineeGroupId = table.Column<int>(nullable: true),
                    ClosedById = table.Column<string>(nullable: true),
                    IssueUrlId = table.Column<int>(nullable: true),
                    TicketStatusId = table.Column<int>(nullable: true),
                    TicketIssueId = table.Column<int>(nullable: true),
                    PriorityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_AssigneeId",
                        column: x => x.AssigneeId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_ClosedById",
                        column: x => x.ClosedById,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_IssueUrls_IssueUrlId",
                        column: x => x.IssueUrlId,
                        principalSchema: "ticketing",
                        principalTable: "IssueUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Groups_NomineeGroupId",
                        column: x => x.NomineeGroupId,
                        principalSchema: "identity",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_OpenedById",
                        column: x => x.OpenedById,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalSchema: "ticketing",
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketIssues_TicketIssueId",
                        column: x => x.TicketIssueId,
                        principalSchema: "ticketing",
                        principalTable: "TicketIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketStatus_TicketStatusId",
                        column: x => x.TicketStatusId,
                        principalSchema: "ticketing",
                        principalTable: "TicketStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserIssueUrl",
                schema: "ticketing",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    IssueUrlId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIssueUrl", x => new { x.UserId, x.IssueUrlId });
                    table.ForeignKey(
                        name: "FK_UserIssueUrl_IssueUrls_IssueUrlId",
                        column: x => x.IssueUrlId,
                        principalSchema: "ticketing",
                        principalTable: "IssueUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserIssueUrl_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                schema: "ticketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    IsPublic = table.Column<bool>(nullable: false),
                    TicketId = table.Column<string>(nullable: true),
                    TicketId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_Tickets_TicketId1",
                        column: x => x.TicketId1,
                        principalSchema: "ticketing",
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketTicketLabel",
                schema: "ticketing",
                columns: table => new
                {
                    TicketLabelId = table.Column<int>(nullable: false),
                    TicketId = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "TicketAttachments",
                schema: "ticketing",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedById = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: true),
                    Location = table.Column<string>(maxLength: 1500, nullable: true),
                    ConversationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketAttachments_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalSchema: "ticketing",
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "identity",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "identity",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "identity",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "identity",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "identity",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CustomerOrganizationId",
                schema: "identity",
                table: "AspNetUsers",
                column: "CustomerOrganizationId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "identity",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "identity",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhoneNumber",
                schema: "identity",
                table: "AspNetUsers",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PostId",
                schema: "identity",
                table: "AspNetUsers",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupTypeId",
                schema: "identity",
                table: "Groups",
                column: "GroupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name",
                schema: "identity",
                table: "Groups",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_GroupTypes_Title",
                schema: "identity",
                table: "GroupTypes",
                column: "Title",
                unique: true,
                filter: "[Title] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Title",
                schema: "identity",
                table: "Posts",
                column: "Title",
                unique: true,
                filter: "[Title] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImage_UserId",
                schema: "identity",
                table: "ProfileImage",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                schema: "identity",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_GroupId",
                schema: "identity",
                table: "UserGroup",
                column: "GroupId");

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
                name: "IX_Conversations_TicketId1",
                schema: "ticketing",
                table: "Conversations",
                column: "TicketId1");

            migrationBuilder.CreateIndex(
                name: "IX_IssueUrls_GroupId",
                schema: "ticketing",
                table: "IssueUrls",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueUrls_ProductId",
                schema: "ticketing",
                table: "IssueUrls",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachments_ConversationId",
                schema: "ticketing",
                table: "TicketAttachments",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketIssues_CustomerOrganizationId",
                schema: "ticketing",
                table: "TicketIssues",
                column: "CustomerOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketLabels_CustomerOrganizationId",
                schema: "ticketing",
                table: "TicketLabels",
                column: "CustomerOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssigneeId",
                schema: "ticketing",
                table: "Tickets",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ClosedById",
                schema: "ticketing",
                table: "Tickets",
                column: "ClosedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IssueUrlId",
                schema: "ticketing",
                table: "Tickets",
                column: "IssueUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_NomineeGroupId",
                schema: "ticketing",
                table: "Tickets",
                column: "NomineeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OpenedById",
                schema: "ticketing",
                table: "Tickets",
                column: "OpenedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PriorityId",
                schema: "ticketing",
                table: "Tickets",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketIssueId",
                schema: "ticketing",
                table: "Tickets",
                column: "TicketIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketStatusId",
                schema: "ticketing",
                table: "Tickets",
                column: "TicketStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatus_Title",
                schema: "ticketing",
                table: "TicketStatus",
                column: "Title",
                unique: true,
                filter: "[Title] IS NOT NULL")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_TicketTicketLabel_TicketLabelId",
                schema: "ticketing",
                table: "TicketTicketLabel",
                column: "TicketLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIssueUrl_IssueUrlId",
                schema: "ticketing",
                table: "UserIssueUrl",
                column: "IssueUrlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "ProfileImage",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "TicketManagingPipelines",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "UserGroup",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "TicketAttachments",
                schema: "ticketing");

            migrationBuilder.DropTable(
                name: "TicketTicketLabel",
                schema: "ticketing");

            migrationBuilder.DropTable(
                name: "UserIssueUrl",
                schema: "ticketing");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Conversations",
                schema: "ticketing");

            migrationBuilder.DropTable(
                name: "TicketLabels",
                schema: "ticketing");

            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "ticketing");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "IssueUrls",
                schema: "ticketing");

            migrationBuilder.DropTable(
                name: "Priorities",
                schema: "ticketing");

            migrationBuilder.DropTable(
                name: "TicketIssues",
                schema: "ticketing");

            migrationBuilder.DropTable(
                name: "TicketStatus",
                schema: "ticketing");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "proprietary");

            migrationBuilder.DropTable(
                name: "GroupTypes",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "CustomerOrganizations",
                schema: "identity");
        }
    }
}
