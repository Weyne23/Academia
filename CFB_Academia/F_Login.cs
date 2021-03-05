using CFB_Academia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFB_Academia
{
    public partial class F_Login : Form
    {
        Form1 form1;
        public F_Login(Form1 f)
        {
            InitializeComponent();
            form1 = f;
        }

        private void btn_logar_Click(object sender, EventArgs e)
        {
            string username = tb_userName.Text;
            string senha = tb_senha.Text;

            if (username == "" || senha == "")
            {
                MessageBox.Show("Username e/ou Senha Invalido(s)!", "Erro");
                tb_userName.Focus();
                return;
            }

            try
            {
                using (var ctx = new AcademiaContexto())
                {
                    var user = ctx.Usuarios.Single(u => u.UserName == username);
                    MessageBox.Show("Usuario cadastrado.");
                }
            }
            catch
            {
                MessageBox.Show("Usuario ou senha errados!");
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
