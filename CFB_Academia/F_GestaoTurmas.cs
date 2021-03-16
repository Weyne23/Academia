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
    public partial class F_GestaoTurmas : Form
    {
        int idTurma;
        int modo = 0; //0 = neutro, 1 = Edição, 2 = atualização
        public F_GestaoTurmas()
        {
            InitializeComponent();
        }

        private void limparCampos()
        {
            tb_nomeTurma.Clear();
            cb_horario.SelectedIndex = -1;
            cb_nomeProfessor.SelectedIndex = -1;
            n_maxAlunos.Value = 1;
            cb_status.SelectedIndex = -1;
            tb_vagasRestantes.Clear();
        }

        public string numVagas(int maxAlunos)
        {
            using (var ctx = new AcademiaContexto())
            {
                var numAluno = ctx.AlunoTurmas // Query para numero de alunos
                            .Where(at => at.TurmaID == idTurma)
                            .Include(a => a.Aluno)
                            .Where(a => a.Aluno.Status == "A")
                            .Count();

                int numVagas = maxAlunos - numAluno;
                return numVagas.ToString();
            } 
        }

        private void carregarTurmas(int tamId, int tamTurma, int tamHorario)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Turma", typeof(string));
            dt.Columns.Add("Horário", typeof(string));

            try
            {
                using (var ctx = new AcademiaContexto())
                {
                    var turmas = (from t in ctx.Turmas
                                  join h in ctx.Horarios on t.HorarioID equals h.HorarioID
                                  select new
                                  {
                                      idTurma = t.TurmaID,
                                      desTurma = t.DesTurma,
                                      desHorario = h.DesHorario,
                                  });
                    foreach (var t in turmas)
                    {
                        dt.Rows.Add(t.idTurma, t.desTurma, t.desHorario);
                    }

                    dgv_turmas.DataSource = dt;
                    dgv_turmas.Columns[0].Width = tamId;
                    dgv_turmas.Columns[1].Width = tamTurma;
                    dgv_turmas.Columns[2].Width = tamHorario;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message, "Erro");
            }
        }

        private void carregarProfessores()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Professor", typeof(string));
            using (var ctx = new AcademiaContexto())
            {
                var professores = ctx.Professores
                                    .Select(p => new { p.Nome, p.ProfessorID })
                                    .OrderBy(p => p.ProfessorID);
                cb_nomeProfessor.Items.Clear();

                foreach (var p in professores)
                {
                    dt.Rows.Add(p.ProfessorID, p.Nome);
                }

                cb_nomeProfessor.DataSource = dt;
                cb_nomeProfessor.DisplayMember = "Professor";
                cb_nomeProfessor.ValueMember = "Id";
            }
        }

        public void carregarStatus()
        {
            Dictionary<string, string> st = new Dictionary<string, string>();
            st.Add("A", "Ativa");
            st.Add("P", "Paralizada");
            st.Add("C", "Cancelada");
            cb_status.Items.Clear();
            cb_status.DataSource = new BindingSource(st, null);
            cb_status.DisplayMember = "Value";
            cb_status.ValueMember = "Key";
        }

        void carregarHorarios()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Horario", typeof(string));
            using (var ctx = new AcademiaContexto())
            {
                List<Horario> horarios = ctx.Horarios.OrderBy(h => h.DesHorario).ToList();

                foreach (var h in horarios)
                {
                    dt.Rows.Add(h.HorarioID, h.DesHorario);
                }

                cb_horario.DataSource = dt;
                cb_horario.DisplayMember = "Horario";
                cb_horario.ValueMember = "Id";
            }
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void F_GestaoTurmas_Load(object sender, EventArgs e)
        {
            carregarTurmas(40, 175, 90);
            carregarProfessores();
            carregarStatus();
            carregarHorarios();
        }

        private void dgv_turmas_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int numLinhas = dgv.SelectedRows.Count;
            if (numLinhas > 0)
            {
                modo = 1;
                idTurma = Convert.ToInt32(dgv.SelectedRows[0].Cells[0].Value);
                using (var ctx = new AcademiaContexto())
                {
                    var turma = ctx.Turmas.Where(t => t.TurmaID == idTurma)
                        .Select(t => new { t.ProfessorID, t.HorarioID, t.DesTurma, t.Status, t.MaxAlunos })
                        .First();

                    cb_horario.SelectedValue = turma.HorarioID;
                    cb_nomeProfessor.SelectedValue = turma.ProfessorID;
                    cb_status.SelectedValue = turma.Status;
                    tb_nomeTurma.Text = turma.DesTurma;
                    n_maxAlunos.Value = turma.MaxAlunos;
                    tb_vagasRestantes.Text = numVagas(turma.MaxAlunos);
                }
            }
        }

        private void btn_novaTurma_Click(object sender, EventArgs e)
        {
            limparCampos();
            tb_nomeTurma.Focus();
            modo = 2;
        }

        private void btn_salvarEdicoes_Click(object sender, EventArgs e)
        {
            if (modo != 0)
            {
                if (modo == 1)
                {
                    int linha = dgv_turmas.SelectedRows[0].Index;
                    using (var ctx = new AcademiaContexto())
                    {
                        var turma = ctx.Turmas.Find(idTurma);
                        turma.ProfessorID = Convert.ToInt32(cb_nomeProfessor.SelectedValue);
                        turma.MaxAlunos = Convert.ToInt32(Math.Round(n_maxAlunos.Value, 0));
                        turma.HorarioID = Convert.ToInt32(cb_horario.SelectedValue);
                        turma.DesTurma = tb_nomeTurma.Text;
                        turma.Status = cb_status.SelectedValue.ToString();
                        ctx.SaveChanges();
                        dgv_turmas.Rows[linha].Cells[1].Value = tb_nomeTurma.Text;
                        dgv_turmas.Rows[linha].Cells[2].Value = cb_horario.Text;
                        tb_vagasRestantes.Text = numVagas(turma.MaxAlunos);
                        MessageBox.Show("Dados Editados", "Mensagem");
                    }
                }
                else
                {
                    using (var ctx = new AcademiaContexto())
                    {
                        var exisTurma = ctx.Turmas.SingleOrDefault(t => t.DesTurma == tb_nomeTurma.Text);
                        if (exisTurma == null)
                        {
                            Turma turma = new Turma();
                            turma.ProfessorID = Convert.ToInt32(cb_nomeProfessor.SelectedValue);
                            turma.MaxAlunos = Convert.ToInt32(Math.Round(n_maxAlunos.Value, 0));
                            turma.HorarioID = Convert.ToInt32(cb_horario.SelectedValue);
                            turma.DesTurma = tb_nomeTurma.Text;
                            turma.Status = cb_status.SelectedValue.ToString();
                            ctx.Turmas.Add(turma);
                            ctx.SaveChanges();
                            carregarTurmas(40, 175, 90);
                            MessageBox.Show("Turma adicionada!", "Mensagem");
                        }
                        else
                        {
                            MessageBox.Show("Já existe uma turma com esse nome.", "Erro");
                        }
                    }
                }
            }
            
        }

        private void btn_excluirTurma_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir essa turma?", "Mensagem", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int linha = dgv_turmas.SelectedRows[0].Index;
                using (var ctx = new AcademiaContexto())
                {
                    var turma = ctx.Turmas.Find(Convert.ToInt32(dgv_turmas.Rows[linha].Cells[0].Value));
                    ctx.Remove(turma);
                    ctx.SaveChanges();
                    MessageBox.Show("Turma excluida!", "Mensagem");
                    dgv_turmas.Rows.Remove(dgv_turmas.SelectedRows[0]);
                }
            }
        }
    }
}