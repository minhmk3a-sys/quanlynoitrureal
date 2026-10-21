namespace QuanLyNoiTru
{
    partial class XuatVienForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle altStyle = new System.Windows.Forms.DataGridViewCellStyle();
            altStyle.BackColor = System.Drawing.Color.FromArgb(242, 247, 252);

            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlTopList = new System.Windows.Forms.Panel();
            this.lblTim = new System.Windows.Forms.Label();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();

            // Controls của panel bên phải
            this.lblHeaderChiPhi = new System.Windows.Forms.Label();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.lblTenBN = new System.Windows.Forms.Label();
            this.txtTenBN = new System.Windows.Forms.TextBox();
            this.lblBHYT = new System.Windows.Forms.Label();
            this.txtBHYT = new System.Windows.Forms.TextBox();
            this.lblKhoaPhong = new System.Windows.Forms.Label();
            this.txtKhoaPhong = new System.Windows.Forms.TextBox();
            this.lblNgayVao = new System.Windows.Forms.Label();
            this.txtNgayVao = new System.Windows.Forms.TextBox();
            this.lblNgayRa = new System.Windows.Forms.Label();
            this.dtpNgayRa = new System.Windows.Forms.DateTimePicker();
            this.lblSoNgayNam = new System.Windows.Forms.Label();
            this.txtSoNgayNam = new System.Windows.Forms.TextBox();

            this.lblSep1 = new System.Windows.Forms.Label();
            this.lblTienGiuong = new System.Windows.Forms.Label();
            this.txtTienGiuong = new System.Windows.Forms.TextBox();
            this.lblTienThuoc = new System.Windows.Forms.Label();
            this.txtTienThuoc = new System.Windows.Forms.TextBox();
            this.lblTienDichVu = new System.Windows.Forms.Label();
            this.txtTienDichVu = new System.Windows.Forms.TextBox();

            this.lblTongTien = new System.Windows.Forms.Label();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.lblBHTra = new System.Windows.Forms.Label();
            this.txtBHTra = new System.Windows.Forms.TextBox();
            this.lblBNTra = new System.Windows.Forms.Label();
            this.txtBNTra = new System.Windows.Forms.TextBox();

            this.lblTT_NV_Label = new System.Windows.Forms.Label();
            this.lblTrangThaiNV = new System.Windows.Forms.Label();
            this.lblTT_VP_Label = new System.Windows.Forms.Label();
            this.lblTrangThaiVP = new System.Windows.Forms.Label();

            this.btnLuuVienPhi = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnHoanTatXuatVien = new System.Windows.Forms.Button();
            this.btnInPhieu = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.pnlTopList.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlTopList
            // 
            this.pnlTopList.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.pnlTopList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopList.Location = new System.Drawing.Point(0, 0);
            this.pnlTopList.Size = new System.Drawing.Size(650, 48);
            this.pnlTopList.Name = "pnlTopList";
            this.pnlTopList.Controls.Add(this.lblTim);
            this.pnlTopList.Controls.Add(this.txtTim);
            this.pnlTopList.Controls.Add(this.btnLamMoi);

            // lblTim
            this.lblTim.Location = new System.Drawing.Point(12, 14);
            this.lblTim.Size = new System.Drawing.Size(80, 24);
            this.lblTim.Text = "Tìm kiếm:";

            // txtTim
            this.txtTim.Location = new System.Drawing.Point(95, 10);
            this.txtTim.Size = new System.Drawing.Size(260, 27);

            // btnLamMoi
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(370, 8);
            this.btnLamMoi.Size = new System.Drawing.Size(100, 30);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AlternatingRowsDefaultCellStyle = altStyle;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 48);
            this.dgv.MultiSelect = false;
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(650, 602);
            this.dgv.Name = "dgv";

            // 
            // pnlRight (Bảng tổng hợp chi phí và nút xuất viện)
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(248, 250, 253);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(650, 0);
            this.pnlRight.Size = new System.Drawing.Size(460, 650);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.AutoScroll = true;
            this.pnlRight.Padding = new System.Windows.Forms.Padding(15);

            // lblHeaderChiPhi
            this.lblHeaderChiPhi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeaderChiPhi.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.lblHeaderChiPhi.Location = new System.Drawing.Point(15, 12);
            this.lblHeaderChiPhi.Size = new System.Drawing.Size(430, 28);
            this.lblHeaderChiPhi.Text = "BẢNG TỔNG HỢP CHI PHÍ XUẤT VIỆN";

            // Mã NV & Bệnh nhân
            this.lblMaNV.Location = new System.Drawing.Point(15, 48);
            this.lblMaNV.Size = new System.Drawing.Size(110, 24);
            this.lblMaNV.Text = "Mã nhập viện:";
            this.txtMaNV.Location = new System.Drawing.Point(135, 45);
            this.txtMaNV.Size = new System.Drawing.Size(305, 27);
            this.txtMaNV.ReadOnly = true;

            this.lblTenBN.Location = new System.Drawing.Point(15, 83);
            this.lblTenBN.Size = new System.Drawing.Size(110, 24);
            this.lblTenBN.Text = "Bệnh nhân:";
            this.txtTenBN.Location = new System.Drawing.Point(135, 80);
            this.txtTenBN.Size = new System.Drawing.Size(305, 27);
            this.txtTenBN.ReadOnly = true;

            this.lblBHYT.Location = new System.Drawing.Point(15, 118);
            this.lblBHYT.Size = new System.Drawing.Size(110, 24);
            this.lblBHYT.Text = "Thẻ BHYT:";
            this.txtBHYT.Location = new System.Drawing.Point(135, 115);
            this.txtBHYT.Size = new System.Drawing.Size(305, 27);
            this.txtBHYT.ReadOnly = true;

            this.lblKhoaPhong.Location = new System.Drawing.Point(15, 153);
            this.lblKhoaPhong.Size = new System.Drawing.Size(110, 24);
            this.lblKhoaPhong.Text = "Khoa / Giường:";
            this.txtKhoaPhong.Location = new System.Drawing.Point(135, 150);
            this.txtKhoaPhong.Size = new System.Drawing.Size(305, 27);
            this.txtKhoaPhong.ReadOnly = true;

            this.lblNgayVao.Location = new System.Drawing.Point(15, 188);
            this.lblNgayVao.Size = new System.Drawing.Size(110, 24);
            this.lblNgayVao.Text = "Ngày vào:";
            this.txtNgayVao.Location = new System.Drawing.Point(135, 185);
            this.txtNgayVao.Size = new System.Drawing.Size(140, 27);
            this.txtNgayVao.ReadOnly = true;

            this.lblSoNgayNam.Location = new System.Drawing.Point(285, 188);
            this.lblSoNgayNam.Size = new System.Drawing.Size(65, 24);
            this.lblSoNgayNam.Text = "Số ngày:";
            this.txtSoNgayNam.Location = new System.Drawing.Point(355, 185);
            this.txtSoNgayNam.Size = new System.Drawing.Size(85, 27);
            this.txtSoNgayNam.ReadOnly = true;

            this.lblNgayRa.Location = new System.Drawing.Point(15, 223);
            this.lblNgayRa.Size = new System.Drawing.Size(110, 24);
            this.lblNgayRa.Text = "Ngày ra viện:";
            this.dtpNgayRa.Location = new System.Drawing.Point(135, 220);
            this.dtpNgayRa.Size = new System.Drawing.Size(305, 27);
            this.dtpNgayRa.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayRa.CustomFormat = "dd/MM/yyyy HH:mm";

            // Đường kẻ
            this.lblSep1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSep1.Location = new System.Drawing.Point(15, 258);
            this.lblSep1.Size = new System.Drawing.Size(425, 2);

            // Chi tiết chi phí
            this.lblTienGiuong.Location = new System.Drawing.Point(15, 272);
            this.lblTienGiuong.Size = new System.Drawing.Size(120, 24);
            this.lblTienGiuong.Text = "1. Tiền giường:";
            this.txtTienGiuong.Location = new System.Drawing.Point(145, 269);
            this.txtTienGiuong.Size = new System.Drawing.Size(295, 27);
            this.txtTienGiuong.ReadOnly = true;
            this.txtTienGiuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblTienThuoc.Location = new System.Drawing.Point(15, 307);
            this.lblTienThuoc.Size = new System.Drawing.Size(120, 24);
            this.lblTienThuoc.Text = "2. Tiền thuốc:";
            this.txtTienThuoc.Location = new System.Drawing.Point(145, 304);
            this.txtTienThuoc.Size = new System.Drawing.Size(295, 27);
            this.txtTienThuoc.ReadOnly = true;
            this.txtTienThuoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblTienDichVu.Location = new System.Drawing.Point(15, 342);
            this.lblTienDichVu.Size = new System.Drawing.Size(120, 24);
            this.lblTienDichVu.Text = "3. Tiền dịch vụ:";
            this.txtTienDichVu.Location = new System.Drawing.Point(145, 339);
            this.txtTienDichVu.Size = new System.Drawing.Size(295, 27);
            this.txtTienDichVu.ReadOnly = true;
            this.txtTienDichVu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.Location = new System.Drawing.Point(15, 380);
            this.lblTongTien.Size = new System.Drawing.Size(120, 24);
            this.lblTongTien.Text = "TỔNG VIỆN PHÍ:";
            this.txtTongTien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTongTien.Location = new System.Drawing.Point(145, 377);
            this.txtTongTien.Size = new System.Drawing.Size(295, 27);
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblBHTra.ForeColor = System.Drawing.Color.FromArgb(0, 100, 0);
            this.lblBHTra.Location = new System.Drawing.Point(15, 415);
            this.lblBHTra.Size = new System.Drawing.Size(120, 24);
            this.lblBHTra.Text = "Bảo hiểm trả:";
            this.txtBHTra.ForeColor = System.Drawing.Color.FromArgb(0, 100, 0);
            this.txtBHTra.Location = new System.Drawing.Point(145, 412);
            this.txtBHTra.Size = new System.Drawing.Size(295, 27);
            this.txtBHTra.ReadOnly = true;
            this.txtBHTra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblBNTra.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBNTra.ForeColor = System.Drawing.Color.FromArgb(160, 50, 50);
            this.lblBNTra.Location = new System.Drawing.Point(15, 450);
            this.lblBNTra.Size = new System.Drawing.Size(120, 24);
            this.lblBNTra.Text = "BN THANH TOÁN:";
            this.txtBNTra.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtBNTra.ForeColor = System.Drawing.Color.FromArgb(160, 50, 50);
            this.txtBNTra.Location = new System.Drawing.Point(145, 447);
            this.txtBNTra.Size = new System.Drawing.Size(295, 27);
            this.txtBNTra.ReadOnly = true;
            this.txtBNTra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            // Trạng thái
            this.lblTT_VP_Label.Location = new System.Drawing.Point(15, 485);
            this.lblTT_VP_Label.Size = new System.Drawing.Size(120, 24);
            this.lblTT_VP_Label.Text = "Trạng thái VP:";
            this.lblTrangThaiVP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiVP.ForeColor = System.Drawing.Color.Red;
            this.lblTrangThaiVP.Location = new System.Drawing.Point(145, 485);
            this.lblTrangThaiVP.Size = new System.Drawing.Size(295, 24);
            this.lblTrangThaiVP.Text = "Chưa thanh toán";

            // Các nút thao tác
            this.btnLuuVienPhi.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnLuuVienPhi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuVienPhi.ForeColor = System.Drawing.Color.White;
            this.btnLuuVienPhi.Location = new System.Drawing.Point(15, 520);
            this.btnLuuVienPhi.Size = new System.Drawing.Size(130, 34);
            this.btnLuuVienPhi.Text = "Lưu viện phí";
            this.btnLuuVienPhi.UseVisualStyleBackColor = false;
            this.btnLuuVienPhi.Click += new System.EventHandler(this.btnLuuVienPhi_Click);

            this.btnThanhToan.BackColor = System.Drawing.Color.FromArgb(30, 130, 60);
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(155, 520);
            this.btnThanhToan.Size = new System.Drawing.Size(140, 34);
            this.btnThanhToan.Text = "Thanh toán (100%)";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);

            this.btnInPhieu.BackColor = System.Drawing.Color.FromArgb(70, 70, 70);
            this.btnInPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInPhieu.ForeColor = System.Drawing.Color.White;
            this.btnInPhieu.Location = new System.Drawing.Point(305, 520);
            this.btnInPhieu.Size = new System.Drawing.Size(135, 34);
            this.btnInPhieu.Text = "In phiếu";
            this.btnInPhieu.UseVisualStyleBackColor = false;
            this.btnInPhieu.Click += new System.EventHandler(this.btnInPhieu_Click);

            this.btnHoanTatXuatVien.BackColor = System.Drawing.Color.FromArgb(180, 40, 40);
            this.btnHoanTatXuatVien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnHoanTatXuatVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoanTatXuatVien.ForeColor = System.Drawing.Color.White;
            this.btnHoanTatXuatVien.Location = new System.Drawing.Point(15, 565);
            this.btnHoanTatXuatVien.Size = new System.Drawing.Size(425, 42);
            this.btnHoanTatXuatVien.Text = "HOÀN TẤT XUẤT VIỆN (TRẢ GIƯỜNG)";
            this.btnHoanTatXuatVien.UseVisualStyleBackColor = false;
            this.btnHoanTatXuatVien.Click += new System.EventHandler(this.btnHoanTatXuatVien_Click);

            // Thêm controls vào panel
            this.pnlRight.Controls.Add(this.lblHeaderChiPhi);
            this.pnlRight.Controls.Add(this.lblMaNV);
            this.pnlRight.Controls.Add(this.txtMaNV);
            this.pnlRight.Controls.Add(this.lblTenBN);
            this.pnlRight.Controls.Add(this.txtTenBN);
            this.pnlRight.Controls.Add(this.lblBHYT);
            this.pnlRight.Controls.Add(this.txtBHYT);
            this.pnlRight.Controls.Add(this.lblKhoaPhong);
            this.pnlRight.Controls.Add(this.txtKhoaPhong);
            this.pnlRight.Controls.Add(this.lblNgayVao);
            this.pnlRight.Controls.Add(this.txtNgayVao);
            this.pnlRight.Controls.Add(this.lblSoNgayNam);
            this.pnlRight.Controls.Add(this.txtSoNgayNam);
            this.pnlRight.Controls.Add(this.lblNgayRa);
            this.pnlRight.Controls.Add(this.dtpNgayRa);
            this.pnlRight.Controls.Add(this.lblSep1);
            this.pnlRight.Controls.Add(this.lblTienGiuong);
            this.pnlRight.Controls.Add(this.txtTienGiuong);
            this.pnlRight.Controls.Add(this.lblTienThuoc);
            this.pnlRight.Controls.Add(this.txtTienThuoc);
            this.pnlRight.Controls.Add(this.lblTienDichVu);
            this.pnlRight.Controls.Add(this.txtTienDichVu);
            this.pnlRight.Controls.Add(this.lblTongTien);
            this.pnlRight.Controls.Add(this.txtTongTien);
            this.pnlRight.Controls.Add(this.lblBHTra);
            this.pnlRight.Controls.Add(this.txtBHTra);
            this.pnlRight.Controls.Add(this.lblBNTra);
            this.pnlRight.Controls.Add(this.txtBNTra);
            this.pnlRight.Controls.Add(this.lblTT_VP_Label);
            this.pnlRight.Controls.Add(this.lblTrangThaiVP);
            this.pnlRight.Controls.Add(this.btnLuuVienPhi);
            this.pnlRight.Controls.Add(this.btnThanhToan);
            this.pnlRight.Controls.Add(this.btnInPhieu);
            this.pnlRight.Controls.Add(this.btnHoanTatXuatVien);

            // XuatVienForm
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1110, 650);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Text = "Quản lý xuất viện";
            this.Name = "XuatVienForm";
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.pnlTopList);
            this.Controls.Add(this.pnlRight);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.pnlTopList.ResumeLayout(false);
            this.pnlTopList.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlTopList;
        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgv;

        private System.Windows.Forms.Label lblHeaderChiPhi;
        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.Label lblTenBN;
        private System.Windows.Forms.TextBox txtTenBN;
        private System.Windows.Forms.Label lblBHYT;
        private System.Windows.Forms.TextBox txtBHYT;
        private System.Windows.Forms.Label lblKhoaPhong;
        private System.Windows.Forms.TextBox txtKhoaPhong;
        private System.Windows.Forms.Label lblNgayVao;
        private System.Windows.Forms.TextBox txtNgayVao;
        private System.Windows.Forms.Label lblNgayRa;
        private System.Windows.Forms.DateTimePicker dtpNgayRa;
        private System.Windows.Forms.Label lblSoNgayNam;
        private System.Windows.Forms.TextBox txtSoNgayNam;

        private System.Windows.Forms.Label lblSep1;
        private System.Windows.Forms.Label lblTienGiuong;
        private System.Windows.Forms.TextBox txtTienGiuong;
        private System.Windows.Forms.Label lblTienThuoc;
        private System.Windows.Forms.TextBox txtTienThuoc;
        private System.Windows.Forms.Label lblTienDichVu;
        private System.Windows.Forms.TextBox txtTienDichVu;

        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Label lblBHTra;
        private System.Windows.Forms.TextBox txtBHTra;
        private System.Windows.Forms.Label lblBNTra;
        private System.Windows.Forms.TextBox txtBNTra;

        private System.Windows.Forms.Label lblTT_NV_Label;
        private System.Windows.Forms.Label lblTrangThaiNV;
        private System.Windows.Forms.Label lblTT_VP_Label;
        private System.Windows.Forms.Label lblTrangThaiVP;

        private System.Windows.Forms.Button btnLuuVienPhi;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Button btnHoanTatXuatVien;
        private System.Windows.Forms.Button btnInPhieu;
    }
}
