using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nowaste.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddressInfos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "EstablishmentType",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "ResponsibleCpf",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "ResponsibleName",
                table: "Establishments");

            migrationBuilder.AlterColumn<string>(
                name: "TradeName",
                table: "Establishments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LegalName",
                table: "Establishments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Establishments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressType",
                table: "Addresses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressType",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "TradeName",
                table: "Establishments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LegalName",
                table: "Establishments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Establishments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Establishments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "EstablishmentType",
                table: "Establishments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleCpf",
                table: "Establishments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleName",
                table: "Establishments",
                type: "text",
                nullable: true);
        }
    }
}
