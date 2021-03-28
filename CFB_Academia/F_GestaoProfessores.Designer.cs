
namespace CFB_Academia
{
    partial class F_GestaoProfessores
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mtb_telefone = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgv_professores = new System.Windows.Forms.DataGridView();
            this.tb_idProfessor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_fechar = new System.Windows.Forms.Button();
            this.btn_excluirProfessor = new System.Windows.Forms.Button();
            this.btn_salvarProfessor = new System.Windows.Forms.Button();
            this.btn_novoProfessor = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_nome = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_professores)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mtb_telefone
            // 
            this.mtb_telefone.Location = new System.Drawing.Point(344, 24);
            this.mtb_telefone.Mask = "(00)00000-0000";
            this.mtb_telefone.Name = "mtb_telefone";
            this.mtb_telefone.Size = new System.Drawing.Size(90, 20);
            this.mtb_telefone.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Telefone";
            // 
            // dgv_professores
            // 
            this.dgv_professores.AllowUserToAddRows = false;
            this.dgv_professores.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_professores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_professores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_professores.EnableHeadersVisualStyles = false;
            this.dgv_professores.Location = new System.Drawing.Point(12, 50);
            this.dgv_professores.MultiSelect = false;
            this.dgv_professores.Name = "dgv_professores";
            this.dgv_professores.ReadOnly = true;
            this.dgv_professores.RowHeadersVisible = false;
            this.dgv_professores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_professores.Size = new System.Drawing.Size(422, 359);
            this.dgv_professores.TabIndex = 9;
            this.dgv_professores.SelectionChanged += new System.EventHandler(this.dgv_professores_SelectionChanged);
            // 
            // tb_idProfessor
            // 
            this.tb_idProfessor.Location = new System.Drawing.Point(13, 24);
            this.tb_idProfessor.Name = "tb_idProfessor";
            this.tb_idProfessor.ReadOnly = true;
            this.tb_idProfessor.Size = new System.Drawing.Size(87, 20);
            this.tb_idProfessor.TabIndex = 7;
            this.tb_idProfessor.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "ID";
            // 
            // btn_fechar
            // 
            this.btn_fechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_fechar.Location = new System.Drawing.Point(333, 3);
            this.btn_fechar.Name = "btn_fechar";
            this.btn_fechar.Size = new System.Drawing.Size(101, 23);
            this.btn_fechar.TabIndex = 3;
            this.btn_fechar.Text = "Fechar";
            this.btn_fechar.UseVisualStyleBackColor = true;
            this.btn_fechar.Click += new System.EventHandler(this.btn_fechar_Click);
            // 
            // btn_excluirProfessor
            // 
            this.btn_excluirProfessor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_excluirProfessor.Location = new System.Drawing.Point(226, 3);
            this.btn_excluirProfessor.Name = "btn_excluirProfessor";
            this.btn_excluirProfessor.Size = new System.Drawing.Size(101, 23);
            this.btn_excluirProfessor.TabIndex = 2;
            this.btn_excluirProfessor.Text = "Excluir Professor";
            this.btn_excluirProfessor.UseVisualStyleBackColor = true;
            this.btn_excluirProfessor.Click += new System.EventHandler(this.btn_excluirProfessor_Click);
            // 
            // btn_salvarProfessor
            // 
            this.btn_salvarProfessor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_salvarProfessor.Location = new System.Drawing.Point(119, 3);
            this.btn_salvarProfessor.Name = "btn_salvarProfessor";
            this.btn_salvarProfessor.Size = new System.Drawing.Size(101, 23);
            this.btn_salvarProfessor.TabIndex = 1;
            this.btn_salvarProfessor.Text = "Salvar Professor";
            this.btn_salvarProfessor.UseVisualStyleBackColor = true;
            this.btn_salvarProfessor.Click += new System.EventHandler(this.btn_salvarProfessor_Click);
            // 
            // btn_novoProfessor
            // 
            this.btn_novoProfessor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_novoProfessor.Location = new System.Drawing.Point(12, 3);
            this.btn_novoProfessor.Name = "btn_novoProfessor";
            this.btn_novoProfessor.Size = new System.Drawing.Size(101, 23);
            this.btn_novoProfessor.TabIndex = 0;
            this.btn_novoProfessor.Text = "Novo Professor";
            this.btn_novoProfessor.UseVisualStyleBackColor = true;
            this.btn_novoProfessor.Click += new System.EventHandler(this.btn_novoProfessor_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_fechar);
            this.panel1.Controls.Add(this.btn_excluirProfessor);
            this.panel1.Controls.Add(this.btn_salvarProfessor);
            this.panel1.Controls.Add(this.btn_novoProfessor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 421);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 29);
            this.panel1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Professor";
            // 
            // tb_nome
            // 
            this.tb_nome.Location = new System.Drawing.Point(106, 24);
            this.tb_nome.Name = "tb_nome";
            this.tb_nome.Size = new System.Drawing.Size(232, 20);
            this.tb_nome.TabIndex = 13;
            // 
            // F_GestaoProfessores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 450);
            this.Controls.Add(this.tb_nome);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mtb_telefone);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgv_professores);
            this.Controls.Add(this.tb_idProfessor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_GestaoProfessores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestão de Professores";
            this.Load += new System.EventHandler(this.F_GestaoProfessores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_professores)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mtb_telefone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgv_professores;
        private System.Windows.Forms.TextBox tb_idProfessor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_fechar;
        private System.Windows.Forms.Button btn_excluirProfessor;
        private System.Windows.Forms.Button btn_salvarProfessor;
        private System.Windows.Forms.Button btn_novoProfessor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_nome;
    }
}