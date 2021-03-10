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
    public partial class F_GestaoUsuarios : Form
    {
        Form1 form;
        public F_GestaoUsuarios(Form1 f)
        {
            InitializeComponent();
            form = f;
        }

        private void carregarUsuarios(int tamanhoId, int tamanhoNomeUser)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var ctx = new AcademiaContexto())
                {
                    var user = (from u in ctx.Usuarios
                                select new
                                {
                                    idUser = u.UsuarioID,
                                    nomeUser = u.NomeUsuario
                                });

                    dt.Columns.Add("Id", typeof(int));
                    dt.Columns.Add("Nome Usuario", typeof(string));

                    foreach (var u in user)
                    {
                        dt.Rows.Add(u.idUser, u.nomeUser);
                    }
                    dgv_usuarios.DataSource = dt;
                    dgv_usuarios.Columns[0].Width = tamanhoId;
                    dgv_usuarios.Columns[1].Width = tamanhoNomeUser;
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Erro" + ex.Message, "Erro");
            }
            
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void F_GestaoUsuarios_Load(object sender, EventArgs e)
        {
            carregarUsuarios(75, 170);
        }

        private void dgv_usuarios_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int numLinhas = dgv.SelectedRows.Count;
            if (numLinhas > 0)
            {
                Usuario user;
                int linhaCel = Convert.ToInt32(dgv.SelectedRows[0].Cells[0].Value);
                using (var ctx = new AcademiaContexto())
                {
                    user = ctx.Usuarios.Find(linhaCel);
                }

                tb_ID.Text = user.UsuarioID.ToString();
                tb_nome.Text = user.NomeUsuario;
                tb_userName.Text = user.UserName;
                tb_senha.Text = user.SenhaUsuario;
                cb_status.Text = user.StatusUsuario;
                n_nivel.Value = user.NivelUsuario;
            }
        }

        private void btn_novoUsuario_Click(object sender, EventArgs e)
        {
            F_NovoUsuario f_NovoUsuario = new F_NovoUsuario();
            f_NovoUsuario.ShowDialog();
            carregarUsuarios(75, 170);
        }

        private void btrn_salvarAlteracoes_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Salvar Alterações?", "Mensagem", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int linhaSelecionada = dgv_usuarios.SelectedRows[0].Index;
                using (var ctx = new AcademiaContexto())
                {
                    var user = ctx.Usuarios.Find(Convert.ToInt32(tb_ID.Text));
                    user.NomeUsuario = tb_nome.Text;
                    user.NivelUsuario = Convert.ToInt32(Math.Round(n_nivel.Value, 0));//Tirando as casas decimais
                    user.StatusUsuario = cb_status.Text;
                    user.UserName = tb_userName.Text;
                    user.SenhaUsuario = tb_senha.Text;
                    ctx.SaveChanges();
                }

                if (Convert.ToInt32(tb_ID.Text) == Globais.IdUsuarioLogado)
                {
                    form.lb_acesso.Text = n_nivel.Value.ToString();
                    form.lb_nomeUsuario.Text = tb_nome.Text;
                    Globais.nivel = Convert.ToInt32(n_nivel.Value);
                }

                MessageBox.Show("Usuario atualizado.", "Mensagem");
                dgv_usuarios[1, linhaSelecionada].Value = tb_nome.Text;
                dgv_usuarios.CurrentCell = dgv_usuarios[0, linhaSelecionada];
            }
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza quer excluir esse usuário?", "Mensagem", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var ctx = new AcademiaContexto())
                {
                    var user = ctx.Usuarios.Find(Convert.ToInt32(tb_ID.Text));
                    ctx.Remove(user);
                    ctx.SaveChanges();
                }

                if (Convert.ToInt32(tb_ID.Text) == Globais.IdUsuarioLogado)
                {
                    form.lb_acesso.Text = "0";
                    form.lb_nomeUsuario.Text = "...";
                    form.pb_ledLogado.Image = Properties.Resources.led_circle_red;
                    Globais.nivel = 0;
                    Globais.logado = false;
                    this.Close();
                }

                MessageBox.Show("Usuario Excluido.", "Mensagem");
                dgv_usuarios.Rows.Remove(dgv_usuarios.SelectedRows[0]);
            }
        }
    }
}
