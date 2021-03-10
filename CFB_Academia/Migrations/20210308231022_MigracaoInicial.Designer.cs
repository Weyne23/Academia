﻿// <auto-generated />
using CFB_Academia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace CFB_Academia.Migrations
{
    [DbContext(typeof(AcademiaContexto))]
    [Migration("20210308231022_MigracaoInicial")]
    partial class MigracaoInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

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

                    b.Property<string>("DesTruma");

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
