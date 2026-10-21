namespace QuanLyNoiTru
{
    partial class PhongForm
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

            this.dgv = new System.Windows.Forms.DataGridView();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblMa = new System.Windows.Forms.Label();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblKhoa = new System.Windows.Forms.Label();
            this.cmbKhoa = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTieuDeTim = new System.Windows.Forms.Label();
            this.lblTimMa = new System.Windows.Forms.Label();
            this.txtTimMa = new System.Windows.Forms.TextBox();
            this.lblTimTen = new System.Windows.Forms.Label();
            this.txtTimTen = new System.Windows.Forms.TextBox();
            this.lblTimKhoa = new System.Windows.Forms.Label();
            this.cmbTimKhoa = new System.Windows.Forms.ComboBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnLamMoiTim = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();

            // dgv
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AlternatingRowsDefaultCellStyle = altStyle;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(900, 400);
            this.dgv.Name = "dgv";

            // pnlTop
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Size = new System.Drawing.Size(900, 240);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Controls.Add(this.lblMa);
            this.pnlTop.Controls.Add(this.txtMa);
            this.pnlTop.Controls.Add(this.lblTen);
            this.pnlTop.Controls.Add(this.txtTen);
            this.pnlTop.Controls.Add(this.lblKhoa);
            this.pnlTop.Controls.Add(this.cmbKhoa);
            this.pnlTop.Controls.Add(this.btnThem);
            this.pnlTop.Controls.Add(this.btnSua);
            this.pnlTop.Controls.Add(this.btnXoa);
            this.pnlTop.Controls.Add(this.btnLamMoi);
            this.pnlTop.Controls.Add(this.lblTieuDeTim);
            this.pnlTop.Controls.Add(this.lblTimMa);
            this.pnlTop.Controls.Add(this.txtTimMa);
            this.pnlTop.Controls.Add(this.lblTimTen);
            this.pnlTop.Controls.Add(this.txtTimTen);
            this.pnlTop.Controls.Add(this.lblTimKhoa);
            this.pnlTop.Controls.Add(this.cmbTimKhoa);
            this.pnlTop.Controls.Add(this.btnTimKiem);
            this.pnlTop.Controls.Add(this.btnLamMoiTim);

            // lblMa
            this.lblMa.Location = new System.Drawing.Point(10, 16);
            this.lblMa.Size = new System.Drawing.Size(100, 24);
            this.lblMa.Text = "Mã phòng";

            // txtMa
            this.txtMa.Location = new System.Drawing.Point(115, 10);
            this.txtMa.ReadOnly = true;
            this.txtMa.Size = new System.Drawing.Size(170, 27);

            // lblTen
            this.lblTen.Location = new System.Drawing.Point(10, 51);
            this.lblTen.Size = new System.Drawing.Size(100, 24);
            this.lblTen.Text = "Tên phòng";

            // txtTen
            this.txtTen.Location = new System.Drawing.Point(115, 45);
            this.txtTen.Size = new System.Drawing.Size(170, 27);

            // lblKhoa
            this.lblKhoa.Location = new System.Drawing.Point(10, 86);
            this.lblKhoa.Size = new System.Drawing.Size(100, 24);
            this.lblKhoa.Text = "Khoa";

            // cmbKhoa
            this.cmbKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKhoa.FormattingEnabled = true;
            this.cmbKhoa.Location = new System.Drawing.Point(115, 80);
            this.cmbKhoa.Size = new System.Drawing.Size(170, 29);

            // btnThem
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(10, 120);
            this.btnThem.Size = new System.Drawing.Size(90, 32);
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            // btnSua
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(110, 120);
            this.btnSua.Size = new System.Drawing.Size(90, 32);
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            // btnXoa
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(210, 120);
            this.btnXoa.Size = new System.Drawing.Size(90, 32);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            // btnLamMoi
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(310, 120);
            this.btnLamMoi.Size = new System.Drawing.Size(90, 32);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // 
            // lblTieuDeTim
            // 
            this.lblTieuDeTim.AutoSize = false;
            this.lblTieuDeTim.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeTim.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.lblTieuDeTim.Location = new System.Drawing.Point(10, 168);
            this.lblTieuDeTim.Size = new System.Drawing.Size(260, 24);
            this.lblTieuDeTim.Text = "🔍 TÌM KIẾM PHÒNG BỆNH";
            this.lblTieuDeTim.Name = "lblTieuDeTim";
            this.lblTieuDeTim.TabIndex = 17;
            // 
            // lblTimMa
            // 
            this.lblTimMa.AutoSize = false;
            this.lblTimMa.Location = new System.Drawing.Point(10, 202);
            this.lblTimMa.Size = new System.Drawing.Size(70, 24);
            this.lblTimMa.Text = "Mã phòng";
            this.lblTimMa.Name = "lblTimMa";
            this.lblTimMa.TabIndex = 18;
            // 
            // txtTimMa
            // 
            this.txtTimMa.Location = new System.Drawing.Point(85, 197);
            this.txtTimMa.Size = new System.Drawing.Size(100, 27);
            this.txtTimMa.Name = "txtTimMa";
            this.txtTimMa.TabIndex = 19;
            // 
            // lblTimTen
            // 
            this.lblTimTen.AutoSize = false;
            this.lblTimTen.Location = new System.Drawing.Point(195, 202);
            this.lblTimTen.Size = new System.Drawing.Size(75, 24);
            this.lblTimTen.Text = "Tên phòng";
            this.lblTimTen.Name = "lblTimTen";
            this.lblTimTen.TabIndex = 20;
            // 
            // txtTimTen
            // 
            this.txtTimTen.Location = new System.Drawing.Point(275, 197);
            this.txtTimTen.Size = new System.Drawing.Size(150, 27);
            this.txtTimTen.Name = "txtTimTen";
            this.txtTimTen.TabIndex = 21;
            // 
            // lblTimKhoa
            // 
            this.lblTimKhoa.AutoSize = false;
            this.lblTimKhoa.Location = new System.Drawing.Point(435, 202);
            this.lblTimKhoa.Size = new System.Drawing.Size(45, 24);
            this.lblTimKhoa.Text = "Khoa";
            this.lblTimKhoa.Name = "lblTimKhoa";
            this.lblTimKhoa.TabIndex = 22;
            // 
            // cmbTimKhoa
            // 
            this.cmbTimKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimKhoa.FormattingEnabled = true;
            this.cmbTimKhoa.Location = new System.Drawing.Point(485, 196);
            this.cmbTimKhoa.Size = new System.Drawing.Size(150, 29);
            this.cmbTimKhoa.Name = "cmbTimKhoa";
            this.cmbTimKhoa.TabIndex = 23;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(52, 101, 164);
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(645, 195);
            this.btnTimKiem.Size = new System.Drawing.Size(110, 31);
            this.btnTimKiem.Text = "🔍 Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.TabIndex = 24;
            // 
            // btnLamMoiTim
            // 
            this.btnLamMoiTim.BackColor = System.Drawing.Color.FromArgb(238, 242, 248);
            this.btnLamMoiTim.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 200, 225);
            this.btnLamMoiTim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiTim.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnLamMoiTim.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.btnLamMoiTim.Location = new System.Drawing.Point(765, 195);
            this.btnLamMoiTim.Size = new System.Drawing.Size(100, 31);
            this.btnLamMoiTim.Text = "🔄 Làm mới";
            this.btnLamMoiTim.UseVisualStyleBackColor = false;
            this.btnLamMoiTim.Click += new System.EventHandler(this.btnLamMoiTim_Click);
            this.btnLamMoiTim.Name = "btnLamMoiTim";
            this.btnLamMoiTim.TabIndex = 25;

            // PhongForm
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Text = "Quản lý phòng";
            this.Name = "PhongForm";
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
        private System.Windows.Forms.Label lblKhoa;
        private System.Windows.Forms.ComboBox cmbKhoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTieuDeTim;
        private System.Windows.Forms.Label lblTimMa;
        private System.Windows.Forms.TextBox txtTimMa;
        private System.Windows.Forms.Label lblTimTen;
        private System.Windows.Forms.TextBox txtTimTen;
        private System.Windows.Forms.Label lblTimKhoa;
        private System.Windows.Forms.ComboBox cmbTimKhoa;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoiTim;
    }
}
