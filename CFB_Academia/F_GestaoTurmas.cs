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
    public partial class F_GestaoTurmas : Form
    {
        public F_GestaoTurmas()
        {
            InitializeComponent();
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
                                      desTurma = t.DesTruma,
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
                MessageBox.Show("Erro" + ex.Message, "Erro");
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
                                    .Select(p => new { p.Nome,  p.ProfessorID })
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
    }
}
