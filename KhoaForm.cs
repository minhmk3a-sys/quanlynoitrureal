using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Quản lý khoa: thêm / sửa / xóa / tìm kiếm.
    /// Kế thừa BaseForm để dùng các hàm tiện ích chung.
    /// </summary>
    public partial class KhoaForm : BaseForm
    {
        public KhoaForm()
        {
            InitializeComponent();
            LoadData();
            dgv.SelectionChanged += (s, e) => FillInputs();
            txtTim.TextChanged += (s, e) => LoadData();
        }

        // --- Tải dữ liệu lên bảng ---
        void LoadData()
        {
            dgv.DataSource = DatabaseHelper.GetData(
                @"SELECT MaKhoa AS [Mã khoa], TenKhoa AS [Tên khoa],
                         CASE WHEN TrangThai = 1 THEN N'Hoạt động' ELSE N'Ngừng' END AS [Trạng thái],
                         GhiChu AS [Ghi chú]
                  FROM KHOA
                  WHERE MaKhoa LIKE @k OR TenKhoa LIKE @k
                  ORDER BY MaKhoa",
                DatabaseHelper.Param("@k", "%" + txtTim.Text.Trim() + "%"));
        }

        // --- Khi bấm vào một dòng, điền thông tin lên ô nhập ---
        void FillInputs()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;
            txtMa.Text = GetText(r, "Mã khoa");
            txtTen.Text = GetText(r, "Tên khoa");
            chkTrangThai.Checked = GetText(r, "Trạng thái") == "Hoạt động";
            txtGhiChu.Text = GetText(r, "Ghi chú");
        }

        // --- Xoá trắng các ô nhập ---
        void ClearInputs()
        {
            txtMa.Text = txtTen.Text = txtGhiChu.Text = "";
            chkTrangThai.Checked = true;
        }

        void Reset()
        {
            txtTim.Text = "";
            LoadData();
            dgv.CurrentCell = null;
            ClearInputs();
        }

        void btnLamMoi_Click(object s, EventArgs e) { Reset(); }

        // --- Kiểm tra dữ liệu nhập ---
        bool Valid()
        {
            if (txtTen.Text.Trim() == "") { ShowWarning("Vui lòng nhập tên khoa."); txtTen.Focus(); return false; }
            return true;
        }

        // --- Tạo mảng tham số SQL ---
        SqlParameter[] Ps(string ma)
        {
            return new[]
            {
                DatabaseHelper.Param("@ma", ma),
                DatabaseHelper.Param("@ten", txtTen.Text.Trim()),
                DatabaseHelper.Param("@tt", chkTrangThai.Checked),
                DatabaseHelper.Param("@gc", txtGhiChu.Text.Trim())
            };
        }

        // --- Thêm ---
        void btnThem_Click(object s, EventArgs e)
        {
            if (!Valid()) return;
            try
            {
                string ma = DatabaseHelper.NextId("KHOA", "MaKhoa", "KH");
                DatabaseHelper.ExecuteNonQuery(
                    "INSERT INTO KHOA(MaKhoa, TenKhoa, TrangThai, GhiChu) VALUES(@ma, @ten, @tt, @gc)", Ps(ma));
                Reset();
                ShowInfo("Đã thêm khoa " + ma + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        // --- Sửa ---
        void btnSua_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn khoa cần sửa."); return; }
            if (!Valid()) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery(
                    "UPDATE KHOA SET TenKhoa=@ten, TrangThai=@tt, GhiChu=@gc WHERE MaKhoa=@ma", Ps(txtMa.Text));
                LoadData();
                ShowInfo("Đã cập nhật khoa " + txtMa.Text + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        // --- Xóa ---
        void btnXoa_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn khoa cần xóa."); return; }
            if (!Ask("Xóa khoa " + txtMa.Text + " - " + txtTen.Text + "?")) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM KHOA WHERE MaKhoa=@ma", DatabaseHelper.Param("@ma", txtMa.Text));
                Reset();
            }
            catch (Exception ex)
            {
                ShowError(ex, "Khoa này đang có phòng hoặc bác sĩ nên không thể xóa.\nHãy bỏ chọn \"Hoạt động\" rồi bấm Sửa.");
            }
        }
    }
}
