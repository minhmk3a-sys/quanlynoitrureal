namespace QuanLyNoiTru
{
    partial class BenhNhanForm
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
            this.lblCccd = new System.Windows.Forms.Label();
            this.txtCccd = new System.Windows.Forms.TextBox();
            this.lblSdt = new System.Windows.Forms.Label();
            this.txtSdt = new System.Windows.Forms.TextBox();
            this.lblNghe = new System.Windows.Forms.Label();
            this.txtNghe = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblNguoiThan = new System.Windows.Forms.Label();
            this.txtNguoiThan = new System.Windows.Forms.TextBox();
            this.lblSdtNT = new System.Windows.Forms.Label();
            this.txtSdtNT = new System.Windows.Forms.TextBox();
            this.lblTinhTrang = new System.Windows.Forms.Label();
            this.txtTinhTrang = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTieuDeTim = new System.Windows.Forms.Label();
            this.lblTimMa = new System.Windows.Forms.Label();
            this.txtTimMa = new System.Windows.Forms.TextBox();
            this.lblTimTen = new System.Windows.Forms.Label();
            this.txtTimTen = new System.Windows.Forms.TextBox();
            this.lblTimGt = new System.Windows.Forms.Label();
            this.cmbTimGt = new System.Windows.Forms.ComboBox();
            this.lblTimSdt = new System.Windows.Forms.Label();
            this.txtTimSdt = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnLamMoiTim = new System.Windows.Forms.Button();
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
            this.pnlTop.Size = new System.Drawing.Size(1085, 285);
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
            this.pnlTop.Controls.Add(this.lblCccd);
            this.pnlTop.Controls.Add(this.txtCccd);
            this.pnlTop.Controls.Add(this.lblSdt);
            this.pnlTop.Controls.Add(this.txtSdt);
            this.pnlTop.Controls.Add(this.lblNghe);
            this.pnlTop.Controls.Add(this.txtNghe);
            this.pnlTop.Controls.Add(this.lblDiaChi);
            this.pnlTop.Controls.Add(this.txtDiaChi);
            this.pnlTop.Controls.Add(this.lblNguoiThan);
            this.pnlTop.Controls.Add(this.txtNguoiThan);
            this.pnlTop.Controls.Add(this.lblSdtNT);
            this.pnlTop.Controls.Add(this.txtSdtNT);
            this.pnlTop.Controls.Add(this.lblTinhTrang);
            this.pnlTop.Controls.Add(this.txtTinhTrang);
            this.pnlTop.Controls.Add(this.btnThem);
            this.pnlTop.Controls.Add(this.btnSua);
            this.pnlTop.Controls.Add(this.btnXoa);
            this.pnlTop.Controls.Add(this.btnLamMoi);
            this.pnlTop.Controls.Add(this.lblTieuDeTim);
            this.pnlTop.Controls.Add(this.lblTimMa);
            this.pnlTop.Controls.Add(this.txtTimMa);
            this.pnlTop.Controls.Add(this.lblTimTen);
            this.pnlTop.Controls.Add(this.txtTimTen);
            this.pnlTop.Controls.Add(this.lblTimGt);
            this.pnlTop.Controls.Add(this.cmbTimGt);
            this.pnlTop.Controls.Add(this.lblTimSdt);
            this.pnlTop.Controls.Add(this.txtTimSdt);
            this.pnlTop.Controls.Add(this.btnTimKiem);
            this.pnlTop.Controls.Add(this.btnLamMoiTim);
            // 
            // lblMa
            // 
            this.lblMa.AutoSize = false;
            this.lblMa.Location = new System.Drawing.Point(10, 16);
            this.lblMa.Size = new System.Drawing.Size(120, 24);
            this.lblMa.Text = "Mã BN";
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
            // lblCccd
            // 
            this.lblCccd.AutoSize = false;
            this.lblCccd.Location = new System.Drawing.Point(330, 16);
            this.lblCccd.Size = new System.Drawing.Size(120, 24);
            this.lblCccd.Text = "CCCD";
            this.lblCccd.Name = "lblCccd";
            this.lblCccd.TabIndex = 10;
            // 
            // txtCccd
            // 
            this.txtCccd.Location = new System.Drawing.Point(455, 10);
            this.txtCccd.Size = new System.Drawing.Size(170, 27);
            this.txtCccd.Name = "txtCccd";
            this.txtCccd.TabIndex = 11;
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
            // lblNghe
            // 
            this.lblNghe.AutoSize = false;
            this.lblNghe.Location = new System.Drawing.Point(330, 86);
            this.lblNghe.Size = new System.Drawing.Size(120, 24);
            this.lblNghe.Text = "Nghề nghiệp";
            this.lblNghe.Name = "lblNghe";
            this.lblNghe.TabIndex = 14;
            // 
            // txtNghe
            // 
            this.txtNghe.Location = new System.Drawing.Point(455, 80);
            this.txtNghe.Size = new System.Drawing.Size(170, 27);
            this.txtNghe.Name = "txtNghe";
            this.txtNghe.TabIndex = 15;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = false;
            this.lblDiaChi.Location = new System.Drawing.Point(330, 121);
            this.lblDiaChi.Size = new System.Drawing.Size(120, 24);
            this.lblDiaChi.Text = "Địa chỉ";
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.TabIndex = 16;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(455, 115);
            this.txtDiaChi.Size = new System.Drawing.Size(170, 27);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.TabIndex = 17;
            // 
            // lblNguoiThan
            // 
            this.lblNguoiThan.AutoSize = false;
            this.lblNguoiThan.Location = new System.Drawing.Point(650, 16);
            this.lblNguoiThan.Size = new System.Drawing.Size(120, 24);
            this.lblNguoiThan.Text = "Người thân";
            this.lblNguoiThan.Name = "lblNguoiThan";
            this.lblNguoiThan.TabIndex = 18;
            // 
            // txtNguoiThan
            // 
            this.txtNguoiThan.Location = new System.Drawing.Point(775, 10);
            this.txtNguoiThan.Size = new System.Drawing.Size(230, 27);
            this.txtNguoiThan.Name = "txtNguoiThan";
            this.txtNguoiThan.TabIndex = 19;
            // 
            // lblSdtNT
            // 
            this.lblSdtNT.AutoSize = false;
            this.lblSdtNT.Location = new System.Drawing.Point(650, 51);
            this.lblSdtNT.Size = new System.Drawing.Size(120, 24);
            this.lblSdtNT.Text = "SĐT người thân";
            this.lblSdtNT.Name = "lblSdtNT";
            this.lblSdtNT.TabIndex = 20;
            // 
            // txtSdtNT
            // 
            this.txtSdtNT.Location = new System.Drawing.Point(775, 45);
            this.txtSdtNT.Size = new System.Drawing.Size(230, 27);
            this.txtSdtNT.Name = "txtSdtNT";
            this.txtSdtNT.TabIndex = 21;
            // 
            // lblTinhTrang
            // 
            this.lblTinhTrang.AutoSize = false;
            this.lblTinhTrang.Location = new System.Drawing.Point(650, 86);
            this.lblTinhTrang.Size = new System.Drawing.Size(120, 24);
            this.lblTinhTrang.Text = "Tình trạng bệnh";
            this.lblTinhTrang.Name = "lblTinhTrang";
            this.lblTinhTrang.TabIndex = 22;
            // 
            // txtTinhTrang
            // 
            this.txtTinhTrang.Location = new System.Drawing.Point(775, 80);
            this.txtTinhTrang.Size = new System.Drawing.Size(230, 27);
            this.txtTinhTrang.Name = "txtTinhTrang";
            this.txtTinhTrang.TabIndex = 23;
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
            this.btnThem.TabIndex = 24;
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
            this.btnSua.TabIndex = 25;
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
            this.btnXoa.TabIndex = 26;
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
            this.btnLamMoi.TabIndex = 27;
            // 
            // lblTieuDeTim
            // 
            this.lblTieuDeTim.AutoSize = false;
            this.lblTieuDeTim.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeTim.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.lblTieuDeTim.Location = new System.Drawing.Point(10, 208);
            this.lblTieuDeTim.Size = new System.Drawing.Size(260, 24);
            this.lblTieuDeTim.Text = "🔍 TÌM KIẾM BỆNH NHÂN";
            this.lblTieuDeTim.Name = "lblTieuDeTim";
            this.lblTieuDeTim.TabIndex = 28;
            // 
            // lblTimMa
            // 
            this.lblTimMa.AutoSize = false;
            this.lblTimMa.Location = new System.Drawing.Point(10, 243);
            this.lblTimMa.Size = new System.Drawing.Size(55, 24);
            this.lblTimMa.Text = "Mã BN";
            this.lblTimMa.Name = "lblTimMa";
            this.lblTimMa.TabIndex = 29;
            // 
            // txtTimMa
            // 
            this.txtTimMa.Location = new System.Drawing.Point(70, 238);
            this.txtTimMa.Size = new System.Drawing.Size(90, 27);
            this.txtTimMa.Name = "txtTimMa";
            this.txtTimMa.TabIndex = 30;
            // 
            // lblTimTen
            // 
            this.lblTimTen.AutoSize = false;
            this.lblTimTen.Location = new System.Drawing.Point(170, 243);
            this.lblTimTen.Size = new System.Drawing.Size(70, 24);
            this.lblTimTen.Text = "Họ và tên";
            this.lblTimTen.Name = "lblTimTen";
            this.lblTimTen.TabIndex = 31;
            // 
            // txtTimTen
            // 
            this.txtTimTen.Location = new System.Drawing.Point(245, 238);
            this.txtTimTen.Size = new System.Drawing.Size(140, 27);
            this.txtTimTen.Name = "txtTimTen";
            this.txtTimTen.TabIndex = 32;
            // 
            // lblTimGt
            // 
            this.lblTimGt.AutoSize = false;
            this.lblTimGt.Location = new System.Drawing.Point(395, 243);
            this.lblTimGt.Size = new System.Drawing.Size(65, 24);
            this.lblTimGt.Text = "Giới tính";
            this.lblTimGt.Name = "lblTimGt";
            this.lblTimGt.TabIndex = 33;
            // 
            // cmbTimGt
            // 
            this.cmbTimGt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimGt.FormattingEnabled = true;
            this.cmbTimGt.Items.AddRange(new object[] { "-- Tất cả --", "Nam", "Nữ" });
            this.cmbTimGt.Location = new System.Drawing.Point(465, 237);
            this.cmbTimGt.Size = new System.Drawing.Size(105, 29);
            this.cmbTimGt.Name = "cmbTimGt";
            this.cmbTimGt.TabIndex = 34;
            // 
            // lblTimSdt
            // 
            this.lblTimSdt.AutoSize = false;
            this.lblTimSdt.Location = new System.Drawing.Point(580, 243);
            this.lblTimSdt.Size = new System.Drawing.Size(85, 24);
            this.lblTimSdt.Text = "Số điện thoại";
            this.lblTimSdt.Name = "lblTimSdt";
            this.lblTimSdt.TabIndex = 35;
            // 
            // txtTimSdt
            // 
            this.txtTimSdt.Location = new System.Drawing.Point(670, 238);
            this.txtTimSdt.Size = new System.Drawing.Size(125, 27);
            this.txtTimSdt.Name = "txtTimSdt";
            this.txtTimSdt.TabIndex = 36;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(805, 236);
            this.btnTimKiem.Size = new System.Drawing.Size(115, 31);
            this.btnTimKiem.Text = "🔍 Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.TabIndex = 37;
            // 
            // btnLamMoiTim
            // 
            this.btnLamMoiTim.BackColor = System.Drawing.Color.FromArgb(238, 242, 248);
            this.btnLamMoiTim.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 200, 225);
            this.btnLamMoiTim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiTim.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnLamMoiTim.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.btnLamMoiTim.Location = new System.Drawing.Point(930, 236);
            this.btnLamMoiTim.Size = new System.Drawing.Size(105, 31);
            this.btnLamMoiTim.Text = "🔄 Làm mới";
            this.btnLamMoiTim.UseVisualStyleBackColor = false;
            this.btnLamMoiTim.Click += new System.EventHandler(this.btnLamMoiTim_Click);
            this.btnLamMoiTim.Name = "btnLamMoiTim";
            this.btnLamMoiTim.TabIndex = 38;
            // 
            // BenhNhanForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1085, 650);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Text = "Quản lý bệnh nhân";
            this.Name = "BenhNhanForm";
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
        private System.Windows.Forms.Label lblCccd;
        private System.Windows.Forms.TextBox txtCccd;
        private System.Windows.Forms.Label lblSdt;
        private System.Windows.Forms.TextBox txtSdt;
        private System.Windows.Forms.Label lblNghe;
        private System.Windows.Forms.TextBox txtNghe;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblNguoiThan;
        private System.Windows.Forms.TextBox txtNguoiThan;
        private System.Windows.Forms.Label lblSdtNT;
        private System.Windows.Forms.TextBox txtSdtNT;
        private System.Windows.Forms.Label lblTinhTrang;
        private System.Windows.Forms.TextBox txtTinhTrang;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTieuDeTim;
        private System.Windows.Forms.Label lblTimMa;
        private System.Windows.Forms.TextBox txtTimMa;
        private System.Windows.Forms.Label lblTimTen;
        private System.Windows.Forms.TextBox txtTimTen;
        private System.Windows.Forms.Label lblTimGt;
        private System.Windows.Forms.ComboBox cmbTimGt;
        private System.Windows.Forms.Label lblTimSdt;
        private System.Windows.Forms.TextBox txtTimSdt;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoiTim;
    }
}
