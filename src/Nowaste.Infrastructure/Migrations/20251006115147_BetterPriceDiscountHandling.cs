using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nowaste.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BetterPriceDiscountHandling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatingDayEntity_Establishments_EstablishmentId",
                table: "OperatingDayEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperatingDayEntity",
                table: "OperatingDayEntity");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "ProductPricesHistory");

            migrationBuilder.RenameTable(
                name: "OperatingDayEntity",
                newName: "OperatingDays");

            migrationBuilder.RenameIndex(
                name: "IX_OperatingDayEntity_EstablishmentId",
                table: "OperatingDays",
                newName: "IX_OperatingDays_EstablishmentId");

            migrationBuilder.AddColumn<bool>(
                name: "ShowDiscountAsPercentage",
                table: "ProductPricesHistory",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperatingDays",
                table: "OperatingDays",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingDays_Establishments_EstablishmentId",
                table: "OperatingDays",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperatingDays_Establishments_EstablishmentId",
                table: "OperatingDays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperatingDays",
                table: "OperatingDays");

            migrationBuilder.DropColumn(
                name: "ShowDiscountAsPercentage",
                table: "ProductPricesHistory");

            migrationBuilder.RenameTable(
                name: "OperatingDays",
                newName: "OperatingDayEntity");

            migrationBuilder.RenameIndex(
                name: "IX_OperatingDays_EstablishmentId",
                table: "OperatingDayEntity",
                newName: "IX_OperatingDayEntity_EstablishmentId");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "ProductPricesHistory",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperatingDayEntity",
                table: "OperatingDayEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OperatingDayEntity_Establishments_EstablishmentId",
                table: "OperatingDayEntity",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
