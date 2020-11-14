using Microsoft.EntityFrameworkCore.Migrations;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.Migrations
{
    public partial class adduserIdToProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ProfileImage_ProfileImageId",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "identity",
                table: "ProfileImage",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImage_UserId",
                schema: "identity",
                table: "ProfileImage",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileImage_AspNetUsers_UserId",
                schema: "identity",
                table: "ProfileImage",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileImage_AspNetUsers_UserId",
                schema: "identity",
                table: "ProfileImage");

            migrationBuilder.DropIndex(
                name: "IX_ProfileImage_UserId",
                schema: "identity",
                table: "ProfileImage");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "identity",
                table: "ProfileImage");

            migrationBuilder.AddColumn<int>(
                name: "ProfileImageId",
                schema: "identity",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                schema: "identity",
                table: "AspNetUsers",
                column: "ProfileImageId",
                unique: true,
                filter: "[ProfileImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ProfileImage_ProfileImageId",
                schema: "identity",
                table: "AspNetUsers",
                column: "ProfileImageId",
                principalSchema: "identity",
                principalTable: "ProfileImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
