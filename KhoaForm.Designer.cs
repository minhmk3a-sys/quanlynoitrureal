namespace QuanLyNoiTru
{
    partial class KhoaForm
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
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTim = new System.Windows.Forms.Label();
            this.txtTim = new System.Windows.Forms.TextBox();

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
            this.dgv.TabIndex = 0;

            // pnlTop
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Size = new System.Drawing.Size(900, 160);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.TabIndex = 1;
            this.pnlTop.Controls.Add(this.lblMa);
            this.pnlTop.Controls.Add(this.txtMa);
            this.pnlTop.Controls.Add(this.lblTen);
            this.pnlTop.Controls.Add(this.txtTen);
            this.pnlTop.Controls.Add(this.chkTrangThai);
            this.pnlTop.Controls.Add(this.lblGhiChu);
            this.pnlTop.Controls.Add(this.txtGhiChu);
            this.pnlTop.Controls.Add(this.btnThem);
            this.pnlTop.Controls.Add(this.btnSua);
            this.pnlTop.Controls.Add(this.btnXoa);
            this.pnlTop.Controls.Add(this.btnLamMoi);
            this.pnlTop.Controls.Add(this.lblTim);
            this.pnlTop.Controls.Add(this.txtTim);

            // lblMa
            this.lblMa.Location = new System.Drawing.Point(10, 16);
            this.lblMa.Size = new System.Drawing.Size(100, 24);
            this.lblMa.Text = "Mã khoa";
            this.lblMa.Name = "lblMa";

            // txtMa
            this.txtMa.Location = new System.Drawing.Point(115, 10);
            this.txtMa.ReadOnly = true;
            this.txtMa.Size = new System.Drawing.Size(170, 27);
            this.txtMa.Name = "txtMa";

            // lblTen
            this.lblTen.Location = new System.Drawing.Point(10, 51);
            this.lblTen.Size = new System.Drawing.Size(100, 24);
            this.lblTen.Text = "Tên khoa";
            this.lblTen.Name = "lblTen";

            // txtTen
            this.txtTen.Location = new System.Drawing.Point(115, 45);
            this.txtTen.Size = new System.Drawing.Size(170, 27);
            this.txtTen.Name = "txtTen";

            // chkTrangThai
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrangThai.Location = new System.Drawing.Point(330, 12);
            this.chkTrangThai.Text = "Hoạt động";
            this.chkTrangThai.Name = "chkTrangThai";

            // lblGhiChu
            this.lblGhiChu.Location = new System.Drawing.Point(10, 86);
            this.lblGhiChu.Size = new System.Drawing.Size(100, 24);
            this.lblGhiChu.Text = "Ghi chú";
            this.lblGhiChu.Name = "lblGhiChu";

            // txtGhiChu
            this.txtGhiChu.Location = new System.Drawing.Point(115, 80);
            this.txtGhiChu.Size = new System.Drawing.Size(350, 27);
            this.txtGhiChu.Name = "txtGhiChu";

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
            this.btnThem.Name = "btnThem";

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
            this.btnSua.Name = "btnSua";

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
            this.btnXoa.Name = "btnXoa";

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
            this.btnLamMoi.Name = "btnLamMoi";

            // lblTim
            this.lblTim.Location = new System.Drawing.Point(430, 126);
            this.lblTim.Size = new System.Drawing.Size(80, 24);
            this.lblTim.Text = "Tìm kiếm";
            this.lblTim.Name = "lblTim";

            // txtTim
            this.txtTim.Location = new System.Drawing.Point(515, 120);
            this.txtTim.Size = new System.Drawing.Size(250, 27);
            this.txtTim.Name = "txtTim";

            // KhoaForm
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Text = "Quản lý khoa";
            this.Name = "KhoaForm";
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
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.TextBox txtTim;
    }
}
