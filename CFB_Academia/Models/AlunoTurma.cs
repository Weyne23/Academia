using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB_Academia.Models
{
    class AlunoTurma
    {
        public int AlunoID { get; set; }
        public Aluno Aluno { get; set; }
        public int TurmaID { get; set; }
        public Turma Turma { get; set; }
    }
}
