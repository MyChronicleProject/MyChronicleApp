using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyChronicle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FamilyTreePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FamilyTreePermisions_FamilyTreePermisionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FamilyTreePermisionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FamilyTreePermisionId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "FamilyTreePermisions",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyTreePermisions_AppUserId",
                table: "FamilyTreePermisions",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyTreePermisions_AspNetUsers_AppUserId",
                table: "FamilyTreePermisions",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyTreePermisions_AspNetUsers_AppUserId",
                table: "FamilyTreePermisions");

            migrationBuilder.DropIndex(
                name: "IX_FamilyTreePermisions_AppUserId",
                table: "FamilyTreePermisions");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "FamilyTreePermisions");

            migrationBuilder.AddColumn<Guid>(
                name: "FamilyTreePermisionId",
                table: "AspNetUsers",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FamilyTreePermisionId",
                table: "AspNetUsers",
                column: "FamilyTreePermisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FamilyTreePermisions_FamilyTreePermisionId",
                table: "AspNetUsers",
                column: "FamilyTreePermisionId",
                principalTable: "FamilyTreePermisions",
                principalColumn: "Id");
        }
    }
}
