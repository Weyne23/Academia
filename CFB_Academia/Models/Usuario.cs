using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB_Academia.Models
{
    class Usuario
    {
        public int UsuarioID { get; set; }
        public string NomeUsuario { get; set; }
        public string UserName { get; set; }
        public string SenhaUsuario { get; set; }
        public string StatusUsuario { get; set; }
        public int NivelUsuario { get; set; }

    }
}
