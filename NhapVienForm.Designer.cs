namespace QuanLyNoiTru
{
    partial class NhapVienForm
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
            this.lblKhoa = new System.Windows.Forms.Label();
            this.cmbKhoa = new System.Windows.Forms.ComboBox();
            this.lblGiuong = new System.Windows.Forms.Label();
            this.cmbGiuong = new System.Windows.Forms.ComboBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            this.lblBN = new System.Windows.Forms.Label();
            this.cmbBN = new System.Windows.Forms.ComboBox();
            this.lblPhong = new System.Windows.Forms.Label();
            this.cmbPhong = new System.Windows.Forms.ComboBox();
            this.lblBS = new System.Windows.Forms.Label();
            this.cmbBS = new System.Windows.Forms.ComboBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblVao = new System.Windows.Forms.Label();
            this.dtpVao = new System.Windows.Forms.DateTimePicker();
            this.lblRa = new System.Windows.Forms.Label();
            this.dtpRa = new System.Windows.Forms.DateTimePicker();
            this.lblTrangThaiTen = new System.Windows.Forms.Label();
            this.lblTT = new System.Windows.Forms.Label();
            this.btnTaoHoSo = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnRaVien = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTieuDeTim = new System.Windows.Forms.Label();
            this.lblTimMa = new System.Windows.Forms.Label();
            this.txtTimMa = new System.Windows.Forms.TextBox();
            this.lblTimBN = new System.Windows.Forms.Label();
            this.txtTimBN = new System.Windows.Forms.TextBox();
            this.lblTimTT = new System.Windows.Forms.Label();
            this.cmbTimTT = new System.Windows.Forms.ComboBox();
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
            this.pnlTop.Size = new System.Drawing.Size(1085, 275);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.TabIndex = 1;
            this.pnlTop.Controls.Add(this.lblMa);
            this.pnlTop.Controls.Add(this.txtMa);
            this.pnlTop.Controls.Add(this.lblKhoa);
            this.pnlTop.Controls.Add(this.cmbKhoa);
            this.pnlTop.Controls.Add(this.lblGiuong);
            this.pnlTop.Controls.Add(this.cmbGiuong);
            this.pnlTop.Controls.Add(this.lblLyDo);
            this.pnlTop.Controls.Add(this.txtLyDo);
            this.pnlTop.Controls.Add(this.lblBN);
            this.pnlTop.Controls.Add(this.cmbBN);
            this.pnlTop.Controls.Add(this.lblPhong);
            this.pnlTop.Controls.Add(this.cmbPhong);
            this.pnlTop.Controls.Add(this.lblBS);
            this.pnlTop.Controls.Add(this.cmbBS);
            this.pnlTop.Controls.Add(this.lblGhiChu);
            this.pnlTop.Controls.Add(this.txtGhiChu);
            this.pnlTop.Controls.Add(this.lblVao);
            this.pnlTop.Controls.Add(this.dtpVao);
            this.pnlTop.Controls.Add(this.lblRa);
            this.pnlTop.Controls.Add(this.dtpRa);
            this.pnlTop.Controls.Add(this.lblTrangThaiTen);
            this.pnlTop.Controls.Add(this.lblTT);
            this.pnlTop.Controls.Add(this.btnTaoHoSo);
            this.pnlTop.Controls.Add(this.btnCapNhat);
            this.pnlTop.Controls.Add(this.btnRaVien);
            this.pnlTop.Controls.Add(this.btnHuy);
            this.pnlTop.Controls.Add(this.btnLamMoi);
            this.pnlTop.Controls.Add(this.lblTieuDeTim);
            this.pnlTop.Controls.Add(this.lblTimMa);
            this.pnlTop.Controls.Add(this.txtTimMa);
            this.pnlTop.Controls.Add(this.lblTimBN);
            this.pnlTop.Controls.Add(this.txtTimBN);
            this.pnlTop.Controls.Add(this.lblTimTT);
            this.pnlTop.Controls.Add(this.cmbTimTT);
            this.pnlTop.Controls.Add(this.btnTimKiem);
            this.pnlTop.Controls.Add(this.btnLamMoiTim);
            // 
            // lblMa
            // 
            this.lblMa.AutoSize = false;
            this.lblMa.Location = new System.Drawing.Point(10, 16);
            this.lblMa.Size = new System.Drawing.Size(120, 24);
            this.lblMa.Text = "Mã nhập viện";
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
            // lblKhoa
            // 
            this.lblKhoa.AutoSize = false;
            this.lblKhoa.Location = new System.Drawing.Point(10, 51);
            this.lblKhoa.Size = new System.Drawing.Size(120, 24);
            this.lblKhoa.Text = "Khoa";
            this.lblKhoa.Name = "lblKhoa";
            this.lblKhoa.TabIndex = 4;
            // 
            // cmbKhoa
            // 
            this.cmbKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKhoa.FormattingEnabled = true;
            this.cmbKhoa.Location = new System.Drawing.Point(135, 45);
            this.cmbKhoa.Size = new System.Drawing.Size(170, 29);
            this.cmbKhoa.Name = "cmbKhoa";
            this.cmbKhoa.TabIndex = 5;
            // 
            // lblGiuong
            // 
            this.lblGiuong.AutoSize = false;
            this.lblGiuong.Location = new System.Drawing.Point(10, 86);
            this.lblGiuong.Size = new System.Drawing.Size(120, 24);
            this.lblGiuong.Text = "Giường";
            this.lblGiuong.Name = "lblGiuong";
            this.lblGiuong.TabIndex = 6;
            // 
            // cmbGiuong
            // 
            this.cmbGiuong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGiuong.FormattingEnabled = true;
            this.cmbGiuong.Location = new System.Drawing.Point(135, 80);
            this.cmbGiuong.Size = new System.Drawing.Size(170, 29);
            this.cmbGiuong.Name = "cmbGiuong";
            this.cmbGiuong.TabIndex = 7;
            // 
            // lblLyDo
            // 
            this.lblLyDo.AutoSize = false;
            this.lblLyDo.Location = new System.Drawing.Point(10, 121);
            this.lblLyDo.Size = new System.Drawing.Size(120, 24);
            this.lblLyDo.Text = "Lý do nhập viện";
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.TabIndex = 8;
            // 
            // txtLyDo
            // 
            this.txtLyDo.Location = new System.Drawing.Point(135, 115);
            this.txtLyDo.Size = new System.Drawing.Size(170, 27);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.TabIndex = 9;
            // 
            // lblBN
            // 
            this.lblBN.AutoSize = false;
            this.lblBN.Location = new System.Drawing.Point(330, 16);
            this.lblBN.Size = new System.Drawing.Size(120, 24);
            this.lblBN.Text = "Bệnh nhân";
            this.lblBN.Name = "lblBN";
            this.lblBN.TabIndex = 10;
            // 
            // cmbBN
            // 
            this.cmbBN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBN.FormattingEnabled = true;
            this.cmbBN.Location = new System.Drawing.Point(455, 10);
            this.cmbBN.Size = new System.Drawing.Size(200, 29);
            this.cmbBN.Name = "cmbBN";
            this.cmbBN.TabIndex = 11;
            // 
            // lblPhong
            // 
            this.lblPhong.AutoSize = false;
            this.lblPhong.Location = new System.Drawing.Point(330, 51);
            this.lblPhong.Size = new System.Drawing.Size(120, 24);
            this.lblPhong.Text = "Phòng";
            this.lblPhong.Name = "lblPhong";
            this.lblPhong.TabIndex = 12;
            // 
            // cmbPhong
            // 
            this.cmbPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPhong.FormattingEnabled = true;
            this.cmbPhong.Location = new System.Drawing.Point(455, 45);
            this.cmbPhong.Size = new System.Drawing.Size(200, 29);
            this.cmbPhong.Name = "cmbPhong";
            this.cmbPhong.TabIndex = 13;
            // 
            // lblBS
            // 
            this.lblBS.AutoSize = false;
            this.lblBS.Location = new System.Drawing.Point(330, 86);
            this.lblBS.Size = new System.Drawing.Size(120, 24);
            this.lblBS.Text = "Bác sĩ phụ trách";
            this.lblBS.Name = "lblBS";
            this.lblBS.TabIndex = 14;
            // 
            // cmbBS
            // 
            this.cmbBS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBS.FormattingEnabled = true;
            this.cmbBS.Location = new System.Drawing.Point(455, 80);
            this.cmbBS.Size = new System.Drawing.Size(200, 29);
            this.cmbBS.Name = "cmbBS";
            this.cmbBS.TabIndex = 15;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Location = new System.Drawing.Point(330, 121);
            this.lblGhiChu.Size = new System.Drawing.Size(120, 24);
            this.lblGhiChu.Text = "Ghi chú";
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.TabIndex = 16;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(455, 115);
            this.txtGhiChu.Size = new System.Drawing.Size(200, 27);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.TabIndex = 17;
            // 
            // lblVao
            // 
            this.lblVao.AutoSize = false;
            this.lblVao.Location = new System.Drawing.Point(670, 16);
            this.lblVao.Size = new System.Drawing.Size(120, 24);
            this.lblVao.Text = "Ngày vào viện";
            this.lblVao.Name = "lblVao";
            this.lblVao.TabIndex = 18;
            // 
            // dtpVao
            // 
            this.dtpVao.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpVao.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVao.Location = new System.Drawing.Point(795, 10);
            this.dtpVao.Size = new System.Drawing.Size(190, 27);
            this.dtpVao.Name = "dtpVao";
            this.dtpVao.TabIndex = 19;
            // 
            // lblRa
            // 
            this.lblRa.AutoSize = false;
            this.lblRa.Location = new System.Drawing.Point(670, 51);
            this.lblRa.Size = new System.Drawing.Size(120, 24);
            this.lblRa.Text = "Ngày ra viện";
            this.lblRa.Name = "lblRa";
            this.lblRa.TabIndex = 20;
            // 
            // dtpRa
            // 
            this.dtpRa.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpRa.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRa.Location = new System.Drawing.Point(795, 45);
            this.dtpRa.Size = new System.Drawing.Size(190, 27);
            this.dtpRa.Name = "dtpRa";
            this.dtpRa.TabIndex = 21;
            // 
            // lblTrangThaiTen
            // 
            this.lblTrangThaiTen.AutoSize = false;
            this.lblTrangThaiTen.Location = new System.Drawing.Point(670, 86);
            this.lblTrangThaiTen.Size = new System.Drawing.Size(120, 24);
            this.lblTrangThaiTen.Text = "Trạng thái";
            this.lblTrangThaiTen.Name = "lblTrangThaiTen";
            this.lblTrangThaiTen.TabIndex = 22;
            // 
            // lblTT
            // 
            this.lblTT.AutoSize = false;
            this.lblTT.Location = new System.Drawing.Point(795, 86);
            this.lblTT.Size = new System.Drawing.Size(190, 24);
            this.lblTT.Text = "Mới";
            this.lblTT.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTT.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.lblTT.Name = "lblTT";
            this.lblTT.TabIndex = 23;
            // 
            // btnTaoHoSo
            // 
            this.btnTaoHoSo.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnTaoHoSo.FlatAppearance.BorderSize = 0;
            this.btnTaoHoSo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoHoSo.ForeColor = System.Drawing.Color.White;
            this.btnTaoHoSo.Location = new System.Drawing.Point(10, 160);
            this.btnTaoHoSo.Size = new System.Drawing.Size(110, 32);
            this.btnTaoHoSo.Text = "Tạo hồ sơ";
            this.btnTaoHoSo.UseVisualStyleBackColor = false;
            this.btnTaoHoSo.Click += new System.EventHandler(this.btnTaoHoSo_Click);
            this.btnTaoHoSo.Name = "btnTaoHoSo";
            this.btnTaoHoSo.TabIndex = 24;
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnCapNhat.FlatAppearance.BorderSize = 0;
            this.btnCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(130, 160);
            this.btnCapNhat.Size = new System.Drawing.Size(100, 32);
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.TabIndex = 25;
            // 
            // btnRaVien
            // 
            this.btnRaVien.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnRaVien.FlatAppearance.BorderSize = 0;
            this.btnRaVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRaVien.ForeColor = System.Drawing.Color.White;
            this.btnRaVien.Location = new System.Drawing.Point(240, 160);
            this.btnRaVien.Size = new System.Drawing.Size(110, 32);
            this.btnRaVien.Text = "Cho ra viện";
            this.btnRaVien.UseVisualStyleBackColor = false;
            this.btnRaVien.Click += new System.EventHandler(this.btnRaVien_Click);
            this.btnRaVien.Name = "btnRaVien";
            this.btnRaVien.TabIndex = 26;
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(360, 160);
            this.btnHuy.Size = new System.Drawing.Size(100, 32);
            this.btnHuy.Text = "Hủy hồ sơ";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.TabIndex = 27;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(470, 160);
            this.btnLamMoi.Size = new System.Drawing.Size(90, 32);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.TabIndex = 28;
            // 
            // lblTieuDeTim
            // 
            this.lblTieuDeTim.AutoSize = true;
            this.lblTieuDeTim.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeTim.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.lblTieuDeTim.Location = new System.Drawing.Point(10, 203);
            this.lblTieuDeTim.Name = "lblTieuDeTim";
            this.lblTieuDeTim.Size = new System.Drawing.Size(180, 17);
            this.lblTieuDeTim.Text = "TÌM KIẾM HỒ SƠ NHẬP VIỆN";
            // 
            // lblTimMa
            // 
            this.lblTimMa.AutoSize = false;
            this.lblTimMa.Location = new System.Drawing.Point(10, 234);
            this.lblTimMa.Size = new System.Drawing.Size(55, 24);
            this.lblTimMa.Text = "Mã NV";
            this.lblTimMa.Name = "lblTimMa";
            this.lblTimMa.TabIndex = 29;
            // 
            // txtTimMa
            // 
            this.txtTimMa.Location = new System.Drawing.Point(68, 229);
            this.txtTimMa.Size = new System.Drawing.Size(90, 27);
            this.txtTimMa.Name = "txtTimMa";
            this.txtTimMa.TabIndex = 30;
            // 
            // lblTimBN
            // 
            this.lblTimBN.AutoSize = false;
            this.lblTimBN.Location = new System.Drawing.Point(168, 234);
            this.lblTimBN.Size = new System.Drawing.Size(75, 24);
            this.lblTimBN.Text = "Bệnh nhân";
            this.lblTimBN.Name = "lblTimBN";
            this.lblTimBN.TabIndex = 31;
            // 
            // txtTimBN
            // 
            this.txtTimBN.Location = new System.Drawing.Point(245, 229);
            this.txtTimBN.Size = new System.Drawing.Size(160, 27);
            this.txtTimBN.Name = "txtTimBN";
            this.txtTimBN.TabIndex = 32;
            // 
            // lblTimTT
            // 
            this.lblTimTT.AutoSize = false;
            this.lblTimTT.Location = new System.Drawing.Point(415, 234);
            this.lblTimTT.Size = new System.Drawing.Size(75, 24);
            this.lblTimTT.Text = "Trạng thái";
            this.lblTimTT.Name = "lblTimTT";
            this.lblTimTT.TabIndex = 33;
            // 
            // cmbTimTT
            // 
            this.cmbTimTT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimTT.FormattingEnabled = true;
            this.cmbTimTT.Items.AddRange(new object[] { "-- Tất cả --", "Đang điều trị", "Đã ra viện", "Hủy" });
            this.cmbTimTT.Location = new System.Drawing.Point(492, 228);
            this.cmbTimTT.Size = new System.Drawing.Size(140, 29);
            this.cmbTimTT.Name = "cmbTimTT";
            this.cmbTimTT.TabIndex = 34;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(645, 226);
            this.btnTimKiem.Size = new System.Drawing.Size(110, 31);
            this.btnTimKiem.Text = "🔍 Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.TabIndex = 35;
            // 
            // btnLamMoiTim
            // 
            this.btnLamMoiTim.BackColor = System.Drawing.Color.FromArgb(238, 242, 248);
            this.btnLamMoiTim.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 200, 225);
            this.btnLamMoiTim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiTim.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnLamMoiTim.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.btnLamMoiTim.Location = new System.Drawing.Point(765, 226);
            this.btnLamMoiTim.Size = new System.Drawing.Size(100, 31);
            this.btnLamMoiTim.Text = "🔄 Làm mới";
            this.btnLamMoiTim.UseVisualStyleBackColor = false;
            this.btnLamMoiTim.Click += new System.EventHandler(this.btnLamMoiTim_Click);
            this.btnLamMoiTim.Name = "btnLamMoiTim";
            this.btnLamMoiTim.TabIndex = 36;
            // 
            // NhapVienForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1085, 650);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Text = "Nhập viện";
            this.Name = "NhapVienForm";
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
        private System.Windows.Forms.Label lblKhoa;
        private System.Windows.Forms.ComboBox cmbKhoa;
        private System.Windows.Forms.Label lblGiuong;
        private System.Windows.Forms.ComboBox cmbGiuong;
        private System.Windows.Forms.Label lblLyDo;
        private System.Windows.Forms.TextBox txtLyDo;
        private System.Windows.Forms.Label lblBN;
        private System.Windows.Forms.ComboBox cmbBN;
        private System.Windows.Forms.Label lblPhong;
        private System.Windows.Forms.ComboBox cmbPhong;
        private System.Windows.Forms.Label lblBS;
        private System.Windows.Forms.ComboBox cmbBS;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblVao;
        private System.Windows.Forms.DateTimePicker dtpVao;
        private System.Windows.Forms.Label lblRa;
        private System.Windows.Forms.DateTimePicker dtpRa;
        private System.Windows.Forms.Label lblTrangThaiTen;
        private System.Windows.Forms.Label lblTT;
        private System.Windows.Forms.Button btnTaoHoSo;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnRaVien;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTieuDeTim;
        private System.Windows.Forms.Label lblTimMa;
        private System.Windows.Forms.TextBox txtTimMa;
        private System.Windows.Forms.Label lblTimBN;
        private System.Windows.Forms.TextBox txtTimBN;
        private System.Windows.Forms.Label lblTimTT;
        private System.Windows.Forms.ComboBox cmbTimTT;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoiTim;
    }
}
