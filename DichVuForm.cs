using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Form quản lý danh mục dịch vụ y tế (chẩn đoán hình ảnh, siêu âm, xét nghiệm, phẫu thuật, ...):
    /// - Quản lý thông tin: Mã dịch vụ, Tên dịch vụ, Đơn giá gốc, Phần trăm Bảo hiểm Y tế (% BHYT) chi trả, và Ghi chú.
    /// - Hỗ trợ đầy đủ chức năng Thêm mới, Sửa thông tin, Xóa và Tìm kiếm tức thời.
    /// </summary>
    public partial class DichVuForm : BaseForm
    {
        /// <summary>
        /// Khởi tạo form Dịch vụ, tải danh sách dịch vụ và gán các sự kiện tương tác UI.
        /// </summary>
        public DichVuForm()
        {
            InitializeComponent();
            LoadData();

            // Khi click chọn dòng trên lưới, nạp dữ liệu chi tiết lên form nhập liệu
            dgv.SelectionChanged += (s, e) => FillInputs();

            // Tìm kiếm tức thời khi gõ từ khóa vào ô tìm kiếm
            txtTim.TextChanged += (s, e) => LoadData();
        }

        /// <summary>
        /// Nạp danh sách dịch vụ từ CSDL lọc theo từ khóa tìm kiếm (theo mã hoặc tên dịch vụ).
        /// </summary>
        void LoadData()
        {
            dgv.DataSource = DatabaseHelper.GetData(
                @"SELECT MaDV AS [Mã DV], TenDV AS [Tên dịch vụ],
                         DonGia AS [Đơn giá], PT_BaoHiem AS [% BH],
                         GhiChu AS [Ghi chú]
                  FROM DICHVU
                  WHERE MaDV LIKE @k OR TenDV LIKE @k
                  ORDER BY MaDV",
                DatabaseHelper.Param("@k", "%" + txtTim.Text.Trim() + "%"));
            FormatColumn(dgv, "Đơn giá", "N0");
        }

        /// <summary>
        /// Đưa thông tin của dòng đang chọn từ DataGridView lên các điều khiển nhập liệu tương ứng.
        /// </summary>
        void FillInputs()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;
            txtMa.Text = GetText(r, "Mã DV");
            txtTen.Text = GetText(r, "Tên dịch vụ");
            txtDonGia.Text = GetText(r, "Đơn giá");
            txtBH.Text = GetText(r, "% BH");
            txtGhiChu.Text = GetText(r, "Ghi chú");
        }

        /// <summary>
        /// Xóa sạch các ô nhập liệu trên form.
        /// </summary>
        void ClearInputs()
        {
            txtMa.Text = txtTen.Text = txtDonGia.Text = txtBH.Text = txtGhiChu.Text = "";
        }

        /// <summary>
        /// Làm mới lại trạng thái form: xóa từ khóa tìm kiếm, tải lại bảng dữ liệu và bỏ chọn.
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
        /// Kiểm tra tính hợp lệ của dữ liệu nhập:
        /// - Tên dịch vụ không được để trống.
        /// - Đơn giá phải là số hợp lệ >= 0.
        /// </summary>
        bool Valid()
        {
            if (txtTen.Text.Trim() == "") { ShowWarning("Vui lòng nhập tên dịch vụ."); txtTen.Focus(); return false; }
            decimal dg;
            if (!decimal.TryParse(txtDonGia.Text, out dg) || dg < 0)
            { ShowWarning("Đơn giá phải là số >= 0."); txtDonGia.Focus(); return false; }
            return true;
        }

        /// <summary>
        /// Tạo mảng tham số SqlParameter phục vụ cho câu lệnh INSERT/UPDATE.
        /// </summary>
        SqlParameter[] Ps(string ma)
        {
            double? bh = null;
            double bhVal;
            if (double.TryParse(txtBH.Text, out bhVal)) bh = bhVal;

            return new[]
            {
                DatabaseHelper.Param("@ma", ma),
                DatabaseHelper.Param("@ten", txtTen.Text.Trim()),
                DatabaseHelper.Param("@gia", decimal.Parse(txtDonGia.Text)),
                DatabaseHelper.Param("@bh", (object)bh ?? DBNull.Value),
                DatabaseHelper.Param("@gc", txtGhiChu.Text.Trim())
            };
        }

        /// <summary>
        /// Thêm dịch vụ mới vào CSDL với mã sinh tự động (DV...).
        /// </summary>
        void btnThem_Click(object s, EventArgs e)
        {
            if (!Valid()) return;
            try
            {
                string ma = DatabaseHelper.NextId("DICHVU", "MaDV", "DV");
                DatabaseHelper.ExecuteNonQuery(
                    "INSERT INTO DICHVU(MaDV, TenDV, DonGia, PT_BaoHiem, GhiChu) VALUES(@ma, @ten, @gia, @bh, @gc)", Ps(ma));
                Reset();
                ShowInfo("Đã thêm dịch vụ " + ma + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        /// <summary>
        /// Cập nhật thông tin dịch vụ đang chọn theo mã dịch vụ.
        /// </summary>
        void btnSua_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn dịch vụ cần sửa."); return; }
            if (!Valid()) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery(
                    "UPDATE DICHVU SET TenDV=@ten, DonGia=@gia, PT_BaoHiem=@bh, GhiChu=@gc WHERE MaDV=@ma", Ps(txtMa.Text));
                LoadData();
                ShowInfo("Đã cập nhật dịch vụ " + txtMa.Text + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        /// <summary>
        /// Xóa dịch vụ khỏi hệ thống. Nếu dịch vụ đã được sử dụng trong lịch điều trị thì chặn xóa.
        /// </summary>
        void btnXoa_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn dịch vụ cần xóa."); return; }
            if (!Ask("Xóa dịch vụ " + txtMa.Text + " - " + txtTen.Text + "?")) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM DICHVU WHERE MaDV=@ma", DatabaseHelper.Param("@ma", txtMa.Text));
                Reset();
            }
            catch (Exception ex)
            {
                ShowError(ex, "Dịch vụ này đang được sử dụng trong lịch điều trị nên không thể xóa.");
            }
        }
    }
}
