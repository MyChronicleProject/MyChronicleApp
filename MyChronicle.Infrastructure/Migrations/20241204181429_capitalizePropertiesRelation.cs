using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyChronicle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class capitalizePropertiesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "Relations",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "endDate",
                table: "Relations",
                newName: "EndDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Relations",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Relations",
                newName: "endDate");
        }
    }
}
