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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            F_Login f_Login = new F_Login(this);
            f_Login.ShowDialog();
        }

        private void abreForm(int nivel, Form f)
        {
            if (Globais.logado)
            {
                if (Globais.nivel >= nivel)
                {
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Acesso negado!");
                }
            }
            else
            {
                MessageBox.Show("É necessario ter um usuario logado.");
            }
        }

        private void logarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_Login f_Login = new F_Login(this);
            f_Login.ShowDialog();
        }

        private void deslogarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lb_acesso.Text = "0";
            lb_nomeUsuario.Text = "...";
            pb_ledLogado.Image = Properties.Resources.led_circle_red;
            Globais.nivel = 0;
            Globais.logado = false;
        }

        private void bancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globais.logado)
            {
                if(Globais.nivel >= 3)
                {
                    //ToDo
                }
                else
                {
                    MessageBox.Show("Acesso negado!");
                }
            }
            else
            {
                MessageBox.Show("É necessario ter um usuario logado.");
            }
        }

        private void novoUsuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_NovoUsuario f_NovoUsuario = new F_NovoUsuario();
            abreForm(Globais.nivel, f_NovoUsuario);
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_GestaoUsuarios f_GestaoUsuarios = new F_GestaoUsuarios(this);
            abreForm(Globais.nivel, f_GestaoUsuarios);
               
        }

        private void novoAlunoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globais.logado)
            {
                //ToDo
            }
            else
            {
                MessageBox.Show("É necessario ter um usuario logado.");
            }
        }

        private void horáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_Horarios f_Horarios = new F_Horarios();
            abreForm(Globais.nivel, f_Horarios);
        }
    }
}
