using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academico.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_Curso",
                columns: table => new
                {
                    IdCurso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModalidadCurso = table.Column<byte>(type: "tinyint", nullable: false),
                    NombreCurso = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CupoMaximo = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Curso", x => x.IdCurso);
                });

            migrationBuilder.CreateTable(
                name: "tb_Estudiante",
                columns: table => new
                {
                    IdEstudiante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoIdentificacion = table.Column<byte>(type: "tinyint", nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Estudiante", x => x.IdEstudiante);
                });

            migrationBuilder.CreateTable(
                name: "tb_Matricula",
                columns: table => new
                {
                    IdMatricula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Matricula", x => x.IdMatricula);
                    table.ForeignKey(
                        name: "FK_tb_Matricula_tb_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "tb_Curso",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_Matricula_tb_Estudiante_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "tb_Estudiante",
                        principalColumn: "IdEstudiante",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_Curso_NombreCurso",
                table: "tb_Curso",
                column: "NombreCurso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Estudiante_Cedula",
                table: "tb_Estudiante",
                column: "Cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Estudiante_CorreoElectronico",
                table: "tb_Estudiante",
                column: "CorreoElectronico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Matricula_CursoId",
                table: "tb_Matricula",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Matricula_EstudianteId_CursoId",
                table: "tb_Matricula",
                columns: new[] { "EstudianteId", "CursoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Matricula");

            migrationBuilder.DropTable(
                name: "tb_Curso");

            migrationBuilder.DropTable(
                name: "tb_Estudiante");
        }
    }
}
