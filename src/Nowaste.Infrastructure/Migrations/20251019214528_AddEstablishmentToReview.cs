using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nowaste.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEstablishmentToReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Establishments_EstablishmentEntityId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_EstablishmentEntityId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "EstablishmentEntityId",
                table: "Reviews");

            migrationBuilder.AddColumn<Guid>(
                name: "EstablishmentId",
                table: "Reviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EstablishmentId",
                table: "Reviews",
                column: "EstablishmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Establishments_EstablishmentId",
                table: "Reviews",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Establishments_EstablishmentId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_EstablishmentId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "EstablishmentId",
                table: "Reviews");

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
        }
    }
}
