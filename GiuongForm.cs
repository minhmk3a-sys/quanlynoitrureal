using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Quản lý giường: thêm / sửa / xóa / tìm kiếm.
    /// Mỗi giường thuộc một phòng, có đơn giá riêng và trạng thái (trống / đang sử dụng).
    /// </summary>
    public partial class GiuongForm : BaseForm
    {
        public GiuongForm()
        {
            InitializeComponent();
            FillCombo(cmbPhong, DatabaseHelper.GetData("SELECT MaPhong, TenPhong FROM PHONG ORDER BY MaPhong"), "TenPhong", "MaPhong");
            LoadData();
            dgv.SelectionChanged += (s, e) => FillInputs();
            txtTim.TextChanged += (s, e) => LoadData();
        }

        void LoadData()
        {
            dgv.DataSource = DatabaseHelper.GetData(
                @"SELECT g.MaGiuong AS [Mã giường], g.TenGiuong AS [Tên giường],
                         g.MaPhong AS [Mã phòng], p.TenPhong AS [Phòng],
                         g.DonGia AS [Đơn giá],
                         g.TrangThai AS [Trạng thái]
                  FROM GIUONG g LEFT JOIN PHONG p ON p.MaPhong = g.MaPhong
                  WHERE g.MaGiuong LIKE @k OR g.TenGiuong LIKE @k OR p.TenPhong LIKE @k
                  ORDER BY g.MaGiuong",
                DatabaseHelper.Param("@k", "%" + txtTim.Text.Trim() + "%"));
            FormatColumn(dgv, "Đơn giá", "N0");
        }

        void FillInputs()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;
            txtMa.Text = GetText(r, "Mã giường");
            txtTen.Text = GetText(r, "Tên giường");
            if (GetText(r, "Mã phòng") != "") cmbPhong.SelectedValue = GetText(r, "Mã phòng");
            txtDonGia.Text = GetText(r, "Đơn giá");
        }

        void ClearInputs()
        {
            txtMa.Text = txtTen.Text = txtDonGia.Text = "";
        }

        void Reset()
        {
            txtTim.Text = "";
            LoadData();
            dgv.CurrentCell = null;
            ClearInputs();
        }

        void btnLamMoi_Click(object s, EventArgs e) { Reset(); }

        bool Valid()
        {
            if (txtTen.Text.Trim() == "") { ShowWarning("Vui lòng nhập tên giường."); txtTen.Focus(); return false; }
            if (GetValue(cmbPhong) == null) { ShowWarning("Vui lòng chọn phòng."); return false; }
            decimal dg;
            if (!decimal.TryParse(txtDonGia.Text, out dg) || dg < 0)
            { ShowWarning("Đơn giá phải là số >= 0."); txtDonGia.Focus(); return false; }
            return true;
        }

        SqlParameter[] Ps(string ma)
        {
            return new[]
            {
                DatabaseHelper.Param("@ma", ma),
                DatabaseHelper.Param("@ten", txtTen.Text.Trim()),
                DatabaseHelper.Param("@phong", GetValue(cmbPhong)),
                DatabaseHelper.Param("@gia", decimal.Parse(txtDonGia.Text))
            };
        }

        void btnThem_Click(object s, EventArgs e)
        {
            if (!Valid()) return;
            try
            {
                string ma = DatabaseHelper.NextId("GIUONG", "MaGiuong", "GI");
                DatabaseHelper.ExecuteNonQuery(
                    "INSERT INTO GIUONG(MaGiuong, TenGiuong, MaPhong, DonGia, TrangThai, SoGiuong) VALUES(@ma, @ten, @phong, @gia, N'Trống', 1)", Ps(ma));
                Reset();
                ShowInfo("Đã thêm giường " + ma + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnSua_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn giường cần sửa."); return; }
            if (!Valid()) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery(
                    "UPDATE GIUONG SET TenGiuong=@ten, MaPhong=@phong, DonGia=@gia WHERE MaGiuong=@ma", Ps(txtMa.Text));
                LoadData();
                ShowInfo("Đã cập nhật giường " + txtMa.Text + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnXoa_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn giường cần xóa."); return; }
            if (!Ask("Xóa giường " + txtMa.Text + " - " + txtTen.Text + "?")) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM GIUONG WHERE MaGiuong=@ma", DatabaseHelper.Param("@ma", txtMa.Text));
                Reset();
            }
            catch (Exception ex)
            {
                ShowError(ex, "Giường này đang có bệnh nhân sử dụng nên không thể xóa.");
            }
        }
    }
}
