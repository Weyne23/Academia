using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB_Academia.Models
{
    class AcademiaContexto : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=" + Globais.caminhoBanco + Globais.nomeBanco);
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Turma> Turmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Turma>()
                .Property(t => t.MaxAlunos)
                .HasDefaultValue(-1);

            modelBuilder.Entity<Turma>()
                .Property(t => t.Status)
                .HasDefaultValue('A');
        }
    }
}
