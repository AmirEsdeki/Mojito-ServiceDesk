using Microsoft.EntityFrameworkCore.Migrations;

namespace Mojito.ServiceDesk.Infrastructure.Persistence.EF.Migrations
{
    public partial class makeProfileImageOptional : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "ProfileImageId",
                schema: "identity",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ProfileImage_ProfileImageId",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileImageId",
                schema: "identity",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                schema: "identity",
                table: "AspNetUsers",
                column: "ProfileImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ProfileImage_ProfileImageId",
                schema: "identity",
                table: "AspNetUsers",
                column: "ProfileImageId",
                principalSchema: "identity",
                principalTable: "ProfileImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
