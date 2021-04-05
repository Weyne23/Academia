using CFB_Academia.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CFB_Academia
{
    public partial class F_GestaoAlunos : Form
    {
        List<int> turmasDoAluno = new List<int>();
        List<int> nTurmasDoAluno = new List<int>();
        string[] plano;
        Aluno alunoAtual;
        bool trocouFoto = false;
        string destinoFotoAntiga = "";
        string origemCompleto = "";
        string destinoCompleto = "";
        string pastaDestino = Globais.caminhoFoto;
        string foto = "";

        public F_GestaoAlunos()
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
                              orderby t.TurmaID
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
        private void F_GestaoAlunos_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Dictionary<string, string> status = new Dictionary<string, string>();
            status.Add("A", "Ativo");
            status.Add("B", "Bloqueado");
            status.Add("C", "Cancelado");

            cb_status.DataSource = new BindingSource(status, null);
            cb_status.ValueMember = "Key";
            cb_status.DisplayMember = "Value";

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nome", typeof(string));

            using (var ctx = new AcademiaContexto())
            {
                var alunos = ctx.Alunos.ToList();

                foreach (var a in alunos)
                {
                    dt.Rows.Add(a.AlunoID, a.Nome);
                }

                var turmas = (from t in ctx.Turmas
                              join h in ctx.Horarios on t.HorarioID equals h.HorarioID
                              where t.Status == "A"
                              orderby t.TurmaID
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
            }

            dgv_turmas.DataSource = carregarTurmas();
            dgv_alunos.DataSource = dt;
            dgv_alunos.Columns[0].Width = 45;
            dgv_alunos.Columns[1].Width = 244;
            dgv_turmas.Columns[0].Width = 30;
            dgv_turmas.Columns[1].Width = 60;
            dgv_turmas.Columns[2].Width = 120;
            dgv_turmas.Columns[3].Width = 90;
            dgv_turmas.Columns[4].Width = 50;
            dgv_turmas.Columns[5].Width = 50;
            dgv_alunos.AllowUserToResizeRows = false;
            dgv_alunos.AllowUserToResizeColumns = false;
            dgv_turmas.AllowUserToResizeRows = false;
            dgv_turmas.AllowUserToResizeColumns = false;
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_alunos_SelectionChanged(object sender, EventArgs e)
        {
            int numLinhas = dgv_alunos.SelectedRows.Count;
            if (numLinhas > 0)
            {
                for (int i = 0; i < dgv_turmas.Rows.Count; i++)
                {
                    dgv_turmas.Rows[i].Cells[0].Value = false;
                    dgv_turmas.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }

                turmasDoAluno.Clear();

                int idAluno = Convert.ToInt32(dgv_alunos.SelectedRows[0].Cells[0].Value);
                int indexAluno = dgv_alunos.SelectedRows[0].Index;
                using (var ctx = new AcademiaContexto())
                {
                    var turmasAluno = ctx.AlunoTurmas.Where(at => at.AlunoID == idAluno)
                                                     .ToList();

                    foreach (var ta in turmasAluno)
                    {
                        int idTurma = ta.TurmaID;
                        for (int i = 0; i < dgv_turmas.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dgv_turmas.Rows[i].Cells[1].Value) == idTurma)
                            {
                                dgv_turmas.Rows[i].Cells[0].Value = true;
                                dgv_turmas.Rows[i].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                                turmasDoAluno.Add(idTurma);
                                break;
                            }
                        }
                    }
                    var aluno = ctx.Alunos.Find(idAluno);
                    alunoAtual = aluno;
                    tb_nomeAluno.Text = aluno.Nome;
                    mtb_telefone.Text = aluno.Telefone;
                    cb_status.SelectedValue = aluno.Status;
                    tb_dataDeEntrada.Text = aluno.DataDeEntrada.ToString("dd/MM/yyyy");
                    pb_fotoAluno.ImageLocation = aluno.Foto;
                    mtb_plano.Text = aluno.Plano;
                    plano = mtb_plano.Text.Split(' ');

                    try
                    {
                        double totalPorMes = Convert.ToDouble(plano[plano.Length - 1].Replace('.', ',')) / Convert.ToInt32(plano[0]);
                        tb_total.Text = totalPorMes.ToString("N2");
                    }
                    catch
                    {
                        tb_total.Text = "Erro";
                    }
                    
                    trocouFoto = false;
                    destinoFotoAntiga = pb_fotoAluno.ImageLocation;
                }
            }
        }

        private void dgv_turmas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Rows[e.RowIndex].Cells[0].Selected)
            {
                dgv.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            int idAluno = Convert.ToInt32(dgv_alunos.SelectedRows[0].Cells[0].Value);
            if (MessageBox.Show("Deseja excluir esse aluno?", "Mensagem", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (File.Exists(pb_fotoAluno.ImageLocation))
                {
                    File.Delete(pb_fotoAluno.ImageLocation);
                }
                using (var ctx = new AcademiaContexto())
                {
                    var aluno = ctx.Alunos.Find(idAluno);
                    ctx.Remove(aluno);
                    ctx.SaveChanges();
                    tb_nomeAluno.Clear();
                    cb_status.SelectedIndex = -1;
                    mtb_telefone.Clear();
                    dgv_alunos.Rows.Remove(dgv_alunos.SelectedRows[0]);
                    dgv_turmas.DataSource = carregarTurmas();
                    MessageBox.Show("Aluno excluido", "Mensagem");
                }
            }
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            using (var ctx = new AcademiaContexto())
            {
                string turmasLotadas = "Aluno não adicionado na turma(s) pois esta(ão) lotada(s):";
                int tamanhoStringTL = turmasLotadas.Length;
                var aluno = ctx.Alunos.Find(Convert.ToInt32(dgv_alunos.SelectedRows[0].Cells[0].Value));
                nTurmasDoAluno.Clear();
                aluno.Telefone = mtb_telefone.Text;
                aluno.Status = cb_status.SelectedValue.ToString();
                aluno.Nome = tb_nomeAluno.Text;
                aluno.Plano = mtb_plano.Text;
                dgv_alunos[1, dgv_alunos.SelectedRows[0].Index].Value = tb_nomeAluno.Text;

                if (trocouFoto)
                {
                    if(destinoFotoAntiga != null)
                    {
                        if (File.Exists(destinoFotoAntiga))
                        {
                            File.Delete(destinoFotoAntiga);
                        }
                    }
                    System.IO.File.Copy(origemCompleto, destinoCompleto, true);
                    if (File.Exists(destinoCompleto))
                    {
                        pb_fotoAluno.ImageLocation = destinoCompleto;
                    }
                    aluno.Foto = destinoCompleto;
                }

                if (cb_status.SelectedValue.ToString() == "C")
                {
                    for (int i = 0; i < dgv_turmas.Rows.Count; i++)
                    {
                        dgv_turmas.Rows[i].Cells[0].Value = false;
                        dgv_turmas.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        int idTurma = Convert.ToInt32(dgv_turmas.Rows[i].Cells[1].Value);
                        if (turmasDoAluno.Contains(idTurma))
                        {
                            var turmaAluno = ctx.AlunoTurmas
                                           .Where(at =>
                                           at.TurmaID == idTurma
                                           &&
                                           at.AlunoID == Convert.ToInt32(dgv_alunos.SelectedRows[0].Cells[0].Value)).First();
                            ctx.Remove(turmaAluno);
                            dgv_turmas.Rows[i].Cells[5].Value = Convert.ToInt32(dgv_turmas.Rows[i].Cells[5].Value) - 1;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dgv_turmas.Rows.Count; i++)
                    {
                        dgv_turmas.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        int idTurma = Convert.ToInt32(dgv_turmas.Rows[i].Cells[1].Value);
                        if (!turmasDoAluno.Contains(idTurma) && Convert.ToBoolean(dgv_turmas.Rows[i].Cells[0].Value))
                        {
                            if (Convert.ToInt32(dgv_turmas.Rows[i].Cells[5].Value) == Convert.ToInt32(dgv_turmas.Rows[i].Cells[4].Value))
                            {
                                turmasLotadas += "\n" + "Turma de " + dgv_turmas.Rows[i].Cells[2].Value.ToString();
                                dgv_turmas.Rows[i].Cells[0].Value = false;
                            }
                            else
                            {
                                var turma = ctx.Turmas.Find(Convert.ToInt32(idTurma));
                                ctx.Add(new AlunoTurma { Aluno = aluno, Turma = turma });
                                nTurmasDoAluno.Add(idTurma);
                                dgv_turmas.Rows[i].Cells[5].Value = Convert.ToInt32(dgv_turmas.Rows[i].Cells[5].Value) + 1;
                                dgv_turmas.Rows[i].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                            }
                        }
                        else if (turmasDoAluno.Contains(idTurma) && Convert.ToBoolean(dgv_turmas.Rows[i].Cells[0].Value))
                        {
                            nTurmasDoAluno.Add(idTurma);
                            dgv_turmas.Rows[i].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                        }
                        else if (turmasDoAluno.Contains(idTurma) && !Convert.ToBoolean(dgv_turmas.Rows[i].Cells[0].Value))
                        {
                            var turmaAluno = ctx.AlunoTurmas
                                                .Where(at =>
                                                at.TurmaID == idTurma
                                                &&
                                                at.AlunoID == Convert.ToInt32(dgv_alunos.SelectedRows[0].Cells[0].Value)).First();
                            ctx.Remove(turmaAluno);
                            dgv_turmas.Rows[i].Cells[5].Value = Convert.ToInt32(dgv_turmas.Rows[i].Cells[5].Value) - 1;
                        }
                    }
                }  

                turmasDoAluno.Clear();
                turmasDoAluno.AddRange(nTurmasDoAluno);
                trocouFoto = false;
                alunoAtual = aluno;
                plano = mtb_plano.Text.Split(' ');
                double totalPorMes = Convert.ToDouble(plano[plano.Length - 1].Replace('.', ',')) / Convert.ToInt32(plano[0]);
                tb_total.Text = totalPorMes.ToString("N2");
                ctx.SaveChanges();

                if (tamanhoStringTL < turmasLotadas.Length)
                {
                    MessageBox.Show(turmasLotadas + "\n" + "Outras alterações feitas salvas", "Erro");
                }
                else
                {
                    MessageBox.Show("Alterações feitas salvas", "Mensagem");
                }
            }
        }

        private void dgv_turmas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.ColumnIndex == dgv.Columns[" "].Index)
            {
                dgv.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void dgv_turmas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < dgv_turmas.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgv_turmas.Rows[i].Cells[0].Value) == true)
                {
                    dgv_turmas.Rows[i].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                }
            }
        }

        private void pb_fotoAluno_DoubleClick(object sender, EventArgs e)
        {
            destinoCompleto = "";
            origemCompleto = pb_fotoAluno.ImageLocation;
            pastaDestino = Globais.caminhoFoto;
            foto = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                trocouFoto = true;
                foto = openFileDialog1.SafeFileName;
                origemCompleto = openFileDialog1.FileName;
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

        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            string dataVencimento = alunoAtual.Plano;

            string nomeArquivo = Globais.caminho + @"\CarteirinhaAluno.pdf";
            FileStream arquivoPDF = new FileStream(nomeArquivo, FileMode.Create);
            Document doc = new Document(PageSize.A7.Rotate());
            PdfWriter escritorPDF = PdfWriter.GetInstance(doc, arquivoPDF);

            iTextSharp.text.Image imagem = iTextSharp.text.Image.GetInstance(Globais.caminho + @"\imgs\logotipo-recortado.jpg");
            imagem.ScaleToFit(40f, 40f);
            imagem.Alignment = Element.PARAGRAPH;
            //imagem.SetAbsolutePosition(30f, 140f); // pocisao x/-y

            string dados = "";
            Paragraph paragrafo1 = new Paragraph(dados, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, (int)System.Drawing.FontStyle.Bold));
            paragrafo1.Add("Projeto Academia.\n\n");
            paragrafo1.Alignment = Element.ALIGN_CENTER;

            Paragraph paragrafo2 = new Paragraph(dados, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 9, (int)System.Drawing.FontStyle.Regular));
            paragrafo2.Add("Nome:" + alunoAtual.Nome + "\n");
            paragrafo2.Add("Telefone:" + alunoAtual.Telefone + "\n");
            paragrafo2.Add("Data de Entrada:" + alunoAtual.DataDeEntrada.Date.ToString("dd/MM/yyyy") + "\n");
            paragrafo2.Add("Plano Acaba em:" + alunoAtual.DataDeEntrada.Date.AddMonths(Convert.ToInt32(plano[0])).ToString("dd/MM/yyyy") + "\n");
            paragrafo2.Alignment = Element.ALIGN_LEFT;

            doc.Open();
            doc.Add(imagem);
            doc.Add(paragrafo1);
            doc.Add(paragrafo2);

            if (alunoAtual.Foto != "")
            {
                iTextSharp.text.Image imagemAluno = iTextSharp.text.Image.GetInstance(alunoAtual.Foto);
                imagemAluno.ScaleToFit(55f, 80f);
                //imagemAluno.Alignment = Element.PARAGRAPH;
                imagemAluno.SetAbsolutePosition(210f, 55f); // pocisao x/-y
                doc.Add(imagemAluno);
            }

            doc.Close();

            if (MessageBox.Show("Deseja abrir a carteirinha do aluno?", "Mensagem", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(Globais.caminho + @"\CarteirinhaAluno.pdf");
            }
        }
    }
}