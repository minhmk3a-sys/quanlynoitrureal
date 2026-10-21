using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Form Quản lý bác sĩ (BacSiForm):
    /// Quản lý danh sách bác sĩ điều trị trong bệnh viện (Thêm, Sửa, Xóa, Tìm kiếm).
    /// Bác sĩ được liên kết với một Khoa cụ thể thông qua mã khoa (MaKhoa).
    /// </summary>
    public partial class BacSiForm : BaseForm
    {
        public BacSiForm()
        {
            InitializeComponent(); // Dựng giao diện người dùng
            cmbGt.SelectedIndex = 0; // Mặc định giới tính "Nam"

            // Đổ danh sách Khoa từ CSDL vào ComboBox khoa để người dùng chọn khi thêm/sửa bác sĩ
            FillCombo(cmbKhoa, DatabaseHelper.GetData("SELECT MaKhoa, TenKhoa FROM KHOA ORDER BY MaKhoa"), "TenKhoa", "MaKhoa");

            LoadData(); // Nạp danh sách bác sĩ lên bảng DataGridView

            // Gắn sự kiện tương tác người dùng:
            // - Khi chọn một bác sĩ trên bảng -> Nạp thông tin lên các ô nhập
            dgv.SelectionChanged += (s, e) => FillInputs();
            // - Khi gõ từ khóa tìm kiếm -> Lọc dữ liệu ngay lập tức
            txtTim.TextChanged += (s, e) => LoadData();
        }

        /// <summary>
        /// Nút [Làm mới]: Làm trống ô tìm kiếm và ô nhập liệu, đưa form về trạng thái sẵn sàng nhập mới.
        /// </summary>
        void btnLamMoi_Click(object s, EventArgs e)
        {
            Reset();
        }

        /// <summary>
        /// Tải dữ liệu từ bảng BACSI kết hợp với bảng KHOA (LEFT JOIN) để hiển thị tên khoa rõ ràng.
        /// Tìm kiếm theo: Mã BS, Họ tên hoặc Chuyên khoa.
        /// </summary>
        void LoadData()
        {
            string sql = @"
                SELECT b.MaBS AS [Mã BS], b.HoTen AS [Họ tên], b.NgaySinh AS [Ngày sinh], b.GioiTinh AS [Giới tính],
                       b.ChuyenKhoa AS [Chuyên khoa], b.SoDienThoai AS [SĐT], b.Email AS [Email],
                       b.MaKhoa AS [Mã khoa], k.TenKhoa AS [Khoa], b.TrangThai AS [Đang làm việc]
                FROM BACSI b 
                LEFT JOIN KHOA k ON k.MaKhoa = b.MaKhoa
                WHERE b.MaBS LIKE @k OR b.HoTen LIKE @k OR b.ChuyenKhoa LIKE @k
                ORDER BY b.MaBS";

            dgv.DataSource = DatabaseHelper.GetData(sql, DatabaseHelper.Param("@k", "%" + txtTim.Text.Trim() + "%"));
            FormatColumn(dgv, "Ngày sinh", "dd/MM/yyyy");
        }

        /// <summary>
        /// Đọc thông tin của bác sĩ từ dòng đang chọn và hiển thị lên các ô nhập liệu.
        /// </summary>
        void FillInputs()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;

            txtMa.Text = GetText(r, "Mã BS");
            txtTen.Text = GetText(r, "Họ tên");
            if (r.Cells["Ngày sinh"].Value is DateTime) 
                dtpNs.Value = (DateTime)r.Cells["Ngày sinh"].Value;
            cmbGt.Text = GetText(r, "Giới tính");
            txtChuyenKhoa.Text = GetText(r, "Chuyên khoa");
            txtSdt.Text = GetText(r, "SĐT");
            txtEmail.Text = GetText(r, "Email");
            
            // Chọn đúng khoa của bác sĩ trong ComboBox
            if (GetText(r, "Mã khoa") != "") 
                cmbKhoa.SelectedValue = GetText(r, "Mã khoa");

            // Đánh dấu ô CheckBox "Đang làm việc"
            chkLam.Checked = r.Cells["Đang làm việc"].Value is bool && (bool)r.Cells["Đang làm việc"].Value;
        }

        /// <summary>
        /// Xóa trắng các ô nhập liệu.
        /// </summary>
        void ClearInputs()
        {
            txtMa.Text = txtTen.Text = txtChuyenKhoa.Text = txtSdt.Text = txtEmail.Text = "";
            dtpNs.Value = DateTime.Today;
            cmbGt.SelectedIndex = 0;
            chkLam.Checked = true;
        }

        /// <summary>
        /// Đặt lại form về trạng thái ban đầu.
        /// </summary>
        void Reset()
        {
            txtTim.Text = "";
            LoadData();
            dgv.CurrentCell = null;
            ClearInputs();
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu trước khi lưu vào CSDL.
        /// </summary>
        bool Valid()
        {
            if (txtTen.Text.Trim() == "") 
            { 
                ShowWarning("Vui lòng nhập họ tên bác sĩ."); 
                txtTen.Focus(); 
                return false; 
            }
            if (GetValue(cmbKhoa) == null) 
            { 
                ShowWarning("Vui lòng chọn Khoa công tác cho bác sĩ."); 
                return false; 
            }
            return true;
        }

        /// <summary>
        /// Đóng gói các trường nhập liệu thành mảng tham số SQL.
        /// </summary>
        SqlParameter[] Ps(string ma)
        {
            return new[]
            {
                DatabaseHelper.Param("@ma", ma),
                DatabaseHelper.Param("@ten", txtTen.Text.Trim()),
                DatabaseHelper.Param("@ns", dtpNs.Value.Date),
                DatabaseHelper.Param("@gt", cmbGt.Text),
                DatabaseHelper.Param("@ck", txtChuyenKhoa.Text.Trim()),
                DatabaseHelper.Param("@sdt", txtSdt.Text.Trim()),
                DatabaseHelper.Param("@email", txtEmail.Text.Trim()),
                DatabaseHelper.Param("@khoa", GetValue(cmbKhoa)),
                DatabaseHelper.Param("@tt", chkLam.Checked)
            };
        }

        /// <summary>
        /// Xử lý nút [Thêm]: Tự động sinh mã BS (BS01, BS02...) và thêm vào CSDL.
        /// </summary>
        void btnThem_Click(object s, EventArgs e)
        {
            if (!Valid()) return;
            try
            {
                string ma = DatabaseHelper.NextId("BACSI", "MaBS", "BS");
                string sql = @"
                    INSERT INTO BACSI(MaBS, HoTen, NgaySinh, GioiTinh, ChuyenKhoa, SoDienThoai, Email, MaKhoa, TrangThai)
                    VALUES(@ma, @ten, @ns, @gt, @ck, @sdt, @email, @khoa, @tt)";

                DatabaseHelper.ExecuteNonQuery(sql, Ps(ma));
                Reset();
                ShowInfo("Đã thêm mới thành công bác sĩ " + ma + ".");
            }
            catch (Exception ex) 
            { 
                ShowError(ex); 
            }
        }

        /// <summary>
        /// Xử lý nút [Sửa]: Cập nhật thông tin bác sĩ đang chọn.
        /// </summary>
        void btnSua_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") 
            { 
                ShowWarning("Vui lòng chọn bác sĩ cần sửa trong danh sách."); 
                return; 
            }
            if (!Valid()) return;

            try
            {
                string sql = @"
                    UPDATE BACSI 
                    SET HoTen=@ten, NgaySinh=@ns, GioiTinh=@gt, ChuyenKhoa=@ck, SoDienThoai=@sdt,
                        Email=@email, MaKhoa=@khoa, TrangThai=@tt
                    WHERE MaBS=@ma";

                DatabaseHelper.ExecuteNonQuery(sql, Ps(txtMa.Text));
                LoadData();
                ShowInfo("Đã cập nhật thông tin bác sĩ " + txtMa.Text + " thành công.");
            }
            catch (Exception ex) 
            { 
                ShowError(ex); 
            }
        }

        /// <summary>
        /// Xử lý nút [Xóa]: Xóa bác sĩ. 
        /// Nếu bác sĩ này đã từng phụ trách hồ sơ nhập viện -> Không thể xóa (Khóa ngoại),
        /// hướng dẫn người dùng bỏ chọn 'Đang làm việc' thay vì xóa vật lý khỏi CSDL (Soft delete).
        /// </summary>
        void btnXoa_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") 
            { 
                ShowWarning("Vui lòng chọn bác sĩ cần xóa."); 
                return; 
            }
            if (!Ask("Bạn có chắc chắn muốn xóa bác sĩ " + txtMa.Text + " - " + txtTen.Text + "?")) 
                return;

            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM BACSI WHERE MaBS=@ma", DatabaseHelper.Param("@ma", txtMa.Text));
                Reset();
                ShowInfo("Đã xóa bác sĩ thành công.");
            }
            catch (Exception ex)
            {
                ShowError(ex, "Bác sĩ này đã phụ trách hồ sơ bệnh nhân trong hệ thống nên không thể xóa.\n" +
                              "Gợi ý: Hãy bỏ chọn ô 'Đang làm việc' rồi bấm 'Sửa' để chuyển bác sĩ sang trạng thái ngừng công tác.");
            }
        }
    }
}
