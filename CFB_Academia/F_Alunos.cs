using CFB_Academia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFB_Academia
{
    public partial class F_Alunos : Form
    {
        List<int> selecionados = new List<int>();
        public F_Alunos()
        {
            InitializeComponent();
        }

        private void F_Alunos_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> status = new Dictionary<string, string>();
            DataTable dt = new DataTable();
            status.Add("A", "Ativo");
            status.Add("B", "Bloqueado");
            status.Add("C", "Cancelado");
            cb_status.DataSource = new BindingSource(status, null);

            cb_status.DisplayMember = "Value";
            cb_status.ValueMember = "Key";

            using (var ctx = new AcademiaContexto())
            {
                var turmas = (from t in ctx.Turmas
                              join h in ctx.Horarios on t.HorarioID equals h.HorarioID
                              where t.Status == "A"
                              select new
                              {
                                  idTurma = t.TurmaID,
                                  desTurma = t.DesTurma,
                                  maxAlunos = t.MaxAlunos,
                                  desHorario = h.DesHorario,
                              });
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Turma", typeof(string));
                foreach (var t in turmas)
                {
                    var numAlunos = ctx.AlunoTurmas.Where(at => at.TurmaID == t.idTurma)
                        .Include(a => a.Aluno)
                        .Where(a => a.Aluno.Status == "A")
                        .Count();

                    if (numAlunos < t.maxAlunos)
                    {
                        dt.Rows.Add(t.idTurma, t.desTurma + " / " + t.desHorario);
                    }
                }

                ((ListBox)clb_turmas).DataSource = dt;
                ((ListBox)clb_turmas).ValueMember = "Id";
                ((ListBox)clb_turmas).DisplayMember = "Turma";
            }
        }

        private void btn_novoAluno_Click(object sender, EventArgs e)
        {
            tb_nomeAluno.Enabled = true;
            cb_status.Enabled = true;
            mtb_telefone.Enabled = true;
            clb_turmas.Enabled = true;
            tb_nomeAluno.Clear();
            tb_plano.Clear();

            foreach (int i in clb_turmas.CheckedIndices)
            {
                clb_turmas.SetItemChecked(i, false);
            }

            cb_status.SelectedIndex = -1;
            mtb_telefone.Clear();
            tb_nomeAluno.Focus();
            btn_gravarAluno.Enabled = true;
            btn_cancelar.Enabled = true;
            btn_novoAluno.Enabled = false;
            selecionados.Clear();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            tb_nomeAluno.Enabled = false;
            cb_status.Enabled = false;
            mtb_telefone.Enabled = false;
            clb_turmas.Enabled = false;
            tb_nomeAluno.Clear();
            tb_plano.Clear();

            foreach (int i in clb_turmas.CheckedIndices)
            {
                clb_turmas.SetItemChecked(i, false);
            }

            cb_status.SelectedIndex = -1;
            mtb_telefone.Clear();
            tb_nomeAluno.Focus();
            btn_gravarAluno.Enabled = false;
            btn_cancelar.Enabled = false;
            btn_novoAluno.Enabled = true;
            selecionados.Clear();
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_gravarAluno_Click(object sender, EventArgs e)
        {
            Aluno aluno = new Aluno();
            if (cb_status.Text == "" || tb_nomeAluno.Text == "" || aluno.Telefone == "") 
            {
                MessageBox.Show("Campos \"Nome\", \"Status\" e \"Telefone\" obrigratorios.", "Erro");
            }
            else
            {
                if(selecionados.Count > 0)
                {
                    using (var ctx = new AcademiaContexto())
                    {
                        aluno.Nome = tb_nomeAluno.Text;
                        aluno.Telefone = mtb_telefone.Text;
                        aluno.Status = cb_status.SelectedValue.ToString();
                        foreach (var i in selecionados)
                        {
                            var turma = ctx.Turmas.Find(i);
                            ctx.Add(new AlunoTurma { Aluno = aluno, Turma = turma });
                        }

                        ctx.SaveChanges();
                    }
                }
                else
                {
                    using (var ctx = new AcademiaContexto())
                    {
                        aluno.Nome = tb_nomeAluno.Text;
                        aluno.Telefone = mtb_telefone.Text;
                        aluno.Status = cb_status.SelectedValue.ToString();
                        ctx.Add(aluno);
                        ctx.SaveChanges();
                    }
                }

                tb_nomeAluno.Enabled = false;
                cb_status.Enabled = false;
                mtb_telefone.Enabled = false;
                clb_turmas.Enabled = false;
                tb_nomeAluno.Clear();
                tb_plano.Clear();

                foreach (int i in clb_turmas.CheckedIndices)
                {
                    clb_turmas.SetItemChecked(i, false);
                }

                cb_status.SelectedIndex = -1;
                mtb_telefone.Clear();
                tb_nomeAluno.Focus();
                btn_gravarAluno.Enabled = false;
                btn_cancelar.Enabled = false;
                btn_novoAluno.Enabled = true;
                selecionados.Clear();
                MessageBox.Show("Aluno Adicionado", "Mensagem");
            }
        }

        private void clb_turmas_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!selecionados.Contains(Convert.ToInt32(((ListBox)clb_turmas).SelectedValue)))
                selecionados.Add(Convert.ToInt32(((ListBox)clb_turmas).SelectedValue));
            else if(selecionados.Contains(Convert.ToInt32(((ListBox)clb_turmas).SelectedValue)))
                selecionados.Remove(Convert.ToInt32(((ListBox)clb_turmas).SelectedValue));
        }
    }
}
