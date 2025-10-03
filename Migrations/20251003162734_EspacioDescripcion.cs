using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rrhh_backend.Migrations
{
    /// <inheritdoc />
    public partial class EspacioDescripcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NombreDepartamento",
                table: "RHDepartamento",
                type: "longtext",
                unicode: false,
                maxLength: 55,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(55)",
                oldUnicode: false,
                oldMaxLength: 55);

            migrationBuilder.AlterColumn<string>(
                name: "DescripcionDepartamento",
                table: "RHDepartamento",
                type: "longtext",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NombreDepartamento",
                table: "RHDepartamento",
                type: "varchar(55)",
                unicode: false,
                maxLength: 55,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldUnicode: false,
                oldMaxLength: 55);

            migrationBuilder.AlterColumn<string>(
                name: "DescripcionDepartamento",
                table: "RHDepartamento",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
