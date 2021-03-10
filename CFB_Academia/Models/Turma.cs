using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB_Academia.Models
{
    class Turma
    {
        public int TurmaID { get; set; }
        public string DesTruma { get; set; }
        public int ProfessorID { get; set; }
        public Professor Professor { get; set; }
        public int HorarioID { get; set; }
        public Horario Horario { get; set; }
        public int MaxAlunos { get; set; }
        public string Status { get; set; }
    }
}
