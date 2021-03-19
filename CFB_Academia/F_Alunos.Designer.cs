
namespace CFB_Academia
{
    partial class F_Alunos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_nomeAluno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mtb_telefone = new System.Windows.Forms.MaskedTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_fechar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_gravarAluno = new System.Windows.Forms.Button();
            this.btn_novoAluno = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_selPlano = new System.Windows.Forms.Button();
            this.tb_plano = new System.Windows.Forms.TextBox();
            this.cb_status = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.clb_turmas = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_nomeAluno
            // 
            this.tb_nomeAluno.Enabled = false;
            this.tb_nomeAluno.Location = new System.Drawing.Point(12, 29);
            this.tb_nomeAluno.Name = "tb_nomeAluno";
            this.tb_nomeAluno.Size = new System.Drawing.Size(281, 20);
            this.tb_nomeAluno.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(296, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Telefone";
            // 
            // mtb_telefone
            // 
            this.mtb_telefone.Enabled = false;
            this.mtb_telefone.Location = new System.Drawing.Point(299, 29);
            this.mtb_telefone.Mask = "(00) 00000-0000";
            this.mtb_telefone.Name = "mtb_telefone";
            this.mtb_telefone.Size = new System.Drawing.Size(89, 20);
            this.mtb_telefone.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_fechar);
            this.panel1.Controls.Add(this.btn_cancelar);
            this.panel1.Controls.Add(this.btn_gravarAluno);
            this.panel1.Controls.Add(this.btn_novoAluno);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 209);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 30);
            this.panel1.TabIndex = 12;
            // 
            // btn_fechar
            // 
            this.btn_fechar.Location = new System.Drawing.Point(302, 3);
            this.btn_fechar.Name = "btn_fechar";
            this.btn_fechar.Size = new System.Drawing.Size(86, 23);
            this.btn_fechar.TabIndex = 3;
            this.btn_fechar.Text = "Fechar";
            this.btn_fechar.UseVisualStyleBackColor = true;
            this.btn_fechar.Click += new System.EventHandler(this.btn_fechar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Enabled = false;
            this.btn_cancelar.Location = new System.Drawing.Point(205, 3);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(95, 23);
            this.btn_cancelar.TabIndex = 2;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btn_gravarAluno
            // 
            this.btn_gravarAluno.Enabled = false;
            this.btn_gravarAluno.Location = new System.Drawing.Point(104, 3);
            this.btn_gravarAluno.Name = "btn_gravarAluno";
            this.btn_gravarAluno.Size = new System.Drawing.Size(95, 23);
            this.btn_gravarAluno.TabIndex = 1;
            this.btn_gravarAluno.Text = "Gravar";
            this.btn_gravarAluno.UseVisualStyleBackColor = true;
            this.btn_gravarAluno.Click += new System.EventHandler(this.btn_gravarAluno_Click);
            // 
            // btn_novoAluno
            // 
            this.btn_novoAluno.Location = new System.Drawing.Point(12, 3);
            this.btn_novoAluno.Name = "btn_novoAluno";
            this.btn_novoAluno.Size = new System.Drawing.Size(86, 23);
            this.btn_novoAluno.TabIndex = 0;
            this.btn_novoAluno.Text = "Novo";
            this.btn_novoAluno.UseVisualStyleBackColor = true;
            this.btn_novoAluno.Click += new System.EventHandler(this.btn_novoAluno_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(213, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Plano";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Turmas";
            // 
            // btn_selPlano
            // 
            this.btn_selPlano.Location = new System.Drawing.Point(363, 116);
            this.btn_selPlano.Name = "btn_selPlano";
            this.btn_selPlano.Size = new System.Drawing.Size(25, 23);
            this.btn_selPlano.TabIndex = 25;
            this.btn_selPlano.Text = "...";
            this.btn_selPlano.UseVisualStyleBackColor = true;
            // 
            // tb_plano
            // 
            this.tb_plano.Cursor = System.Windows.Forms.Cursors.No;
            this.tb_plano.Location = new System.Drawing.Point(216, 118);
            this.tb_plano.Name = "tb_plano";
            this.tb_plano.ReadOnly = true;
            this.tb_plano.Size = new System.Drawing.Size(151, 20);
            this.tb_plano.TabIndex = 24;
            // 
            // cb_status
            // 
            this.cb_status.Enabled = false;
            this.cb_status.FormattingEnabled = true;
            this.cb_status.Location = new System.Drawing.Point(216, 74);
            this.cb_status.Name = "cb_status";
            this.cb_status.Size = new System.Drawing.Size(172, 21);
            this.cb_status.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Status";
            // 
            // clb_turmas
            // 
            this.clb_turmas.Enabled = false;
            this.clb_turmas.FormattingEnabled = true;
            this.clb_turmas.Location = new System.Drawing.Point(12, 74);
            this.clb_turmas.Name = "clb_turmas";
            this.clb_turmas.Size = new System.Drawing.Size(198, 124);
            this.clb_turmas.TabIndex = 28;
            this.clb_turmas.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clb_turmas_ItemCheck);
            // 
            // F_Alunos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 239);
            this.Controls.Add(this.clb_turmas);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_selPlano);
            this.Controls.Add(this.tb_plano);
            this.Controls.Add(this.cb_status);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mtb_telefone);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_nomeAluno);
            this.Name = "F_Alunos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Novo Aluno";
            this.Load += new System.EventHandler(this.F_Alunos_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_nomeAluno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mtb_telefone;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_fechar;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Button btn_gravarAluno;
        private System.Windows.Forms.Button btn_novoAluno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_selPlano;
        private System.Windows.Forms.TextBox tb_plano;
        private System.Windows.Forms.ComboBox cb_status;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox clb_turmas;
    }
}