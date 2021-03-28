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
    public partial class F_GestaoProfessores : Form
    {
        string mtb_teste;
        public F_GestaoProfessores()
        {
            InitializeComponent();
            mtb_teste = mtb_telefone.Text;
        }

        private void limparCampos()
        {
            tb_idProfessor.Clear();
            tb_nome.Clear();
            mtb_telefone.Clear();
        }

        public void carregarProfessores(int tamId, int tamNome, int tamTelefone)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nome", typeof(string));
            dt.Columns.Add("Telefone", typeof(string));
            try
            {
                using (var ctx = new AcademiaContexto())
                {
                    var professores = ctx.Professores.ToList();

                    foreach (var p in professores)
                    {
                        dt.Rows.Add(p.ProfessorID, p.Nome, p.Telefone);
                    }
                }

                dgv_professores.DataSource = dt;
                dgv_professores.Columns[0].Width = tamId;
                dgv_professores.Columns[1].Width = tamNome;
                dgv_professores.Columns[2].Width = tamTelefone;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message, "Erro");
            }
        }

        private void F_GestaoProfessores_Load(object sender, EventArgs e)
        {
            carregarProfessores(60, 240, 100);
        }

        private void dgv_professores_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int numLinhas = dgv.SelectedRows.Count;
            if (numLinhas > 0)
            {
                tb_idProfessor.Text = dgv.SelectedRows[0].Cells[0].Value.ToString();
                tb_nome.Text = dgv.SelectedRows[0].Cells[1].Value.ToString();
                mtb_telefone.Text = dgv.SelectedRows[0].Cells[2].Value.ToString();
            }
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_novoProfessor_Click(object sender, EventArgs e)
        {
            limparCampos();
            tb_nome.Focus();
        }

        private void btn_excluirProfessor_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir o professor "+ tb_nome.Text + "?", "Mensagem", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idProfessor = Convert.ToInt32(tb_idProfessor.Text);
                using (var ctx = new AcademiaContexto())
                {
                    var professor = ctx.Professores.Find(idProfessor);
                    ctx.Remove(professor);
                    ctx.SaveChanges();
                    dgv_professores.Rows.Remove(dgv_professores.SelectedRows[0]);
                    limparCampos();
                }
            }
           
        }

        private void btn_salvarProfessor_Click(object sender, EventArgs e)
        {
            if (tb_idProfessor.Text == "")
            {
                if (tb_nome.Text == "" || mtb_telefone.Text == mtb_teste)
                {
                    MessageBox.Show("Campos nome e telefone são obrigratorios.", "Erro");
                    tb_nome.Focus();
                }
                else
                {
                    using (var ctx = new AcademiaContexto())
                    {
                        var existeProf = ctx.Professores.SingleOrDefault(p => p.Nome == tb_nome.Text);
                        if (existeProf == null)
                        {
                            Professor professor = new Professor();
                            professor.Nome = tb_nome.Text;
                            professor.Telefone = mtb_telefone.Text;
                            ctx.Add(professor);
                            ctx.SaveChanges();
                            carregarProfessores(60, 240, 100);
                        }
                        else
                        {
                            MessageBox.Show("Professor já cadastrado.", "Erro");
                            tb_nome.Focus();
                        }
                    }
                }  
            }
            else
            {
                if (tb_nome.Text == "" || mtb_telefone.Text == mtb_teste)
                {
                    MessageBox.Show("Campos nome e telefone são obrigratorios.", "Erro");
                    tb_nome.Focus();
                }
                else
                {
                    using (var ctx = new AcademiaContexto())
                    {
                        var existeProf = ctx.Professores.SingleOrDefault(p => p.Nome == tb_nome.Text);
                        int idProfessor = Convert.ToInt32(tb_idProfessor.Text);
                        if (existeProf == null)
                        {
                            var professor = ctx.Professores.Find(idProfessor);
                            professor.Nome = tb_nome.Text;
                            professor.Telefone = mtb_telefone.Text;
                            ctx.SaveChanges();
                            MessageBox.Show("Informações do professor atualizadas.", "Erro");
                            dgv_professores.SelectedRows[0].Cells[1].Value = tb_nome.Text;
                            dgv_professores.SelectedRows[0].Cells[2].Value = mtb_telefone.Text;
                        }
                        else
                        {
                            MessageBox.Show("Professor já cadastrado.", "Erro");
                            tb_nome.Focus();
                        }
                    }
                }
            }
        }
    }
}
