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
    public partial class F_NovoUsuario : Form
    {
        public F_NovoUsuario()
        {
            InitializeComponent();
        }

        private void limparCampos()
        {
            tb_nome.Clear();
            tb_senha.Clear();
            tb_userName.Clear();
            n_nivel.Value = 1;
            cb_status.Text = "";
            tb_nome.Focus();
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            using (var ctx = new AcademiaContexto())
            {
                Usuario usuario = new Usuario();
                usuario.NomeUsuario = tb_nome.Text;
                usuario.NivelUsuario = Convert.ToInt32(Math.Round(n_nivel.Value, 0));//Tirando as casas decimais
                usuario.StatusUsuario = cb_status.Text;
                usuario.UserName = tb_userName.Text;
                usuario.SenhaUsuario = tb_senha.Text;

                var user = ctx.Usuarios.SingleOrDefault(u => u.UserName == tb_userName.Text);

                if(user == null)
                {
                    ctx.Add(usuario);
                    ctx.SaveChanges();
                    MessageBox.Show("Usuario Cadastrado com sucesso.", "Mensagem");
                    limparCampos();
                }
                else
                {
                    MessageBox.Show("Usuário já Cadastrado!", "Erro");
                }
            }
        }
    }
}
