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
    public partial class F_Horarios : Form
    {
        string mtb_teste;
        public F_Horarios()
        {
            InitializeComponent();
            mtb_teste = mtb_desHorario.Text;
        }

        private void carregarHorarios(int tamanoId, int tamanhoDesHorario)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var ctx = new AcademiaContexto())
                {
                    var horarios = ctx.Horarios.OrderBy(h => h.DesHorario).ToList();
                    dt.Columns.Add("Id", typeof(int));
                    dt.Columns.Add("Horario", typeof(string));

                    foreach (var u in horarios)
                    {
                        dt.Rows.Add(u.HorarioID, u.DesHorario);
                    }
                }

                dgv_horarios.DataSource = dt;
                dgv_horarios.Columns[0].Width = tamanoId;
                dgv_horarios.Columns[1].Width = tamanhoDesHorario;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message, "Erro");
            }
        }

        private void limparCampos()
        {
            mtb_desHorario.Clear();
            tb_idHorario.Clear();
        }

        private void F_Horarios_Load(object sender, EventArgs e)
        {
            carregarHorarios(140, 270);
        }

        private void dgv_horarios_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int numLinhas = dgv.SelectedRows.Count;
            if (numLinhas > 0)
            {
                string idHorario = dgv.SelectedRows[0].Cells[0].Value.ToString();
                string statusHorario = dgv.SelectedRows[0].Cells[1].Value.ToString();
                tb_idHorario.Text = idHorario;
                mtb_desHorario.Text = statusHorario;
            }
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_excluirHorario_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir o horário de " + mtb_desHorario.Text, "Excluir", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idHorario = Convert.ToInt32(dgv_horarios.SelectedRows[0].Cells[0].Value);
                using (var ctx = new AcademiaContexto())
                {
                    var horario = ctx.Horarios.Find(idHorario);
                    ctx.Remove(horario);
                    ctx.SaveChanges();
                    dgv_horarios.Rows.Remove(dgv_horarios.SelectedRows[0]);
                    limparCampos();
                }
            }
        }

        private void btn_novoHorario_Click(object sender, EventArgs e)
        {
            limparCampos();
            mtb_desHorario.Focus();
        }

        private void btn_salvarHorario_Click(object sender, EventArgs e)
        {
            if (tb_idHorario.Text == "")
            {
                if (mtb_desHorario.Text != mtb_teste)
                {
                    using (var ctx = new AcademiaContexto())
                    {
                        var existeHor = ctx.Horarios.SingleOrDefault(h => h.DesHorario == mtb_desHorario.Text);

                        if (existeHor == null)
                        {
                            Horario horario = new Horario();
                            horario.DesHorario = mtb_desHorario.Text;
                            ctx.Add(horario);
                            ctx.SaveChanges();
                            MessageBox.Show("horário adicionado!", "Mensagem");
                            carregarHorarios(140, 270);
                        }
                        else
                        {
                            MessageBox.Show("Horário já Adicionado!", "Erro");
                            mtb_desHorario.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Campo horário obrigatorio", "Erro");
                    mtb_desHorario.Focus();
                } 
            }
            else
            {
                if (mtb_desHorario.Text != "")
                {
                    using (var ctx = new AcademiaContexto())
                    {
                        int idHorario = Convert.ToInt32(tb_idHorario.Text);
                        var existeHor = ctx.Horarios.SingleOrDefault(h => h.DesHorario == mtb_desHorario.Text);
                        if (existeHor == null)
                        {
                            var horario = ctx.Horarios.Find(idHorario);
                            ctx.SaveChanges();
                            horario.DesHorario = mtb_desHorario.Text;
                            dgv_horarios.SelectedRows[0].Cells[1].Value = mtb_desHorario.Text;
                            MessageBox.Show("horário atualizado!", "Mensagem");
                        }
                        else
                        {
                            MessageBox.Show("Horário já Adicionado.", "Erro");
                            mtb_desHorario.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Campo horário obrigatorio", "Erro");
                    mtb_desHorario.Focus();
                }
            }
        }
    }
}
