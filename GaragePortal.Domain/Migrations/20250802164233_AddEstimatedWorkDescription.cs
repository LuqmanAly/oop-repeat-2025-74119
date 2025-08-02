using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GaragePortal.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddEstimatedWorkDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EstimatedWorkDescription",
                table: "Services",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedWorkDescription",
                table: "Services");
        }
    }
}
