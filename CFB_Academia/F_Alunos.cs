using CFB_Academia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CFB_Academia
{
    public partial class F_Alunos : Form
    {
        string origemCompleto = "";
        string foto = "";
        string pastaDestino = Globais.caminhoFoto;
        string destinoCompleto = "";
        string mascara = "";

        public F_Alunos()
        {
            InitializeComponent();
        }

        private DataTable carregarTurmas()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(" ", typeof(bool));
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
                                  numAluno = (from at in ctx.AlunoTurmas
                                              join al in ctx.Alunos on at.AlunoID equals al.AlunoID
                                              where at.TurmaID == t.TurmaID && al.Status == "A" || al.Status == "B"
                                              select al).Count()
                              });

                foreach (var t in turmas)
                {
                    dt.Rows.Add(false, t.idTurma, t.desTurma, t.desHorario, t.maxAlunos, t.numAluno);
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

            dgv_turmas.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            dgv_turmas.Columns[0].Width = 30;
            dgv_turmas.Columns[1].Width = 60;
            dgv_turmas.Columns[2].Width = 120;
            dgv_turmas.Columns[3].Width = 90;
            dgv_turmas.Columns[4].Width = 50;
            dgv_turmas.Columns[5].Width = 50;
            mascara = mtb_telefone.Text;
            dgv_turmas.AllowUserToResizeRows = false;
            dgv_turmas.AllowUserToResizeColumns = false;
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
            pb_fotoAluno.ImageLocation = "";
            destinoCompleto = "";
            mtb_telefone.Clear();
            tb_nomeAluno.Focus();
            btn_gravarAluno.Enabled = true;
            btn_cancelar.Enabled = true;
            btn_addFoto.Enabled = true;
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
            pb_fotoAluno.ImageLocation = "";
            destinoCompleto = "";
            mtb_telefone.Clear();
            tb_nomeAluno.Focus();
            btn_gravarAluno.Enabled = false;
            btn_cancelar.Enabled = false;
            btn_addFoto.Enabled = false;
            btn_novoAluno.Enabled = true;
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_gravarAluno_Click(object sender, EventArgs e)
        {
            Aluno aluno = new Aluno();
            if (cb_status.Text == "" || tb_nomeAluno.Text == "" || mtb_telefone.Text == mascara)
            {
                MessageBox.Show("Campos \"Nome\", \"Status\" e \"Telefone\" obrigratorios.", "Erro");
            }
            else
            {
                if (destinoCompleto == "")
                {
                    if (MessageBox.Show("Sem foto selecionada, desejá continuar?", "Mensagem", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (destinoCompleto != "")
                {
                    System.IO.File.Copy(origemCompleto, destinoCompleto, true);
                    if (File.Exists(destinoCompleto))
                    {
                        pb_fotoAluno.ImageLocation = destinoCompleto;
                    }
                    else
                    {
                        if (MessageBox.Show("Erro ao localizar a foto, desejá continuar?", "Mensagem", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                using (var ctx = new AcademiaContexto())
                {
                    aluno.Nome = tb_nomeAluno.Text;
                    aluno.Telefone = mtb_telefone.Text;
                    aluno.Status = cb_status.SelectedValue.ToString();
                    aluno.Foto = destinoCompleto;
                    aluno.DataDeEntrada = DateTime.Now.Date;

                    for (int i = 0; i < dgv_turmas.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgv_turmas.Rows[i].Cells[0].Value) == true)
                        {
                            if (Convert.ToInt32(dgv_turmas.Rows[i].Cells[5].Value) >= Convert.ToInt32(dgv_turmas.Rows[i].Cells[4].Value))
                            {
                                MessageBox.Show("uma ou mais Turma(s) selecionada(s) está(ão) lotada(s)!", "Mensagem");
                                return;
                            }
                            else
                            {
                                var turma = ctx.Turmas.Find(Convert.ToInt32(dgv_turmas.Rows[i].Cells[1].Value));
                                ctx.Add(new AlunoTurma { Aluno = aluno, Turma = turma });
                            }
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
                pb_fotoAluno.ImageLocation = "";
                destinoCompleto = "";
                btn_gravarAluno.Enabled = false;
                btn_cancelar.Enabled = false;
                btn_addFoto.Enabled = false;
                btn_novoAluno.Enabled = true;
                MessageBox.Show("Aluno Adicionado", "Mensagem");
            }
        }

        private void dgv_turmas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_turmas.Rows[e.RowIndex].Cells[0].Selected)
            {
                dgv_turmas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !Convert.ToBoolean(dgv_turmas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }

        private void btn_addFoto_Click(object sender, EventArgs e)
        {
            origemCompleto = "";
            foto = "";
            pastaDestino = Globais.caminhoFoto;
            destinoCompleto = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                origemCompleto = openFileDialog1.FileName;//Retorna o caminho com o nome do arquivo
                foto = openFileDialog1.SafeFileName;//Retorna so o nome do arquivo
                destinoCompleto = pastaDestino + foto;
            }

            if (File.Exists(destinoCompleto))
            {
                if (MessageBox.Show("Arquivo já existe, desejá substituir?", "Substituir", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            pb_fotoAluno.ImageLocation = origemCompleto;
        }

        private void dgv_turmas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.ColumnIndex == dgv.Columns[" "].Index)
            {
                dgv.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells[0].Value);
            }
        }
    }
}
