using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB_Academia.Models
{
    class Aluno
    {
        public int AlunoID { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Status { get; set; }
        public ICollection<AlunoTurma> AlunoTurmas { get; set; }
    }
}
