using QuanLyNoiTru.dotnet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Form Quản lý Khoa (KhoaForm):
    /// Quản lý danh mục các chuyên khoa trong bệnh viện (Cấp cứu, Nội tim mạch, Ngoại chấn thương...).
    /// Khoa là đơn vị quản lý cấp cao nhất cho các Phòng bệnh và Bác sĩ.
    /// Kế thừa BaseForm để tái sử dụng các hàm tiện ích đồ họa và xử lý lỗi SQL.
    /// </summary>
    public partial class KhoaForm : BaseForm
    {
        public KhoaForm()
        {
            InitializeComponent(); // Dựng giao diện
            LoadData(); // Nạp danh sách khoa lên bảng

            // Đăng ký sự kiện người dùng:
            // - Bấm vào 1 dòng trong bảng -> Điền thông tin lên các ô nhập
            dgv.SelectionChanged += (s, e) => FillInputs();

            // Nhấn Enter trong ô tìm kiếm để thực hiện tìm kiếm
            KeyEventHandler onEnter = (s, e) => { if (e.KeyCode == Keys.Enter) LoadData(); };
            txtTimMa.KeyDown += onEnter;
            txtTimTen.KeyDown += onEnter;
        }

        /// <summary>
        /// Nút [🔍 Tìm kiếm]: Thực hiện lọc danh sách khoa theo điều kiện đã nhập.
        /// </summary>
        void btnTimKiem_Click(object s, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Nút [🔄 Làm mới] ở thanh tìm kiếm: Xóa trắng các ô tìm kiếm và nạp lại toàn bộ danh sách khoa.
        /// </summary>
        void btnLamMoiTim_Click(object s, EventArgs e)
        {
            txtTimMa.Text = "";
            txtTimTen.Text = "";
            LoadData();
        }

        /// <summary>
        /// Đọc toàn bộ danh sách khoa từ bảng KHOA theo nhiều điều kiện tìm kiếm kết hợp (Mã khoa và Tên khoa).
        /// </summary>
        void LoadData()
        {
            string sql = @"
                SELECT MaKhoa AS [Mã khoa], TenKhoa AS [Tên khoa],
                       CASE WHEN TrangThai = 1 THEN N'Hoạt động' ELSE N'Ngừng' END AS [Trạng thái],
                       GhiChu AS [Ghi chú]
                FROM KHOA
                WHERE 1=1 ";

            var ps = new List<SqlParameter>();
            string ma = txtTimMa.Text.Trim();
            if (!string.IsNullOrEmpty(ma))
            {
                sql += " AND MaKhoa LIKE @timMa ";
                ps.Add(DatabaseHelper.Param("@timMa", "%" + ma + "%"));
            }
            string ten = txtTimTen.Text.Trim();
            if (!string.IsNullOrEmpty(ten))
            {
                sql += " AND TenKhoa LIKE @timTen ";
                ps.Add(DatabaseHelper.Param("@timTen", "%" + ten + "%"));
            }
            sql += " ORDER BY MaKhoa";

            dgv.DataSource = DatabaseHelper.GetData(sql, ps.ToArray());
        }

        /// <summary>
        /// Điền dữ liệu của dòng đang chọn trong DataGridView lên các điều khiển nhập liệu phía trên.
        /// </summary>
        void FillInputs()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;

            txtMa.Text = GetText(r, "Mã khoa");
            txtTen.Text = GetText(r, "Tên khoa");
            chkTrangThai.Checked = (GetText(r, "Trạng thái") == "Hoạt động");
            txtGhiChu.Text = GetText(r, "Ghi chú");
        }

        /// <summary>
        /// Xóa trắng các ô nhập liệu, chuẩn bị thêm mới.
        /// </summary>
        void ClearInputs()
        {
            txtMa.Text = txtTen.Text = txtGhiChu.Text = "";
            chkTrangThai.Checked = true;
        }

        /// <summary>
        /// Đặt lại form về trạng thái ban đầu: xóa ô tìm kiếm, nạp lại toàn bộ bảng.
        /// </summary>
        void Reset()
        {
            txtTimMa.Text = "";
            txtTimTen.Text = "";
            LoadData();
            dgv.CurrentCell = null;
            ClearInputs();
        }

        void btnLamMoi_Click(object s, EventArgs e) 
        { 
            Reset(); 
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu nhập (Tên khoa không được để trống).
        /// </summary>
        bool Valid()
        {
            if (txtTen.Text.Trim() == "") 
            { 
                ShowWarning("Vui lòng nhập tên khoa."); 
                txtTen.Focus(); 
                return false; 
            }
            return true;
        }

        /// <summary>
        /// Đóng gói dữ liệu thành mảng tham số SQL an toàn.
        /// </summary>
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

        /// <summary>
        /// Xử lý nút [Thêm]: Tự động sinh mã KHxx (KH01, KH02...) và thêm mới vào CSDL.
        /// </summary>
        void btnThem_Click(object s, EventArgs e)
        {
            if (!Valid()) return;
            try
            {
                string ma = DatabaseHelper.NextId("KHOA", "MaKhoa", "KH");
                string sql = "INSERT INTO KHOA(MaKhoa, TenKhoa, TrangThai, GhiChu) VALUES(@ma, @ten, @tt, @gc)";
                DatabaseHelper.ExecuteNonQuery(sql, Ps(ma));
                Reset();
                ShowInfo("Đã thêm mới thành công khoa " + ma + ".");
            }
            catch (Exception ex) 
            { 
                ShowError(ex); 
            }
        }

        /// <summary>
        /// Xử lý nút [Sửa]: Cập nhật tên khoa, trạng thái hoạt động và ghi chú của khoa đang chọn.
        /// </summary>
        void btnSua_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") 
            { 
                ShowWarning("Vui lòng chọn khoa cần sửa trong danh sách."); 
                return; 
            }
            if (!Valid()) return;

            try
            {
                string sql = "UPDATE KHOA SET TenKhoa=@ten, TrangThai=@tt, GhiChu=@gc WHERE MaKhoa=@ma";
                DatabaseHelper.ExecuteNonQuery(sql, Ps(txtMa.Text));
                LoadData();
                ShowInfo("Đã cập nhật thông tin khoa " + txtMa.Text + " thành công.");
            }
            catch (Exception ex) 
            { 
                ShowError(ex); 
            }
        }

        /// <summary>
        /// Xử lý nút [Xóa]: Xóa khoa khỏi hệ thống.
        /// Cơ chế bảo vệ dữ liệu: Không cho xóa nếu khoa này đang có phòng bệnh hoặc bác sĩ trực thuộc.
        /// </summary>
        void btnXoa_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") 
            { 
                ShowWarning("Vui lòng chọn khoa cần xóa."); 
                return; 
            }
            if (!Ask("Bạn có chắc chắn muốn xóa khoa " + txtMa.Text + " - " + txtTen.Text + "?")) 
                return;

            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM KHOA WHERE MaKhoa=@ma", DatabaseHelper.Param("@ma", txtMa.Text));
                Reset();
                ShowInfo("Đã xóa khoa thành công.");
            }
            catch (Exception ex)
            {
                ShowError(ex, "Khoa này đang có phòng bệnh hoặc bác sĩ trực thuộc nên không thể xóa.\n" +
                              "Gợi ý: Hãy bỏ chọn ô 'Hoạt động' rồi bấm 'Sửa' để chuyển khoa sang trạng thái ngừng hoạt động.");
            }
        }
    }
}
