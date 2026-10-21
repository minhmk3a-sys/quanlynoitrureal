namespace QuanLyNoiTru
{
    partial class BaoCaoForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>Code do Visual Studio quản lý. Sửa giao diện bằng cách mở form trong Designer (kéo thả).</summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(242, 247, 252);
            this.dgv = new System.Windows.Forms.DataGridView();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblBaoCao = new System.Windows.Forms.Label();
            this.cmbBC = new System.Windows.Forms.ComboBox();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnPdf = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(1085, 400);
            this.dgv.Name = "dgv";
            this.dgv.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Size = new System.Drawing.Size(1085, 56);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.TabIndex = 1;
            this.pnlTop.Controls.Add(this.lblBaoCao);
            this.pnlTop.Controls.Add(this.cmbBC);
            this.pnlTop.Controls.Add(this.btnXem);
            this.pnlTop.Controls.Add(this.btnIn);
            this.pnlTop.Controls.Add(this.btnPdf);
            this.pnlTop.Controls.Add(this.btnExcel);
            // 
            // lblBaoCao
            // 
            this.lblBaoCao.AutoSize = false;
            this.lblBaoCao.Location = new System.Drawing.Point(10, 16);
            this.lblBaoCao.Size = new System.Drawing.Size(70, 24);
            this.lblBaoCao.Text = "Báo cáo";
            this.lblBaoCao.Name = "lblBaoCao";
            this.lblBaoCao.TabIndex = 2;
            // 
            // cmbBC
            // 
            this.cmbBC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBC.FormattingEnabled = true;
            this.cmbBC.Location = new System.Drawing.Point(85, 12);
            this.cmbBC.Size = new System.Drawing.Size(390, 29);
            this.cmbBC.Name = "cmbBC";
            this.cmbBC.TabIndex = 3;
            // 
            // btnXem
            // 
            this.btnXem.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnXem.FlatAppearance.BorderSize = 0;
            this.btnXem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXem.ForeColor = System.Drawing.Color.White;
            this.btnXem.Location = new System.Drawing.Point(490, 10);
            this.btnXem.Size = new System.Drawing.Size(70, 32);
            this.btnXem.Text = "Xem";
            this.btnXem.UseVisualStyleBackColor = false;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            this.btnXem.Name = "btnXem";
            this.btnXem.TabIndex = 4;
            // 
            // btnIn
            // 
            this.btnIn.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnIn.FlatAppearance.BorderSize = 0;
            this.btnIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIn.ForeColor = System.Drawing.Color.White;
            this.btnIn.Location = new System.Drawing.Point(570, 10);
            this.btnIn.Size = new System.Drawing.Size(115, 32);
            this.btnIn.Text = "In danh sách";
            this.btnIn.UseVisualStyleBackColor = false;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            this.btnIn.Name = "btnIn";
            this.btnIn.TabIndex = 5;
            // 
            // btnPdf
            // 
            this.btnPdf.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnPdf.FlatAppearance.BorderSize = 0;
            this.btnPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPdf.ForeColor = System.Drawing.Color.White;
            this.btnPdf.Location = new System.Drawing.Point(695, 10);
            this.btnPdf.Size = new System.Drawing.Size(100, 32);
            this.btnPdf.Text = "Xuất PDF";
            this.btnPdf.UseVisualStyleBackColor = false;
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.TabIndex = 6;
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnExcel.FlatAppearance.BorderSize = 0;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(805, 10);
            this.btnExcel.Size = new System.Drawing.Size(110, 32);
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.TabIndex = 7;
            // 
            // BaoCaoForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1085, 650);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Text = "Báo cáo";
            this.Name = "BaoCaoForm";
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.pnlTop);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblBaoCao;
        private System.Windows.Forms.ComboBox cmbBC;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnPdf;
        private System.Windows.Forms.Button btnExcel;
    }
}
