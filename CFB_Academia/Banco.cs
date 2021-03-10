using CFB_Academia.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB_Academia
{
    class Banco
    {
        public static DataTable obterUsuarios()
        {
            DataTable dt = new DataTable();
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                using (var ctx = new AcademiaContexto()) {
                    usuarios = ctx.Usuarios.ToList();
                }

                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Nome Usuario", typeof(string));
                dt.Columns.Add("User Name", typeof(string));
                dt.Columns.Add("Senha", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Nivel", typeof(string));

                foreach (var u in usuarios)
                {
                    dt.Rows.Add(u.UsuarioID, u.NomeUsuario, u.UserName, u.SenhaUsuario, u.StatusUsuario, u.NivelUsuario);
                }

                return dt;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
