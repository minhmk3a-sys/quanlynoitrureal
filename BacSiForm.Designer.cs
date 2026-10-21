namespace QuanLyNoiTru
{
    partial class BacSiForm
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
            this.lblMa = new System.Windows.Forms.Label();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblNs = new System.Windows.Forms.Label();
            this.dtpNs = new System.Windows.Forms.DateTimePicker();
            this.lblGt = new System.Windows.Forms.Label();
            this.cmbGt = new System.Windows.Forms.ComboBox();
            this.lblChuyenKhoa = new System.Windows.Forms.Label();
            this.txtChuyenKhoa = new System.Windows.Forms.TextBox();
            this.lblSdt = new System.Windows.Forms.Label();
            this.txtSdt = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblKhoa = new System.Windows.Forms.Label();
            this.cmbKhoa = new System.Windows.Forms.ComboBox();
            this.chkLam = new System.Windows.Forms.CheckBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTim = new System.Windows.Forms.Label();
            this.txtTim = new System.Windows.Forms.TextBox();
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
            this.pnlTop.Size = new System.Drawing.Size(1085, 215);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.TabIndex = 1;
            this.pnlTop.Controls.Add(this.lblMa);
            this.pnlTop.Controls.Add(this.txtMa);
            this.pnlTop.Controls.Add(this.lblTen);
            this.pnlTop.Controls.Add(this.txtTen);
            this.pnlTop.Controls.Add(this.lblNs);
            this.pnlTop.Controls.Add(this.dtpNs);
            this.pnlTop.Controls.Add(this.lblGt);
            this.pnlTop.Controls.Add(this.cmbGt);
            this.pnlTop.Controls.Add(this.lblChuyenKhoa);
            this.pnlTop.Controls.Add(this.txtChuyenKhoa);
            this.pnlTop.Controls.Add(this.lblSdt);
            this.pnlTop.Controls.Add(this.txtSdt);
            this.pnlTop.Controls.Add(this.lblEmail);
            this.pnlTop.Controls.Add(this.txtEmail);
            this.pnlTop.Controls.Add(this.lblKhoa);
            this.pnlTop.Controls.Add(this.cmbKhoa);
            this.pnlTop.Controls.Add(this.chkLam);
            this.pnlTop.Controls.Add(this.btnThem);
            this.pnlTop.Controls.Add(this.btnSua);
            this.pnlTop.Controls.Add(this.btnXoa);
            this.pnlTop.Controls.Add(this.btnLamMoi);
            this.pnlTop.Controls.Add(this.lblTim);
            this.pnlTop.Controls.Add(this.txtTim);
            // 
            // lblMa
            // 
            this.lblMa.AutoSize = false;
            this.lblMa.Location = new System.Drawing.Point(10, 16);
            this.lblMa.Size = new System.Drawing.Size(120, 24);
            this.lblMa.Text = "Mã BS";
            this.lblMa.Name = "lblMa";
            this.lblMa.TabIndex = 2;
            // 
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(135, 10);
            this.txtMa.ReadOnly = true;
            this.txtMa.Size = new System.Drawing.Size(170, 27);
            this.txtMa.Name = "txtMa";
            this.txtMa.TabIndex = 3;
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = false;
            this.lblTen.Location = new System.Drawing.Point(10, 51);
            this.lblTen.Size = new System.Drawing.Size(120, 24);
            this.lblTen.Text = "Họ tên";
            this.lblTen.Name = "lblTen";
            this.lblTen.TabIndex = 4;
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(135, 45);
            this.txtTen.Size = new System.Drawing.Size(170, 27);
            this.txtTen.Name = "txtTen";
            this.txtTen.TabIndex = 5;
            // 
            // lblNs
            // 
            this.lblNs.AutoSize = false;
            this.lblNs.Location = new System.Drawing.Point(10, 86);
            this.lblNs.Size = new System.Drawing.Size(120, 24);
            this.lblNs.Text = "Ngày sinh";
            this.lblNs.Name = "lblNs";
            this.lblNs.TabIndex = 6;
            // 
            // dtpNs
            // 
            this.dtpNs.CustomFormat = "dd/MM/yyyy";
            this.dtpNs.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNs.Location = new System.Drawing.Point(135, 80);
            this.dtpNs.Size = new System.Drawing.Size(170, 27);
            this.dtpNs.Name = "dtpNs";
            this.dtpNs.TabIndex = 7;
            // 
            // lblGt
            // 
            this.lblGt.AutoSize = false;
            this.lblGt.Location = new System.Drawing.Point(10, 121);
            this.lblGt.Size = new System.Drawing.Size(120, 24);
            this.lblGt.Text = "Giới tính";
            this.lblGt.Name = "lblGt";
            this.lblGt.TabIndex = 8;
            // 
            // cmbGt
            // 
            this.cmbGt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGt.FormattingEnabled = true;
            this.cmbGt.Items.AddRange(new object[] { "Nam", "Nữ" });
            this.cmbGt.Location = new System.Drawing.Point(135, 115);
            this.cmbGt.Size = new System.Drawing.Size(170, 29);
            this.cmbGt.Name = "cmbGt";
            this.cmbGt.TabIndex = 9;
            // 
            // lblChuyenKhoa
            // 
            this.lblChuyenKhoa.AutoSize = false;
            this.lblChuyenKhoa.Location = new System.Drawing.Point(330, 16);
            this.lblChuyenKhoa.Size = new System.Drawing.Size(120, 24);
            this.lblChuyenKhoa.Text = "Chuyên khoa";
            this.lblChuyenKhoa.Name = "lblChuyenKhoa";
            this.lblChuyenKhoa.TabIndex = 10;
            // 
            // txtChuyenKhoa
            // 
            this.txtChuyenKhoa.Location = new System.Drawing.Point(455, 10);
            this.txtChuyenKhoa.Size = new System.Drawing.Size(170, 27);
            this.txtChuyenKhoa.Name = "txtChuyenKhoa";
            this.txtChuyenKhoa.TabIndex = 11;
            // 
            // lblSdt
            // 
            this.lblSdt.AutoSize = false;
            this.lblSdt.Location = new System.Drawing.Point(330, 51);
            this.lblSdt.Size = new System.Drawing.Size(120, 24);
            this.lblSdt.Text = "SĐT";
            this.lblSdt.Name = "lblSdt";
            this.lblSdt.TabIndex = 12;
            // 
            // txtSdt
            // 
            this.txtSdt.Location = new System.Drawing.Point(455, 45);
            this.txtSdt.Size = new System.Drawing.Size(170, 27);
            this.txtSdt.Name = "txtSdt";
            this.txtSdt.TabIndex = 13;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = false;
            this.lblEmail.Location = new System.Drawing.Point(330, 86);
            this.lblEmail.Size = new System.Drawing.Size(120, 24);
            this.lblEmail.Text = "Email";
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.TabIndex = 14;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(455, 80);
            this.txtEmail.Size = new System.Drawing.Size(170, 27);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.TabIndex = 15;
            // 
            // lblKhoa
            // 
            this.lblKhoa.AutoSize = false;
            this.lblKhoa.Location = new System.Drawing.Point(330, 121);
            this.lblKhoa.Size = new System.Drawing.Size(120, 24);
            this.lblKhoa.Text = "Khoa";
            this.lblKhoa.Name = "lblKhoa";
            this.lblKhoa.TabIndex = 16;
            // 
            // cmbKhoa
            // 
            this.cmbKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKhoa.FormattingEnabled = true;
            this.cmbKhoa.Location = new System.Drawing.Point(455, 115);
            this.cmbKhoa.Size = new System.Drawing.Size(170, 29);
            this.cmbKhoa.Name = "cmbKhoa";
            this.cmbKhoa.TabIndex = 17;
            // 
            // chkLam
            // 
            this.chkLam.AutoSize = true;
            this.chkLam.Checked = true;
            this.chkLam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLam.Location = new System.Drawing.Point(650, 12);
            this.chkLam.Text = "Đang làm việc";
            this.chkLam.UseVisualStyleBackColor = true;
            this.chkLam.Name = "chkLam";
            this.chkLam.TabIndex = 18;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(10, 165);
            this.btnThem.Size = new System.Drawing.Size(90, 32);
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            this.btnThem.Name = "btnThem";
            this.btnThem.TabIndex = 19;
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(110, 165);
            this.btnSua.Size = new System.Drawing.Size(90, 32);
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            this.btnSua.Name = "btnSua";
            this.btnSua.TabIndex = 20;
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(210, 165);
            this.btnXoa.Size = new System.Drawing.Size(90, 32);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.TabIndex = 21;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(310, 165);
            this.btnLamMoi.Size = new System.Drawing.Size(90, 32);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.TabIndex = 22;
            // 
            // lblTim
            // 
            this.lblTim.AutoSize = false;
            this.lblTim.Location = new System.Drawing.Point(430, 171);
            this.lblTim.Size = new System.Drawing.Size(80, 24);
            this.lblTim.Text = "Tìm kiếm";
            this.lblTim.Name = "lblTim";
            this.lblTim.TabIndex = 23;
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(515, 165);
            this.txtTim.Size = new System.Drawing.Size(250, 27);
            this.txtTim.Name = "txtTim";
            this.txtTim.TabIndex = 24;
            // 
            // BacSiForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1085, 650);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Text = "Quản lý bác sĩ";
            this.Name = "BacSiForm";
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
        private System.Windows.Forms.Label lblMa;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblNs;
        private System.Windows.Forms.DateTimePicker dtpNs;
        private System.Windows.Forms.Label lblGt;
        private System.Windows.Forms.ComboBox cmbGt;
        private System.Windows.Forms.Label lblChuyenKhoa;
        private System.Windows.Forms.TextBox txtChuyenKhoa;
        private System.Windows.Forms.Label lblSdt;
        private System.Windows.Forms.TextBox txtSdt;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblKhoa;
        private System.Windows.Forms.ComboBox cmbKhoa;
        private System.Windows.Forms.CheckBox chkLam;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.TextBox txtTim;
    }
}
