using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rrhh_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuditoriaEstatusToUseForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstatusAnterior",
                table: "AuditoriaEstatus");

            migrationBuilder.DropColumn(
                name: "EstatusNuevo",
                table: "AuditoriaEstatus");

            migrationBuilder.AddColumn<int>(
                name: "IdEstatusAnterior",
                table: "AuditoriaEstatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdEstatusNuevo",
                table: "AuditoriaEstatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaEstatus_IdEstatusAnterior",
                table: "AuditoriaEstatus",
                column: "IdEstatusAnterior");

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaEstatus_IdEstatusNuevo",
                table: "AuditoriaEstatus",
                column: "IdEstatusNuevo");

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaEstatus_TrabajadorId",
                table: "AuditoriaEstatus",
                column: "TrabajadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditoriaEstatus_EstatusAnterior",
                table: "AuditoriaEstatus",
                column: "IdEstatusAnterior",
                principalTable: "RHEstadoColaborador",
                principalColumn: "IdEstadoColaborador",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditoriaEstatus_EstatusNuevo",
                table: "AuditoriaEstatus",
                column: "IdEstatusNuevo",
                principalTable: "RHEstadoColaborador",
                principalColumn: "IdEstadoColaborador",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditoriaEstatus_RHColaborador",
                table: "AuditoriaEstatus",
                column: "TrabajadorId",
                principalTable: "RHColaborador",
                principalColumn: "IdColaborador",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditoriaEstatus_EstatusAnterior",
                table: "AuditoriaEstatus");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditoriaEstatus_EstatusNuevo",
                table: "AuditoriaEstatus");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditoriaEstatus_RHColaborador",
                table: "AuditoriaEstatus");

            migrationBuilder.DropIndex(
                name: "IX_AuditoriaEstatus_IdEstatusAnterior",
                table: "AuditoriaEstatus");

            migrationBuilder.DropIndex(
                name: "IX_AuditoriaEstatus_IdEstatusNuevo",
                table: "AuditoriaEstatus");

            migrationBuilder.DropIndex(
                name: "IX_AuditoriaEstatus_TrabajadorId",
                table: "AuditoriaEstatus");

            migrationBuilder.DropColumn(
                name: "IdEstatusAnterior",
                table: "AuditoriaEstatus");

            migrationBuilder.DropColumn(
                name: "IdEstatusNuevo",
                table: "AuditoriaEstatus");

            migrationBuilder.AddColumn<string>(
                name: "EstatusAnterior",
                table: "AuditoriaEstatus",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstatusNuevo",
                table: "AuditoriaEstatus",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
