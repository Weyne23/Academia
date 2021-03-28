﻿// <auto-generated />
using CFB_Academia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CFB_Academia.Migrations
{
    [DbContext(typeof(AcademiaContexto))]
    partial class AcademiaContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("CFB_Academia.Models.Aluno", b =>
                {
                    b.Property<int>("AlunoID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataDeEntrada");

                    b.Property<string>("Foto");

                    b.Property<string>("Nome");

                    b.Property<string>("Plano");

                    b.Property<string>("Status");

                    b.Property<string>("Telefone");

                    b.HasKey("AlunoID");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("CFB_Academia.Models.AlunoTurma", b =>
                {
                    b.Property<int>("AlunoID");

                    b.Property<int>("TurmaID");

                    b.HasKey("AlunoID", "TurmaID");

                    b.HasIndex("TurmaID");

                    b.ToTable("AlunoTurmas");
                });

            modelBuilder.Entity("CFB_Academia.Models.Horario", b =>
                {
                    b.Property<int>("HorarioID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DesHorario");

                    b.HasKey("HorarioID");

                    b.ToTable("Horarios");
                });

            modelBuilder.Entity("CFB_Academia.Models.Professor", b =>
                {
                    b.Property<int>("ProfessorID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.Property<string>("Telefone");

                    b.HasKey("ProfessorID");

                    b.ToTable("Professores");
                });

            modelBuilder.Entity("CFB_Academia.Models.Turma", b =>
                {
                    b.Property<int>("TurmaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DesTurma");

                    b.Property<int>("HorarioID");

                    b.Property<int>("MaxAlunos")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(-1);

                    b.Property<int>("ProfessorID");

                    b.Property<string>("Status")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("A");

                    b.HasKey("TurmaID");

                    b.HasIndex("HorarioID");

                    b.HasIndex("ProfessorID");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("CFB_Academia.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NivelUsuario");

                    b.Property<string>("NomeUsuario");

                    b.Property<string>("SenhaUsuario");

                    b.Property<string>("StatusUsuario");

                    b.Property<string>("UserName");

                    b.HasKey("UsuarioID");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("CFB_Academia.Models.AlunoTurma", b =>
                {
                    b.HasOne("CFB_Academia.Models.Aluno", "Aluno")
                        .WithMany("AlunoTurmas")
                        .HasForeignKey("AlunoID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CFB_Academia.Models.Turma", "Turma")
                        .WithMany("AlunoTurmas")
                        .HasForeignKey("TurmaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CFB_Academia.Models.Turma", b =>
                {
                    b.HasOne("CFB_Academia.Models.Horario", "Horario")
                        .WithMany()
                        .HasForeignKey("HorarioID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CFB_Academia.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
