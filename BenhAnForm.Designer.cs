namespace QuanLyNoiTru
{
    partial class BenhAnForm
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
            this.lblNV = new System.Windows.Forms.Label();
            this.cmbNV = new System.Windows.Forms.ComboBox();
            this.lblMa = new System.Windows.Forms.Label();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.lblTrieuChung = new System.Windows.Forms.Label();
            this.txtTrieuChung = new System.Windows.Forms.TextBox();
            this.lblChanDoan = new System.Windows.Forms.Label();
            this.txtChanDoan = new System.Windows.Forms.TextBox();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.txtKetQua = new System.Windows.Forms.TextBox();
            this.lblHuong = new System.Windows.Forms.Label();
            this.txtHuong = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
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
            this.pnlTop.Size = new System.Drawing.Size(1085, 255);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.TabIndex = 1;
            this.pnlTop.Controls.Add(this.lblNV);
            this.pnlTop.Controls.Add(this.cmbNV);
            this.pnlTop.Controls.Add(this.lblMa);
            this.pnlTop.Controls.Add(this.txtMa);
            this.pnlTop.Controls.Add(this.lblTrieuChung);
            this.pnlTop.Controls.Add(this.txtTrieuChung);
            this.pnlTop.Controls.Add(this.lblChanDoan);
            this.pnlTop.Controls.Add(this.txtChanDoan);
            this.pnlTop.Controls.Add(this.lblKetQua);
            this.pnlTop.Controls.Add(this.txtKetQua);
            this.pnlTop.Controls.Add(this.lblHuong);
            this.pnlTop.Controls.Add(this.txtHuong);
            this.pnlTop.Controls.Add(this.btnThem);
            this.pnlTop.Controls.Add(this.btnCapNhat);
            this.pnlTop.Controls.Add(this.btnXoa);
            this.pnlTop.Controls.Add(this.btnLamMoi);
            // 
            // lblNV
            // 
            this.lblNV.AutoSize = false;
            this.lblNV.Location = new System.Drawing.Point(10, 16);
            this.lblNV.Size = new System.Drawing.Size(120, 24);
            this.lblNV.Text = "Hồ sơ nhập viện";
            this.lblNV.Name = "lblNV";
            this.lblNV.TabIndex = 2;
            // 
            // cmbNV
            // 
            this.cmbNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNV.FormattingEnabled = true;
            this.cmbNV.Location = new System.Drawing.Point(135, 10);
            this.cmbNV.Size = new System.Drawing.Size(470, 29);
            this.cmbNV.Name = "cmbNV";
            this.cmbNV.TabIndex = 3;
            // 
            // lblMa
            // 
            this.lblMa.AutoSize = false;
            this.lblMa.Location = new System.Drawing.Point(650, 16);
            this.lblMa.Size = new System.Drawing.Size(120, 24);
            this.lblMa.Text = "Mã BA";
            this.lblMa.Name = "lblMa";
            this.lblMa.TabIndex = 4;
            // 
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(775, 10);
            this.txtMa.ReadOnly = true;
            this.txtMa.Size = new System.Drawing.Size(130, 27);
            this.txtMa.Name = "txtMa";
            this.txtMa.TabIndex = 5;
            // 
            // lblTrieuChung
            // 
            this.lblTrieuChung.AutoSize = false;
            this.lblTrieuChung.Location = new System.Drawing.Point(10, 60);
            this.lblTrieuChung.Size = new System.Drawing.Size(120, 24);
            this.lblTrieuChung.Text = "Triệu chứng";
            this.lblTrieuChung.Name = "lblTrieuChung";
            this.lblTrieuChung.TabIndex = 6;
            // 
            // txtTrieuChung
            // 
            this.txtTrieuChung.Location = new System.Drawing.Point(135, 54);
            this.txtTrieuChung.Multiline = true;
            this.txtTrieuChung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTrieuChung.Size = new System.Drawing.Size(360, 65);
            this.txtTrieuChung.Name = "txtTrieuChung";
            this.txtTrieuChung.TabIndex = 7;
            // 
            // lblChanDoan
            // 
            this.lblChanDoan.AutoSize = false;
            this.lblChanDoan.Location = new System.Drawing.Point(520, 60);
            this.lblChanDoan.Size = new System.Drawing.Size(110, 24);
            this.lblChanDoan.Text = "Chẩn đoán";
            this.lblChanDoan.Name = "lblChanDoan";
            this.lblChanDoan.TabIndex = 8;
            // 
            // txtChanDoan
            // 
            this.txtChanDoan.Location = new System.Drawing.Point(635, 54);
            this.txtChanDoan.Multiline = true;
            this.txtChanDoan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChanDoan.Size = new System.Drawing.Size(370, 65);
            this.txtChanDoan.Name = "txtChanDoan";
            this.txtChanDoan.TabIndex = 9;
            // 
            // lblKetQua
            // 
            this.lblKetQua.AutoSize = false;
            this.lblKetQua.Location = new System.Drawing.Point(10, 135);
            this.lblKetQua.Size = new System.Drawing.Size(120, 24);
            this.lblKetQua.Text = "Kết quả khám";
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.TabIndex = 10;
            // 
            // txtKetQua
            // 
            this.txtKetQua.Location = new System.Drawing.Point(135, 129);
            this.txtKetQua.Multiline = true;
            this.txtKetQua.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtKetQua.Size = new System.Drawing.Size(360, 65);
            this.txtKetQua.Name = "txtKetQua";
            this.txtKetQua.TabIndex = 11;
            // 
            // lblHuong
            // 
            this.lblHuong.AutoSize = false;
            this.lblHuong.Location = new System.Drawing.Point(520, 135);
            this.lblHuong.Size = new System.Drawing.Size(110, 24);
            this.lblHuong.Text = "Hướng điều trị";
            this.lblHuong.Name = "lblHuong";
            this.lblHuong.TabIndex = 12;
            // 
            // txtHuong
            // 
            this.txtHuong.Location = new System.Drawing.Point(635, 129);
            this.txtHuong.Multiline = true;
            this.txtHuong.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHuong.Size = new System.Drawing.Size(370, 65);
            this.txtHuong.Name = "txtHuong";
            this.txtHuong.TabIndex = 13;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(10, 208);
            this.btnThem.Size = new System.Drawing.Size(130, 32);
            this.btnThem.Text = "Thêm lần khám";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            this.btnThem.Name = "btnThem";
            this.btnThem.TabIndex = 14;
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(150, 208);
            this.btnCapNhat.Size = new System.Drawing.Size(100, 32);
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.TabIndex = 15;
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(260, 208);
            this.btnXoa.Size = new System.Drawing.Size(80, 32);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.TabIndex = 16;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(350, 208);
            this.btnLamMoi.Size = new System.Drawing.Size(90, 32);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.TabIndex = 17;
            // 
            // BenhAnForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1085, 650);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Text = "Hồ sơ bệnh án";
            this.Name = "BenhAnForm";
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
        private System.Windows.Forms.Label lblNV;
        private System.Windows.Forms.ComboBox cmbNV;
        private System.Windows.Forms.Label lblMa;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.Label lblTrieuChung;
        private System.Windows.Forms.TextBox txtTrieuChung;
        private System.Windows.Forms.Label lblChanDoan;
        private System.Windows.Forms.TextBox txtChanDoan;
        private System.Windows.Forms.Label lblKetQua;
        private System.Windows.Forms.TextBox txtKetQua;
        private System.Windows.Forms.Label lblHuong;
        private System.Windows.Forms.TextBox txtHuong;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
    }
}
