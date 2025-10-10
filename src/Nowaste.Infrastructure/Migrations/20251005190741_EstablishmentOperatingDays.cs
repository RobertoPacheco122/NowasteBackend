using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nowaste.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EstablishmentOperatingDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SalePrice",
                table: "ProductPricesHistory",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "ProductPricesHistory",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.CreateTable(
                name: "OperatingDayEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    OpeningTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    ClosingTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EstablishmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingDayEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperatingDayEntity_Establishments_EstablishmentId",
                        column: x => x.EstablishmentId,
                        principalTable: "Establishments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperatingDayEntity_EstablishmentId",
                table: "OperatingDayEntity",
                column: "EstablishmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperatingDayEntity");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "ProductPricesHistory",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductPricesHistory",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
