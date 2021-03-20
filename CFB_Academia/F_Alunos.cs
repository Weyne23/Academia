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
        public F_Alunos()
        {
            InitializeComponent();
        }

        private DataTable carregarTurmas()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Turma", typeof(string));
            dt.Columns.Add("Horário", typeof(string));
            dt.Columns.Add("Max. Alunos", typeof(string));
            dt.Columns.Add("Qtd. Alunos", typeof(string));
            using (var ctx = new AcademiaContexto())
            {
                var turmas = (from t in ctx.Turmas
                              join h in ctx.Horarios on t.HorarioID equals h.HorarioID
                              where t.Status == "A"
                              orderby h.DesHorario
                              select new
                              {
                                  idTurma = t.TurmaID,
                                  desTurma = t.DesTurma,
                                  maxAlunos = t.MaxAlunos,
                                  desHorario = h.DesHorario,
                              });

                foreach (var t in turmas)
                {
                    var numAlunos = ctx.AlunoTurmas.Where(at => at.TurmaID == t.idTurma)
                        .Include(a => a.Aluno)
                        .Where(a => a.Aluno.Status == "A")
                        .Count();

                    if (numAlunos < t.maxAlunos)
                    {
                        dt.Rows.Add(t.idTurma, t.desTurma, t.desHorario, t.maxAlunos, numAlunos);
                    }
                }
            }

            return dt;
        }

        private void F_Alunos_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> status = new Dictionary<string, string>();
            status.Add("A", "Ativo");
            status.Add("B", "Bloqueado");
            status.Add("C", "Cancelado");
            cb_status.DataSource = new BindingSource(status, null);

            cb_status.DisplayMember = "Value";
            cb_status.ValueMember = "Key";

            DataTable dt = carregarTurmas();

            dgv_turmas.DataSource = dt;
            dgv_turmas.Columns[0].Width = 30;
            dgv_turmas.Columns[1].Width = 60;
            dgv_turmas.Columns[2].Width = 120;
            dgv_turmas.Columns[3].Width = 90;
            dgv_turmas.Columns[4].Width = 50;
            dgv_turmas.Columns[5].Width = 50;
        }

        private void btn_novoAluno_Click(object sender, EventArgs e)
        {
            tb_nomeAluno.Enabled = true;
            cb_status.Enabled = true;
            mtb_telefone.Enabled = true;
            dgv_turmas.Enabled = true;
            tb_nomeAluno.Clear();
            tb_plano.Clear();

            for (int i = 0; i < dgv_turmas.Rows.Count; i++)
            {
                dgv_turmas.Rows[i].Cells[0].Value = 0;
            }

            cb_status.SelectedIndex = -1;
            mtb_telefone.Clear();
            tb_nomeAluno.Focus();
            btn_gravarAluno.Enabled = true;
            btn_cancelar.Enabled = true;
            btn_novoAluno.Enabled = false;
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            tb_nomeAluno.Enabled = false;
            cb_status.Enabled = false;
            mtb_telefone.Enabled = false;
            dgv_turmas.Enabled = false;
            tb_nomeAluno.Clear();
            tb_plano.Clear();

            for (int i = 0; i < dgv_turmas.Rows.Count; i++)
            {
                dgv_turmas.Rows[i].Cells[0].Value = 0;
            }

            cb_status.SelectedIndex = -1;
            mtb_telefone.Clear();
            tb_nomeAluno.Focus();
            btn_gravarAluno.Enabled = false;
            btn_cancelar.Enabled = false;
            btn_novoAluno.Enabled = true;
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
                using (var ctx = new AcademiaContexto())
                {
                    aluno.Nome = tb_nomeAluno.Text;
                    aluno.Telefone = mtb_telefone.Text;
                    aluno.Status = cb_status.SelectedValue.ToString();


                    for (int i = 0; i < dgv_turmas.Rows.Count; i++)
                    {
                        if (dgv_turmas.Rows[i].Cells[0].Value.ToString() == "1")
                        {
                            var turma = ctx.Turmas.Find(Convert.ToInt32(dgv_turmas.Rows[i].Cells[1].Value));
                            ctx.Add(new AlunoTurma { Aluno = aluno, Turma = turma });
                        }
                    }

                    ctx.Add(aluno);
                    ctx.SaveChanges();

                    DataTable dt = carregarTurmas();
                    dgv_turmas.DataSource = dt;
                }

                tb_nomeAluno.Enabled = false;
                cb_status.Enabled = false;
                mtb_telefone.Enabled = false;
                dgv_turmas.Enabled = false;
                tb_nomeAluno.Clear();
                tb_plano.Clear();

                for (int i = 0; i < dgv_turmas.Rows.Count; i++)
                {
                    dgv_turmas.Rows[i].Cells[0].Value = 0;
                }

                cb_status.SelectedIndex = -1;
                mtb_telefone.Clear();
                tb_nomeAluno.Focus();
                btn_gravarAluno.Enabled = false;
                btn_cancelar.Enabled = false;
                btn_novoAluno.Enabled = true;
                MessageBox.Show("Aluno Adicionado", "Mensagem");
            }
        }

        private void dgv_turmas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_turmas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "0")
            {
                dgv_turmas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
            }
            else if(dgv_turmas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "1")
            {
                dgv_turmas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
            }
        }
    }
}
