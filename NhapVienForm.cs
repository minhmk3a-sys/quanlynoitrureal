using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>Tạo hồ sơ nhập viện, gán Khoa -> Phòng -> Giường + Bác sĩ. Giường chỉ chọn được khi còn "Trống"; ra viện / hủy sẽ tự trả giường.</summary>
    public partial class NhapVienForm : BaseForm
    {
        bool loading;                // true = đang nạp dữ liệu, tạm thời bỏ qua sự kiện đổi lựa chọn
        string editingBed = "";      // giường hiện tại của hồ sơ đang chọn
        string editingStatus = "";   // trạng thái của hồ sơ đang chọn

        public NhapVienForm()
        {
            InitializeComponent();   // dựng giao diện (code nằm trong NhapVienForm.Designer.cs)
            cmbLoc.SelectedIndex = 0;

            FillCombo(cmbBN, DatabaseHelper.GetData("SELECT MaBN, MaBN + N' - ' + HoTen AS Hien FROM BENHNHAN ORDER BY MaBN"), "Hien", "MaBN");
            FillCombo(cmbKhoa, DatabaseHelper.GetData("SELECT MaKhoa, TenKhoa FROM KHOA ORDER BY MaKhoa"), "TenKhoa", "MaKhoa");
            LoadPhongBS();
            LoadData();

            cmbKhoa.SelectedIndexChanged += (s, e) => { if (!loading) LoadPhongBS(); };
            cmbPhong.SelectedIndexChanged += (s, e) => { if (!loading) LoadGiuong(); };
            cmbLoc.SelectedIndexChanged += (s, e) => LoadData();
            txtTim.TextChanged += (s, e) => LoadData();
            dgv.SelectionChanged += (s, e) => SelectRow();
        }

        void btnLamMoi_Click(object s, EventArgs e)
        {
            ResetForm();
        }

        // ---------- nạp dữ liệu ----------

        void LoadPhongBS()
        {
            string k = GetValue(cmbKhoa);
            bool old = loading; loading = true;
            FillCombo(cmbPhong,
                DatabaseHelper.GetData(@"SELECT MaPhong, TenPhong + N' - ' + CAST(CAST(DonGia AS INT) AS NVARCHAR(20)) + N'đ/ngày' AS Hien
                           FROM PHONG WHERE MaKhoa = @k AND TrangThai = 1 ORDER BY MaPhong", DatabaseHelper.Param("@k", k)),
                "Hien", "MaPhong");
            FillCombo(cmbBS,
                DatabaseHelper.GetData(@"SELECT MaBS, MaBS + N' - ' + HoTen AS Hien
                           FROM BACSI WHERE MaKhoa = @k AND TrangThai = 1 ORDER BY MaBS", DatabaseHelper.Param("@k", k)),
                "Hien", "MaBS");
            loading = old;
            LoadGiuong();
        }

        void LoadGiuong()
        {
            string ph = GetValue(cmbPhong);
            bool old = loading; loading = true;
            FillCombo(cmbGiuong,
                DatabaseHelper.GetData(@"SELECT MaGiuong, N'Giường ' + CAST(SoGiuong AS NVARCHAR(5)) + N' (' + MaGiuong + N')' AS Hien
                           FROM GIUONG
                           WHERE MaPhong = @p AND (TrangThai = N'Trống' OR MaGiuong = @cur)
                           ORDER BY SoGiuong", DatabaseHelper.Param("@p", ph), DatabaseHelper.Param("@cur", editingBed)),
                "Hien", "MaGiuong");
            loading = old;
        }

        void LoadData()
        {
            dgv.DataSource = DatabaseHelper.GetData(
                @"SELECT nv.MaNhapVien AS [Mã NV], nv.MaBN AS [Mã BN], bn.HoTen AS [Bệnh nhân],
                         nv.MaBS AS [Mã BS], bs.HoTen AS [Bác sĩ],
                         p.MaKhoa AS [Mã khoa], p.MaPhong AS [Mã phòng], p.TenPhong AS [Phòng], nv.MaGiuong AS [Mã giường],
                         nv.NgayNhapVien AS [Ngày vào viện], nv.NgayRaVien AS [Ngày ra viện],
                         nv.LyDoNhapVien AS [Lý do], nv.TrangThai AS [Trạng thái], nv.GhiChu AS [Ghi chú]
                  FROM NHAPVIEN nv
                  JOIN BENHNHAN bn ON bn.MaBN = nv.MaBN
                  LEFT JOIN BACSI bs ON bs.MaBS = nv.MaBS
                  LEFT JOIN GIUONG g ON g.MaGiuong = nv.MaGiuong
                  LEFT JOIN PHONG p ON p.MaPhong = g.MaPhong
                  WHERE (@tt = N'Tất cả' OR nv.TrangThai = @tt)
                    AND (nv.MaNhapVien LIKE @k OR nv.MaBN LIKE @k OR bn.HoTen LIKE @k)
                  ORDER BY nv.NgayNhapVien DESC",
                DatabaseHelper.Param("@tt", cmbLoc.Text), DatabaseHelper.Param("@k", "%" + txtTim.Text.Trim() + "%"));
            FormatColumn(dgv, "Ngày vào viện", "dd/MM/yyyy HH:mm");
            FormatColumn(dgv, "Ngày ra viện", "dd/MM/yyyy HH:mm");
            // ẩn bớt cột mã dùng nội bộ cho gọn
            foreach (var c in new[] { "Mã khoa", "Mã phòng" })
                if (dgv.Columns.Contains(c)) dgv.Columns[c].Visible = false;
        }

        // ---------- chọn dòng / làm mới ----------

        void SelectRow()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;

            loading = true;
            editingBed = GetText(r, "Mã giường");
            editingStatus = GetText(r, "Trạng thái");
            txtMa.Text = GetText(r, "Mã NV");
            cmbBN.SelectedValue = GetText(r, "Mã BN");
            if (GetText(r, "Mã khoa") != "") cmbKhoa.SelectedValue = GetText(r, "Mã khoa");
            LoadPhongBS();
            if (GetText(r, "Mã phòng") != "") cmbPhong.SelectedValue = GetText(r, "Mã phòng");
            LoadGiuong();
            if (editingBed != "") cmbGiuong.SelectedValue = editingBed;
            if (GetText(r, "Mã BS") != "") cmbBS.SelectedValue = GetText(r, "Mã BS");

            if (r.Cells["Ngày vào viện"].Value is DateTime) dtpVao.Value = (DateTime)r.Cells["Ngày vào viện"].Value;
            if (r.Cells["Ngày ra viện"].Value is DateTime) dtpRa.Value = (DateTime)r.Cells["Ngày ra viện"].Value;
            else dtpRa.Value = DateTime.Now;
            txtLyDo.Text = GetText(r, "Lý do");
            txtGhiChu.Text = GetText(r, "Ghi chú");
            lblTT.Text = editingStatus;
            loading = false;
        }

        void ResetForm()
        {
            LoadData();
            dgv.CurrentCell = null;
            editingBed = "";
            editingStatus = "";
            txtMa.Text = "";
            txtLyDo.Text = "";
            txtGhiChu.Text = "";
            dtpVao.Value = DateTime.Now;
            dtpRa.Value = DateTime.Now;
            lblTT.Text = "Mới";
            LoadPhongBS();
        }

        // ---------- nghiệp vụ ----------

        void btnTaoHoSo_Click(object s, EventArgs e)
        {
            string bn = GetValue(cmbBN), g = GetValue(cmbGiuong), bs = GetValue(cmbBS);
            if (bn == null) { ShowWarning("Hãy chọn bệnh nhân."); return; }
            if (g == null) { ShowWarning("Hãy chọn giường (phòng này có thể đã hết giường trống)."); return; }
            if (bs == null) { ShowWarning("Hãy chọn bác sĩ phụ trách."); return; }

            try
            {
                if (Convert.ToInt32(DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM NHAPVIEN WHERE MaBN=@bn AND TrangThai=N'Đang điều trị'", DatabaseHelper.Param("@bn", bn))) > 0)
                { ShowWarning("Bệnh nhân này đang điều trị nội trú, không thể tạo thêm hồ sơ nhập viện."); return; }

                string st = Convert.ToString(DatabaseHelper.ExecuteScalar("SELECT TrangThai FROM GIUONG WHERE MaGiuong=@g", DatabaseHelper.Param("@g", g)));
                if (st != "Trống") { ShowWarning("Giường này không còn trống."); return; }

                string ma = DatabaseHelper.NextId("NHAPVIEN", "MaNhapVien", "NV");
                DatabaseHelper.ExecuteTransaction(
                    new SqlStatement(@"INSERT INTO NHAPVIEN(MaNhapVien, MaBN, MaBS, MaGiuong, NgayNhapVien, LyDoNhapVien, TrangThai, GhiChu)
                               VALUES(@ma, @bn, @bs, @g, @vao, @ly, N'Đang điều trị', @gc)",
                        DatabaseHelper.Param("@ma", ma), DatabaseHelper.Param("@bn", bn), DatabaseHelper.Param("@bs", bs), DatabaseHelper.Param("@g", g),
                        DatabaseHelper.Param("@vao", dtpVao.Value), DatabaseHelper.Param("@ly", txtLyDo.Text.Trim()), DatabaseHelper.Param("@gc", txtGhiChu.Text.Trim())),
                    new SqlStatement("UPDATE GIUONG SET TrangThai = N'Đang sử dụng' WHERE MaGiuong = @g", DatabaseHelper.Param("@g", g)));

                ResetForm();
                ShowInfo("Đã tạo hồ sơ nhập viện " + ma + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnCapNhat_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn hồ sơ cần cập nhật trong danh sách."); return; }
            if (editingStatus != "Đang điều trị") { ShowWarning("Chỉ cập nhật được hồ sơ đang điều trị."); return; }
            string g = GetValue(cmbGiuong), bs = GetValue(cmbBS);
            if (g == null || bs == null) { ShowWarning("Hãy chọn đủ giường và bác sĩ phụ trách."); return; }

            try
            {
                var list = new List<SqlStatement>();
                list.Add(new SqlStatement(
                    @"UPDATE NHAPVIEN SET MaBS=@bs, MaGiuong=@g, NgayNhapVien=@vao, LyDoNhapVien=@ly, GhiChu=@gc
                      WHERE MaNhapVien=@ma",
                    DatabaseHelper.Param("@bs", bs), DatabaseHelper.Param("@g", g), DatabaseHelper.Param("@vao", dtpVao.Value),
                    DatabaseHelper.Param("@ly", txtLyDo.Text.Trim()), DatabaseHelper.Param("@gc", txtGhiChu.Text.Trim()), DatabaseHelper.Param("@ma", txtMa.Text)));

                if (g != editingBed)   // chuyển giường: trả giường cũ, chiếm giường mới
                {
                    if (editingBed != "")
                        list.Add(new SqlStatement("UPDATE GIUONG SET TrangThai = N'Trống' WHERE MaGiuong = @o", DatabaseHelper.Param("@o", editingBed)));
                    list.Add(new SqlStatement("UPDATE GIUONG SET TrangThai = N'Đang sử dụng' WHERE MaGiuong = @n", DatabaseHelper.Param("@n", g)));
                }
                DatabaseHelper.ExecuteTransaction(list.ToArray());
                string ma = txtMa.Text;
                ResetForm();
                ShowInfo("Đã cập nhật hồ sơ " + ma + " (bệnh nhân không đổi được; muốn đổi hãy hủy hồ sơ và tạo mới).");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnRaVien_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn hồ sơ cần cho ra viện."); return; }
            
            // Theo PRD BR07: Xuất viện phải qua chức năng Xuất viện riêng để tổng hợp chi phí và thanh toán 100%
            var frm = new XuatVienForm();
            frm.ShowDialog();
            ResetForm();
        }

        void btnHuy_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn hồ sơ cần hủy."); return; }
            if (editingStatus != "Đang điều trị") { ShowWarning("Chỉ hủy được hồ sơ đang điều trị."); return; }
            if (!Ask("Hủy hồ sơ nhập viện " + txtMa.Text + "?")) return;

            try
            {
                DatabaseHelper.ExecuteTransaction(
                    new SqlStatement("UPDATE NHAPVIEN SET TrangThai=N'Hủy' WHERE MaNhapVien=@ma", DatabaseHelper.Param("@ma", txtMa.Text)),
                    new SqlStatement("UPDATE GIUONG SET TrangThai=N'Trống' WHERE MaGiuong=@g", DatabaseHelper.Param("@g", editingBed)));
                ResetForm();
            }
            catch (Exception ex) { ShowError(ex); }
        }
    }
}
