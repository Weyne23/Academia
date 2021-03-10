using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CFB_Academia.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    HorarioID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DesHorario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.HorarioID);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    ProfessorID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.ProfessorID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NivelUsuario = table.Column<int>(nullable: false),
                    NomeUsuario = table.Column<string>(nullable: true),
                    SenhaUsuario = table.Column<string>(nullable: true),
                    StatusUsuario = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    TurmaID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DesTruma = table.Column<string>(nullable: true),
                    HorarioID = table.Column<int>(nullable: false),
                    MaxAlunos = table.Column<int>(nullable: false, defaultValue: -1)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProfessorID = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true, defaultValue: "A")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.TurmaID);
                    table.ForeignKey(
                        name: "FK_Turmas_Horarios_HorarioID",
                        column: x => x.HorarioID,
                        principalTable: "Horarios",
                        principalColumn: "HorarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turmas_Professores_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professores",
                        principalColumn: "ProfessorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_HorarioID",
                table: "Turmas",
                column: "HorarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ProfessorID",
                table: "Turmas",
                column: "ProfessorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
