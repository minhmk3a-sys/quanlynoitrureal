using QuanLyNoiTru.dotnet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Form Quản lý Giường bệnh nội trú:
    /// - Quản lý thông tin từng giường bệnh trong các phòng điều trị.
    /// - Mỗi giường thuộc về một phòng nhất định, có đơn giá riêng và trạng thái ('Trống' hoặc 'Đang sử dụng').
    /// - Cung cấp đầy đủ các thao tác Thêm mới, Sửa thông tin, Xóa giường và Tìm kiếm theo nhiều điều kiện độc lập (Mã giường, Tên giường, Phòng, Trạng thái).
    /// </summary>
    public partial class GiuongForm : BaseForm
    {
        /// <summary>
        /// Khởi tạo form Giường bệnh, nạp danh sách phòng vào ComboBox và liên kết các sự kiện UI.
        /// </summary>
        public GiuongForm()
        {
            InitializeComponent();
            var dtPhong = DatabaseHelper.GetData("SELECT MaPhong, TenPhong FROM PHONG ORDER BY MaPhong");
            FillCombo(cmbPhong, dtPhong, "TenPhong", "MaPhong");

            // Đổ danh sách phòng vào ComboBox tìm kiếm kèm '-- Tất cả --'
            var dtTimPhong = dtPhong.Copy();
            var rAll = dtTimPhong.NewRow();
            rAll["MaPhong"] = "";
            rAll["TenPhong"] = "-- Tất cả --";
            dtTimPhong.Rows.InsertAt(rAll, 0);
            FillCombo(cmbTimPhong, dtTimPhong, "TenPhong", "MaPhong");
            cmbTimTT.SelectedIndex = 0; // Mặc định trạng thái: '-- Tất cả --'

            LoadData();

            // Khi click chọn dòng trên lưới -> đổ thông tin lên các ô nhập liệu
            dgv.SelectionChanged += (s, e) => FillInputs();

            // Nhấn Enter trong các ô tìm kiếm để thực hiện tìm kiếm
            KeyEventHandler onEnter = (s, e) => { if (e.KeyCode == Keys.Enter) LoadData(); };
            txtTimMa.KeyDown += onEnter;
            txtTimTen.KeyDown += onEnter;
        }

        /// <summary>
        /// Nút [🔍 Tìm kiếm]: Thực hiện lọc danh sách giường bệnh theo điều kiện đã nhập.
        /// </summary>
        void btnTimKiem_Click(object s, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Nút [🔄 Làm mới] ở thanh tìm kiếm: Xóa trắng các ô tìm kiếm và nạp lại toàn bộ danh sách giường.
        /// </summary>
        void btnLamMoiTim_Click(object s, EventArgs e)
        {
            txtTimMa.Text = "";
            txtTimTen.Text = "";
            if (cmbTimPhong.Items.Count > 0) cmbTimPhong.SelectedIndex = 0;
            if (cmbTimTT.Items.Count > 0) cmbTimTT.SelectedIndex = 0;
            LoadData();
        }

        /// <summary>
        /// Nạp danh sách giường bệnh từ CSDL theo nhiều điều kiện kết hợp (Mã giường, Tên giường, Phòng, Trạng thái).
        /// </summary>
        void LoadData()
        {
            string sql = @"
                SELECT g.MaGiuong AS [Mã giường], g.TenGiuong AS [Tên giường],
                       g.MaPhong AS [Mã phòng], p.TenPhong AS [Phòng],
                       g.DonGia AS [Đơn giá],
                       g.TrangThai AS [Trạng thái]
                FROM GIUONG g LEFT JOIN PHONG p ON p.MaPhong = g.MaPhong
                WHERE 1=1 ";

            var ps = new List<SqlParameter>();
            string ma = txtTimMa.Text.Trim();
            if (!string.IsNullOrEmpty(ma))
            {
                sql += " AND g.MaGiuong LIKE @timMa ";
                ps.Add(DatabaseHelper.Param("@timMa", "%" + ma + "%"));
            }
            string ten = txtTimTen.Text.Trim();
            if (!string.IsNullOrEmpty(ten))
            {
                sql += " AND g.TenGiuong LIKE @timTen ";
                ps.Add(DatabaseHelper.Param("@timTen", "%" + ten + "%"));
            }
            string maPhong = GetValue(cmbTimPhong);
            if (!string.IsNullOrEmpty(maPhong))
            {
                sql += " AND g.MaPhong = @timPhong ";
                ps.Add(DatabaseHelper.Param("@timPhong", maPhong));
            }
            if (cmbTimTT.SelectedIndex > 0 && cmbTimTT.Text != "-- Tất cả --")
            {
                sql += " AND g.TrangThai = @timTT ";
                ps.Add(DatabaseHelper.Param("@timTT", cmbTimTT.Text));
            }

            sql += " ORDER BY g.MaGiuong";

            dgv.DataSource = DatabaseHelper.GetData(sql, ps.ToArray());
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
            txtTimMa.Text = "";
            txtTimTen.Text = "";
            if (cmbTimPhong.Items.Count > 0) cmbTimPhong.SelectedIndex = 0;
            if (cmbTimTT.Items.Count > 0) cmbTimTT.SelectedIndex = 0;
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
            if (txtTen.Text.Trim() == "") { ShowWarning("Vui lòng nhập tên/số giường."); txtTen.Focus(); return false; }
            if (GetValue(cmbPhong) == null) { ShowWarning("Vui lòng chọn phòng."); return false; }
            decimal dg;
            if (!decimal.TryParse(txtDonGia.Text, out dg) || dg < 0)
            { ShowWarning("Đơn giá phải là số >= 0."); txtDonGia.Focus(); return false; }
            return true;
        }

        /// <summary>
        /// Đóng gói dữ liệu thành mảng tham số SQL.
        /// </summary>
        SqlParameter[] Ps(string ma)
        {
            return new[]
            {
                DatabaseHelper.Param("@ma", ma),
                DatabaseHelper.Param("@ten", txtTen.Text.Trim()),
                DatabaseHelper.Param("@p", GetValue(cmbPhong)),
                DatabaseHelper.Param("@gia", decimal.Parse(txtDonGia.Text))
            };
        }

        /// <summary>
        /// Thêm giường mới với mã sinh tự động (G...). Trạng thái mặc định ban đầu là 'Trống'.
        /// </summary>
        void btnThem_Click(object s, EventArgs e)
        {
            if (!Valid()) return;
            try
            {
                string ma = DatabaseHelper.NextId("GIUONG", "MaGiuong", "G");
                DatabaseHelper.ExecuteNonQuery("INSERT INTO GIUONG(MaGiuong, TenGiuong, MaPhong, DonGia, TrangThai) VALUES(@ma, @ten, @p, @gia, N'Trống')", Ps(ma));
                Reset();
                ShowInfo("Đã thêm giường " + ma + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        /// <summary>
        /// Cập nhật thông tin giường (tên giường, chuyển phòng, đơn giá).
        /// Không cho phép sửa nếu giường đang có bệnh nhân nằm điều trị.
        /// </summary>
        void btnSua_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn giường cần sửa."); return; }
            if (!Valid()) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery("UPDATE GIUONG SET TenGiuong=@ten, MaPhong=@p, DonGia=@gia WHERE MaGiuong=@ma", Ps(txtMa.Text));
                LoadData();
                ShowInfo("Đã cập nhật giường " + txtMa.Text + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        /// <summary>
        /// Xóa giường bệnh. Hệ thống sẽ chặn nếu giường đang được sử dụng hoặc có lịch sử điều trị.
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
                ShowError(ex, "Giường này đang có bệnh nhân nằm hoặc đã có lịch sử nhập viện, không thể xóa.");
            }
        }
    }
}
