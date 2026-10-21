using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Form Quản lý Giường bệnh nội trú:
    /// - Quản lý thông tin từng giường bệnh trong các phòng điều trị.
    /// - Mỗi giường thuộc về một phòng nhất định, có đơn giá riêng và trạng thái ('Trống' hoặc 'Đang sử dụng').
    /// - Cung cấp đầy đủ các thao tác Thêm mới, Sửa thông tin, Xóa giường và Tìm kiếm theo tên giường, phòng hoặc mã giường.
    /// </summary>
    public partial class GiuongForm : BaseForm
    {
        /// <summary>
        /// Khởi tạo form Giường bệnh, nạp danh sách phòng vào ComboBox và liên kết các sự kiện UI.
        /// </summary>
        public GiuongForm()
        {
            InitializeComponent();
            FillCombo(cmbPhong, DatabaseHelper.GetData("SELECT MaPhong, TenPhong FROM PHONG ORDER BY MaPhong"), "TenPhong", "MaPhong");
            LoadData();

            // Khi click chọn dòng trên lưới -> đổ thông tin lên các ô nhập liệu
            dgv.SelectionChanged += (s, e) => FillInputs();

            // Tìm kiếm tức thời khi nhập ký tự vào ô tìm kiếm
            txtTim.TextChanged += (s, e) => LoadData();
        }

        /// <summary>
        /// Nạp danh sách giường bệnh từ CSDL kèm tên phòng tương ứng, lọc theo từ khóa tìm kiếm.
        /// </summary>
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

        /// <summary>
        /// Điền dữ liệu từ dòng được chọn trong DataGridView lên các ô nhập liệu.
        /// </summary>
        void FillInputs()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;
            txtMa.Text = GetText(r, "Mã giường");
            txtTen.Text = GetText(r, "Tên giường");
            if (GetText(r, "Mã phòng") != "") cmbPhong.SelectedValue = GetText(r, "Mã phòng");
            txtDonGia.Text = GetText(r, "Đơn giá");
        }

        /// <summary>
        /// Xóa sạch dữ liệu trên các ô nhập liệu.
        /// </summary>
        void ClearInputs()
        {
            txtMa.Text = txtTen.Text = txtDonGia.Text = "";
        }

        /// <summary>
        /// Đặt lại form về trạng thái ban đầu: xóa ô tìm kiếm, tải lại bảng và bỏ chọn.
        /// </summary>
        void Reset()
        {
            txtTim.Text = "";
            LoadData();
            dgv.CurrentCell = null;
            ClearInputs();
        }

        /// <summary>
        /// Nút Làm mới: gọi hàm Reset().
        /// </summary>
        void btnLamMoi_Click(object s, EventArgs e) { Reset(); }

        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu giường bệnh:
        /// - Tên giường không được để trống.
        /// - Phải chọn phòng cho giường.
        /// - Đơn giá phải là số >= 0.
        /// </summary>
        bool Valid()
        {
            if (txtTen.Text.Trim() == "") { ShowWarning("Vui lòng nhập tên giường."); txtTen.Focus(); return false; }
            if (GetValue(cmbPhong) == null) { ShowWarning("Vui lòng chọn phòng."); return false; }
            decimal dg;
            if (!decimal.TryParse(txtDonGia.Text, out dg) || dg < 0)
            { ShowWarning("Đơn giá phải là số >= 0."); txtDonGia.Focus(); return false; }
            return true;
        }

        /// <summary>
        /// Tạo mảng tham số SqlParameter cho câu lệnh SQL thêm/sửa giường.
        /// </summary>
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

        /// <summary>
        /// Thêm giường mới vào phòng được chọn:
        /// - Sinh mã giường tự động (GI...).
        /// - Mặc định trạng thái ban đầu là 'Trống'.
        /// </summary>
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

        /// <summary>
        /// Cập nhật thông tin giường (tên giường, phòng, đơn giá).
        /// </summary>
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

        /// <summary>
        /// Xóa giường khỏi hệ thống. Nếu giường đang có bệnh nhân nằm điều trị thì CSDL sẽ chặn xóa để đảm bảo toàn vẹn.
        /// </summary>
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
