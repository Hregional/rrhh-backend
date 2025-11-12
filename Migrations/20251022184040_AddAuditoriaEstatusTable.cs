using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace rrhh_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditoriaEstatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditoriaEstatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TrabajadorId = table.Column<int>(type: "int", nullable: false),
                    FechaCambio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EstatusAnterior = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    EstatusNuevo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Motivo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    SistemaEjecutor = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditoriaEstatus", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditoriaEstatus");
        }
    }
}
