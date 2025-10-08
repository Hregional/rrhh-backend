using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rrhh_backend.Migrations
{
    /// <inheritdoc />
    public partial class uuidLicencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Uuid",
                table: "RHLicencias",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RHLicencias_Uuid",
                table: "RHLicencias",
                column: "Uuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RHLicencias_Uuid",
                table: "RHLicencias");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "RHLicencias");
        }
    }
}
