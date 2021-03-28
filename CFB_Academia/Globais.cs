using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB_Academia
{
    class Globais
    {
        public static string versao = "1.0";
        public static bool logado = false;
        public static int IdUsuarioLogado = 0;
        public static int nivel = 0; //1 = basico - 2 = gerente - 3 = master
        public static string caminho = System.Environment.CurrentDirectory;
        public static string nomeBanco = "db_academia.db";
        public static string caminhoBanco = caminho + @"\banco\";
        public static string caminhoFoto = caminho + @"\foto\";
    }
}
