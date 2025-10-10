using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rrhh_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUrlConstanciaToRHLicencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlConstancia",
                table: "RHLicencias",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlConstancia",
                table: "RHLicencias");
        }
    }
}