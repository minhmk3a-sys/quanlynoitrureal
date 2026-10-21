using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Quản lý phòng: thêm / sửa / xóa / tìm kiếm.
    /// Kế thừa BaseForm để dùng các hàm tiện ích chung.
    /// </summary>
    public partial class PhongForm : BaseForm
    {
        public PhongForm()
        {
            InitializeComponent();
            // Đổ danh sách khoa vào ComboBox
            FillCombo(cmbKhoa, DatabaseHelper.GetData("SELECT MaKhoa, TenKhoa FROM KHOA WHERE TrangThai=1 ORDER BY MaKhoa"), "TenKhoa", "MaKhoa");
            LoadData();
            dgv.SelectionChanged += (s, e) => FillInputs();
            txtTim.TextChanged += (s, e) => LoadData();
        }

        void LoadData()
        {
            dgv.DataSource = DatabaseHelper.GetData(
                @"SELECT p.MaPhong AS [Mã phòng], p.TenPhong AS [Tên phòng],
                         p.MaKhoa AS [Mã khoa], k.TenKhoa AS [Khoa]
                  FROM PHONG p LEFT JOIN KHOA k ON k.MaKhoa = p.MaKhoa
                  WHERE p.MaPhong LIKE @k OR p.TenPhong LIKE @k OR k.TenKhoa LIKE @k
                  ORDER BY p.MaPhong",
                DatabaseHelper.Param("@k", "%" + txtTim.Text.Trim() + "%"));
        }

        void FillInputs()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;
            txtMa.Text = GetText(r, "Mã phòng");
            txtTen.Text = GetText(r, "Tên phòng");
            if (GetText(r, "Mã khoa") != "") cmbKhoa.SelectedValue = GetText(r, "Mã khoa");
        }

        void ClearInputs()
        {
            txtMa.Text = txtTen.Text = "";
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
            if (txtTen.Text.Trim() == "") { ShowWarning("Vui lòng nhập tên phòng."); txtTen.Focus(); return false; }
            if (GetValue(cmbKhoa) == null) { ShowWarning("Vui lòng chọn khoa."); return false; }
            return true;
        }

        SqlParameter[] Ps(string ma)
        {
            return new[]
            {
                DatabaseHelper.Param("@ma", ma),
                DatabaseHelper.Param("@ten", txtTen.Text.Trim()),
                DatabaseHelper.Param("@khoa", GetValue(cmbKhoa))
            };
        }

        void btnThem_Click(object s, EventArgs e)
        {
            if (!Valid()) return;
            try
            {
                string ma = DatabaseHelper.NextId("PHONG", "MaPhong", "PH");
                DatabaseHelper.ExecuteNonQuery(
                    "INSERT INTO PHONG(MaPhong, TenPhong, MaKhoa) VALUES(@ma, @ten, @khoa)", Ps(ma));
                Reset();
                ShowInfo("Đã thêm phòng " + ma + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnSua_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn phòng cần sửa."); return; }
            if (!Valid()) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery(
                    "UPDATE PHONG SET TenPhong=@ten, MaKhoa=@khoa WHERE MaPhong=@ma", Ps(txtMa.Text));
                LoadData();
                ShowInfo("Đã cập nhật phòng " + txtMa.Text + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnXoa_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn phòng cần xóa."); return; }
            if (!Ask("Xóa phòng " + txtMa.Text + " - " + txtTen.Text + "?")) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM PHONG WHERE MaPhong=@ma", DatabaseHelper.Param("@ma", txtMa.Text));
                Reset();
            }
            catch (Exception ex)
            {
                ShowError(ex, "Phòng này đang có giường nên không thể xóa.\nHãy xóa các giường trong phòng trước.");
            }
        }
    }
}
