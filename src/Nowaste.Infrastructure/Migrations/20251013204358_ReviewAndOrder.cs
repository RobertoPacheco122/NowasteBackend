using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nowaste.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReviewAndOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Establishments_EstablishmentId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "EstablishmentId",
                table: "Reviews",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_EstablishmentId",
                table: "Reviews",
                newName: "IX_Reviews_OrderId");

            migrationBuilder.AddColumn<Guid>(
                name: "EstablishmentEntityId",
                table: "Reviews",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EstablishmentEntityId",
                table: "Reviews",
                column: "EstablishmentEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Establishments_EstablishmentEntityId",
                table: "Reviews",
                column: "EstablishmentEntityId",
                principalTable: "Establishments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Orders_OrderId",
                table: "Reviews",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Establishments_EstablishmentEntityId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Orders_OrderId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_EstablishmentEntityId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "EstablishmentEntityId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Reviews",
                newName: "EstablishmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_OrderId",
                table: "Reviews",
                newName: "IX_Reviews_EstablishmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Establishments_EstablishmentId",
                table: "Reviews",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
