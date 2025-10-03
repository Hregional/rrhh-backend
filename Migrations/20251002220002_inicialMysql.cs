using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace rrhh_backend.Migrations
{
    /// <inheritdoc />
    public partial class inicialMysql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AdminEstado",
                columns: table => new
                {
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreEstado = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminEstado", x => x.IdEstado);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AdminPermisos",
                columns: table => new
                {
                    IdPermiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombrePermiso = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    DescripcionPermiso = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminPermisos", x => x.IdPermiso);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AdminRoles",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreRol = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    DescripcionRol = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRoles", x => x.IdRole);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RHDepartamento",
                columns: table => new
                {
                    IdDepartamentos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreDepartamento = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    DescripcionDepartamento = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RHDepartamento", x => x.IdDepartamentos);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RHEstadoCivil",
                columns: table => new
                {
                    IdEstadoCivil = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EstadoCivil = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RHEstadoCivil", x => x.IdEstadoCivil);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RHEstadoColaborador",
                columns: table => new
                {
                    IdEstadoColaborador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EstadosColaborador = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RHEstadoColaborador", x => x.IdEstadoColaborador);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RHEstadoLicencias",
                columns: table => new
                {
                    IdEstadoLicencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EstadoLicencia = table.Column<string>(type: "longtext", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RHEstadoLicencias", x => x.IdEstadoLicencia);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RHTipoLicencias",
                columns: table => new
                {
                    IdTipoLicencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TipoLicencia = table.Column<string>(type: "longtext", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RHTipoLicencias", x => x.IdTipoLicencia);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AdminRolesPermisos",
                columns: table => new
                {
                    IdRolePermiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdRole = table.Column<int>(type: "int", nullable: false),
                    IdPermiso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRolesPermisos", x => x.IdRolePermiso);
                    table.ForeignKey(
                        name: "Relationship4",
                        column: x => x.IdRole,
                        principalTable: "AdminRoles",
                        principalColumn: "IdRole");
                    table.ForeignKey(
                        name: "Relationship5",
                        column: x => x.IdPermiso,
                        principalTable: "AdminPermisos",
                        principalColumn: "IdPermiso");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RHColaborador",
                columns: table => new
                {
                    IdColaborador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdDepartamento = table.Column<int>(type: "int", nullable: false),
                    IdEstadoColaborador = table.Column<int>(type: "int", nullable: false),
                    IdEstadoCivil = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "longtext", nullable: true),
                    Dpi = table.Column<string>(type: "longtext", nullable: false),
                    Nombres = table.Column<string>(type: "longtext", nullable: false),
                    PrimerApellido = table.Column<string>(type: "longtext", nullable: false),
                    SegundoApellido = table.Column<string>(type: "longtext", nullable: true),
                    ApellidoCasada = table.Column<string>(type: "longtext", nullable: true),
                    MunicipioExtendido = table.Column<string>(type: "longtext", nullable: true),
                    DepartamentoExtendido = table.Column<string>(type: "longtext", nullable: true),
                    LugarNacimiento = table.Column<string>(type: "longtext", nullable: true),
                    Telefono = table.Column<string>(type: "longtext", nullable: true),
                    NoCuentaBancaria = table.Column<string>(type: "longtext", nullable: true),
                    Nacionalidad = table.Column<string>(type: "longtext", nullable: true),
                    NoIGSS = table.Column<string>(type: "longtext", nullable: true),
                    NoNIT = table.Column<string>(type: "longtext", nullable: true),
                    NombreConyuge = table.Column<string>(type: "longtext", nullable: true),
                    Direccion = table.Column<string>(type: "longtext", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    FechaInicioLabores = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Debaja = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Foto = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RHColaborador", x => x.IdColaborador);
                    table.ForeignKey(
                        name: "FK_RHColaborador_RHDepartamento_IdDepartamento",
                        column: x => x.IdDepartamento,
                        principalTable: "RHDepartamento",
                        principalColumn: "IdDepartamentos",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RHColaborador_RHEstadoCivil",
                        column: x => x.IdEstadoCivil,
                        principalTable: "RHEstadoCivil",
                        principalColumn: "IdEstadoCivil",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RHColaborador_RHEstadoColaborador",
                        column: x => x.IdEstadoColaborador,
                        principalTable: "RHEstadoColaborador",
                        principalColumn: "IdEstadoColaborador",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AdminUser",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreUsuario = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    Password = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    IdColaborador = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUser", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_User_User_Gth_Colaborador",
                        column: x => x.IdColaborador,
                        principalTable: "RHColaborador",
                        principalColumn: "IdColaborador");
                    table.ForeignKey(
                        name: "Relationship1",
                        column: x => x.IdEstado,
                        principalTable: "AdminEstado",
                        principalColumn: "IdEstado");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RHHistorialDepartamento",
                columns: table => new
                {
                    IdHistorialDepartamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdColaborador = table.Column<int>(type: "int", nullable: false),
                    IdDepartamento = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RHHistorialDepartamento", x => x.IdHistorialDepartamento);
                    table.ForeignKey(
                        name: "FK_RHHistorialDepartamento_RHColaborador",
                        column: x => x.IdColaborador,
                        principalTable: "RHColaborador",
                        principalColumn: "IdColaborador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RHHistorialDepartamento_RHDepartamento",
                        column: x => x.IdDepartamento,
                        principalTable: "RHDepartamento",
                        principalColumn: "IdDepartamentos",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RHLicencias",
                columns: table => new
                {
                    IdLicencias = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdColaborador = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Observaciones = table.Column<string>(type: "longtext", nullable: true),
                    IdTipoLicencia = table.Column<int>(type: "int", nullable: false),
                    IdEstadoLicencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RHLicencias", x => x.IdLicencias);
                    table.ForeignKey(
                        name: "FK_RHLicencias_RHColaborador_IdColaborador",
                        column: x => x.IdColaborador,
                        principalTable: "RHColaborador",
                        principalColumn: "IdColaborador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RHLicencias_RHEstadoLicencias_IdEstadoLicencia",
                        column: x => x.IdEstadoLicencia,
                        principalTable: "RHEstadoLicencias",
                        principalColumn: "IdEstadoLicencia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RHLicencias_RHTipoLicencias_IdTipoLicencia",
                        column: x => x.IdTipoLicencia,
                        principalTable: "RHTipoLicencias",
                        principalColumn: "IdTipoLicencia",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AdminBitacoraUsuario",
                columns: table => new
                {
                    IdBitacoraUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AccionBitacora = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    FechaBitacora = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminBitacoraUsuario", x => x.IdBitacoraUser);
                    table.ForeignKey(
                        name: "Relationship3",
                        column: x => x.IdUsuario,
                        principalTable: "AdminUser",
                        principalColumn: "IdUsuario");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AdminResetPassword",
                columns: table => new
                {
                    IdToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreToken = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FechaCreacionToken = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaExpiracionToken = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminResetPassword", x => x.IdToken);
                    table.ForeignKey(
                        name: "Relationship2",
                        column: x => x.IdUsuario,
                        principalTable: "AdminUser",
                        principalColumn: "IdUsuario");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AdminUserRoles",
                columns: table => new
                {
                    IdUserRoles = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdRole = table.Column<int>(type: "int", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUserRoles", x => x.IdUserRoles);
                    table.ForeignKey(
                        name: "Relationship7",
                        column: x => x.IdUsuario,
                        principalTable: "AdminUser",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "Relationship8",
                        column: x => x.IdRole,
                        principalTable: "AdminRoles",
                        principalColumn: "IdRole");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship3",
                table: "AdminBitacoraUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship2",
                table: "AdminResetPassword",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship4",
                table: "AdminRolesPermisos",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship5",
                table: "AdminRolesPermisos",
                column: "IdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUser_IdColaborador",
                table: "AdminUser",
                column: "IdColaborador",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Relationship1",
                table: "AdminUser",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship7",
                table: "AdminUserRoles",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship8",
                table: "AdminUserRoles",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_RHColaborador_Email",
                table: "RHColaborador",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RHColaborador_IdDepartamento",
                table: "RHColaborador",
                column: "IdDepartamento");

            migrationBuilder.CreateIndex(
                name: "IX_RHColaborador_IdEstadoCivil",
                table: "RHColaborador",
                column: "IdEstadoCivil");

            migrationBuilder.CreateIndex(
                name: "IX_RHColaborador_IdEstadoColaborador",
                table: "RHColaborador",
                column: "IdEstadoColaborador");

            migrationBuilder.CreateIndex(
                name: "IX_RHHistorialDepartamento_IdColaborador",
                table: "RHHistorialDepartamento",
                column: "IdColaborador");

            migrationBuilder.CreateIndex(
                name: "IX_RHHistorialDepartamento_IdDepartamento",
                table: "RHHistorialDepartamento",
                column: "IdDepartamento");

            migrationBuilder.CreateIndex(
                name: "IX_RHLicencias_IdColaborador",
                table: "RHLicencias",
                column: "IdColaborador");

            migrationBuilder.CreateIndex(
                name: "IX_RHLicencias_IdEstadoLicencia",
                table: "RHLicencias",
                column: "IdEstadoLicencia");

            migrationBuilder.CreateIndex(
                name: "IX_RHLicencias_IdTipoLicencia",
                table: "RHLicencias",
                column: "IdTipoLicencia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminBitacoraUsuario");

            migrationBuilder.DropTable(
                name: "AdminResetPassword");

            migrationBuilder.DropTable(
                name: "AdminRolesPermisos");

            migrationBuilder.DropTable(
                name: "AdminUserRoles");

            migrationBuilder.DropTable(
                name: "RHHistorialDepartamento");

            migrationBuilder.DropTable(
                name: "RHLicencias");

            migrationBuilder.DropTable(
                name: "AdminPermisos");

            migrationBuilder.DropTable(
                name: "AdminUser");

            migrationBuilder.DropTable(
                name: "AdminRoles");

            migrationBuilder.DropTable(
                name: "RHEstadoLicencias");

            migrationBuilder.DropTable(
                name: "RHTipoLicencias");

            migrationBuilder.DropTable(
                name: "RHColaborador");

            migrationBuilder.DropTable(
                name: "AdminEstado");

            migrationBuilder.DropTable(
                name: "RHDepartamento");

            migrationBuilder.DropTable(
                name: "RHEstadoCivil");

            migrationBuilder.DropTable(
                name: "RHEstadoColaborador");
        }
    }
}
