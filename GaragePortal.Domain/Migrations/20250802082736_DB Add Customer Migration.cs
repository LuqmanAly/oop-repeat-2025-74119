using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GaragePortal.Domain.Migrations
{
    /// <inheritdoc />
    public partial class DBAddCustomerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdminName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    ClientEmail = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    ClientPassword = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Mechanics",
                columns: table => new
                {
                    TechnicianId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TechnicianName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    TechnicianPassword = table.Column<string>(type: "text", nullable: false),
                    TechnicianEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanics", x => x.TechnicianId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LicensePlate = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Cars_Customers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Customers",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    RepairId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VehicleId = table.Column<int>(type: "integer", nullable: false),
                    TechnicianId = table.Column<int>(type: "integer", nullable: false),
                    RepairDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RepairDescription = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    Hours = table.Column<decimal>(type: "numeric", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RepairCost = table.Column<decimal>(type: "numeric", nullable: true),
                    RepairStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.RepairId);
                    table.ForeignKey(
                        name: "FK_Services_Cars_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Cars",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_Mechanics_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Mechanics",
                        principalColumn: "TechnicianId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ClientId",
                table: "Cars",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_TechnicianId",
                table: "Services",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_VehicleId",
                table: "Services",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Mechanics");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
