using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Form Quản lý bệnh nhân (BenhNhanForm): 
    /// Đảm nhiệm chức năng quản lý thông tin hành chính của bệnh nhân (Thêm, Sửa, Xóa, Tìm kiếm).
    /// Kế thừa từ BaseForm để sử dụng các hàm tiện ích chung (ShowInfo, ShowWarning, ShowError, GetText...).
    /// 
    /// ====================================================================================================
    /// 📚 HƯỚNG DẪN CỐT LÕI CHO NGƯỜI MỚI HỌC C# VỀ CHỨC NĂNG THÊM - SỬA - XÓA - TÌM KIẾM (CRUD)
    /// ====================================================================================================
    /// 1. TÌM KIẾM (Search):
    ///    - Bắt sự kiện 'txtTim.TextChanged' (khi người dùng gõ bất kỳ ký tự nào vào ô tìm kiếm).
    ///    - Gọi hàm 'LoadData()'. Trong hàm này dùng câu lệnh:
    ///         SELECT ... WHERE MaBN LIKE @k OR HoTen LIKE @k ...
    ///         với tham số @k = "%" + txtTim.Text.Trim() + "%"
    ///    - Dấu '%' đại diện cho chuỗi ký tự bất kỳ, giúp tìm kiếm tương đối ở đầu, giữa hoặc cuối chuỗi.
    /// 
    /// 2. HIỂN THỊ CHI TIẾT KHI CHỌN DÒNG (SelectionChanged):
    ///    - Khi nhấp chuột vào một dòng trên DataGridView -> kích hoạt sự kiện 'dgv.SelectionChanged'.
    ///    - Gọi hàm 'FillInputs()': Lấy dữ liệu từng cột của dòng được chọn bằng hàm tiện ích 'GetText(r, "Tên cột")'
    ///      và gán vào các ô nhập liệu (TextBox: txtTen.Text = ..., DateTimePicker: dtpNs.Value = ...).
    /// 
    /// 3. THÊM MỚI (Insert):
    ///    - Bước 3.1: Gọi hàm 'Valid()' để kiểm tra người dùng có nhập đủ và đúng định dạng không (VD: Tên không rỗng, CCCD đủ 12 số).
    ///    - Bước 3.2: Tự động sinh mã tiếp theo qua 'DatabaseHelper.NextId("BENHNHAN", "MaBN", "BN")' -> ra "BN01", "BN02"...
    ///    - Bước 3.3: Đóng gói các giá trị từ giao diện thành mảng tham số 'Ps(ma)' để chống hack SQL Injection.
    ///    - Bước 3.4: Gọi 'DatabaseHelper.ExecuteNonQuery(sql, Ps(ma))' để ghi vào CSDL SQL Server.
    ///    - Bước 3.5: Gọi 'Reset()' để tải lại bảng và xóa trắng ô nhập liệu, thông báo thành công.
    /// 
    /// 4. CHỈNH SỬA (Update):
    ///    - Bước 4.1: Kiểm tra ô 'txtMa.Text' xem người dùng đã nhấp chọn bệnh nhân cần sửa trên bảng chưa.
    ///    - Bước 4.2: Kiểm tra tính hợp lệ dữ liệu qua 'Valid()'.
    ///    - Bước 4.3: Gọi câu lệnh SQL: UPDATE BENHNHAN SET HoTen=@ten, ... WHERE MaBN=@ma.
    ///    - Bước 4.4: Gọi 'LoadData()' để làm mới lại lưới với thông tin mới nhất.
    /// 
    /// 5. XÓA (Delete):
    ///    - Bước 5.1: Kiểm tra đã chọn bệnh nhân cần xóa chưa.
    ///    - Bước 5.2: Hiển thị hộp thoại hỏi lại 'Ask("Bạn có chắc chắn muốn xóa...?")' để chống bấm nhầm.
    ///    - Bước 5.3: Thực thi 'DELETE FROM BENHNHAN WHERE MaBN=@ma'.
    ///    - Bước 5.4: Nếu bệnh nhân này đã có dữ liệu ở bảng khác (nhập viện, viện phí...) -> CSDL sẽ chặn lại
    ///      bằng ràng buộc Khóa Ngoại (Foreign Key - Mã lỗi 547). Hàm 'ShowError(ex, "...")' sẽ tự động bắt
    ///      lỗi này và hiện câu nhắc tiếng Việt rõ ràng, ngăn chương trình không bị văng lỗi (crash).
    /// 
    /// ====================================================================================================
    /// 🛠️ HƯỚNG DẪN 8 BƯỚC ĐỂ THÊM HOẶC BỚT 1 THÀNH PHẦN (TRƯỜNG/CỘT DỮ LIỆU) MỚI:
    /// Giả sử bạn muốn THÊM trường "Email" cho bệnh nhân:
    ///   - Bước 1 (Database): Chạy lệnh SQL: ALTER TABLE BENHNHAN ADD Email NVARCHAR(100) NULL;
    ///   - Bước 2 (Designer): Mở BenhNhanForm.Designer.cs (hoặc kéo thả trong Visual Studio), thêm TextBox 'txtEmail' và Label.
    ///   - Bước 3 (LoadData): Trong câu SELECT, thêm cột: bn.Email AS [Email].
    ///   - Bước 4 (FillInputs): Thêm dòng: txtEmail.Text = GetText(r, "Email");
    ///   - Bước 5 (ClearInputs): Thêm dòng: txtEmail.Text = "";
    ///   - Bước 6 (Valid): Nếu muốn kiểm tra định dạng email (phải có ký tự '@'), viết thêm kiểm tra vào hàm Valid().
    ///   - Bước 7 (Ps): Thêm tham số: DatabaseHelper.Param("@email", txtEmail.Text.Trim()) vào mảng tham số.
    ///   - Bước 8 (Thêm & Sửa):
    ///        + Trong btnThem_Click: Thêm cột Email vào danh sách cột và @email vào VALUES(...) của câu lệnh INSERT.
    ///        + Trong btnSua_Click: Thêm 'Email=@email' vào phần SET của câu lệnh UPDATE.
    /// Khi muốn BỚT (XÓA) một trường dữ liệu: Bạn chỉ cần làm ngược lại toàn bộ 8 bước trên.
    /// ====================================================================================================
    /// </summary>
    public partial class BenhNhanForm : BaseForm
    {
        public BenhNhanForm()
        {
            InitializeComponent(); // Khởi tạo các điều khiển giao diện (TextBox, ComboBox, DataGridView...)
            cmbGt.SelectedIndex = 0; // Mặc định chọn giới tính đầu tiên ("Nam")

            // Nạp danh sách bệnh nhân từ CSDL lên DataGridView lần đầu tiên
            LoadData();

            // Gắn sự kiện sau khi nạp dữ liệu xong để tránh sự kiện kích hoạt quá sớm khi dữ liệu chưa sẵn sàng:
            // - SelectionChanged: Khi người dùng nhấp chọn 1 dòng trên bảng -> Điền thông tin lên các ô nhập
            dgv.SelectionChanged += (s, e) => FillInputs();
            // - TextChanged: Khi gõ từ khóa vào ô tìm kiếm -> Tự động lọc danh sách tức thì
            txtTim.TextChanged += (s, e) => LoadData();
        }

        /// <summary>
        /// Nút [Làm mới]: Xóa trắng ô tìm kiếm và các ô nhập liệu, đưa form về trạng thái ban đầu.
        /// </summary>
        void btnLamMoi_Click(object s, EventArgs e)
        {
            Reset();
        }

        /// <summary>
        /// Đọc danh sách bệnh nhân từ bảng BENHNHAN trong CSDL và hiển thị lên lưới dgv.
        /// Hỗ trợ tìm kiếm theo: Mã BN, Họ tên, số CCCD hoặc Số điện thoại.
        /// </summary>
        void LoadData()
        {
            string sql = @"
                SELECT MaBN AS [Mã BN], HoTen AS [Họ tên], NgaySinh AS [Ngày sinh], GioiTinh AS [Giới tính],
                       CCCD AS [CCCD], SoDienThoai AS [SĐT], DiaChi AS [Địa chỉ], NgheNghiep AS [Nghề nghiệp],
                       NguoiThan AS [Người thân], SDTNguoiThan AS [SĐT người thân], TinhTrang AS [Tình trạng bệnh]
                FROM BENHNHAN
                WHERE MaBN LIKE @k OR HoTen LIKE @k OR CCCD LIKE @k OR SoDienThoai LIKE @k
                ORDER BY MaBN";

            // Tham số @k kèm ký tự đại diện % giúp tìm kiếm chuỗi chứa từ khóa ở bất kỳ vị trí nào
            dgv.DataSource = DatabaseHelper.GetData(sql, DatabaseHelper.Param("@k", "%" + txtTim.Text.Trim() + "%"));
            // Định dạng cột ngày sinh hiển thị dạng ngày/tháng/năm
            FormatColumn(dgv, "Ngày sinh", "dd/MM/yyyy");
        }

        /// <summary>
        /// Lấy dữ liệu từ dòng đang được chọn trong DataGridView và điền ngược lại lên các TextBox/ComboBox.
        /// Giúp người dùng xem chi tiết hoặc chuẩn bị chỉnh sửa.
        /// </summary>
        void FillInputs()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;

            txtMa.Text = GetText(r, "Mã BN");
            txtTen.Text = GetText(r, "Họ tên");
            if (r.Cells["Ngày sinh"].Value is DateTime) 
                dtpNs.Value = (DateTime)r.Cells["Ngày sinh"].Value;
            cmbGt.Text = GetText(r, "Giới tính");
            txtCccd.Text = GetText(r, "CCCD");
            txtSdt.Text = GetText(r, "SĐT");
            txtDiaChi.Text = GetText(r, "Địa chỉ");
            txtNghe.Text = GetText(r, "Nghề nghiệp");
            txtNguoiThan.Text = GetText(r, "Người thân");
            txtSdtNT.Text = GetText(r, "SĐT người thân");
            txtTinhTrang.Text = GetText(r, "Tình trạng bệnh");
        }

        /// <summary>
        /// Xóa sạch dữ liệu trên các ô nhập liệu phía trên.
        /// </summary>
        void ClearInputs()
        {
            txtMa.Text = txtTen.Text = txtCccd.Text = txtSdt.Text = txtDiaChi.Text = "";
            txtNghe.Text = txtNguoiThan.Text = txtSdtNT.Text = txtTinhTrang.Text = "";
            dtpNs.Value = DateTime.Today;
            cmbGt.SelectedIndex = 0;
        }

        /// <summary>
        /// Đặt lại form: Xóa ô tìm kiếm, nạp lại toàn bộ dữ liệu và bỏ chọn trên bảng.
        /// </summary>
        void Reset()
        {
            txtTim.Text = "";
            LoadData();
            dgv.CurrentCell = null;
            ClearInputs();
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu trước khi Thêm hoặc Sửa (Validation).
        /// </summary>
        /// <returns>True nếu dữ liệu hợp lệ, False nếu có lỗi</returns>
        bool Valid()
        {
            if (txtTen.Text.Trim() == "") 
            { 
                ShowWarning("Vui lòng nhập họ tên bệnh nhân."); 
                txtTen.Focus(); 
                return false; 
            }

            if (dtpNs.Value.Date > DateTime.Today) 
            { 
                ShowWarning("Ngày sinh không thể lớn hơn ngày hiện tại."); 
                return false; 
            }

            string c = txtCccd.Text.Trim();
            // Nếu có nhập CCCD thì bắt buộc phải đúng 12 chữ số
            if (c != "" && (c.Length != 12 || !c.All(char.IsDigit))) 
            { 
                ShowWarning("Số CCCD phải gồm đúng 12 chữ số."); 
                txtCccd.Focus(); 
                return false; 
            }

            return true;
        }

        /// <summary>
        /// Đóng gói các giá trị từ form thành mảng tham số SqlParameter để truyền vào câu lệnh SQL an toàn.
        /// </summary>
        SqlParameter[] Ps(string ma)
        {
            return new[]
            {
                DatabaseHelper.Param("@ma", ma),
                DatabaseHelper.Param("@ten", txtTen.Text.Trim()),
                DatabaseHelper.Param("@ns", dtpNs.Value.Date),
                DatabaseHelper.Param("@gt", cmbGt.Text),
                DatabaseHelper.Param("@cccd", txtCccd.Text.Trim()),
                DatabaseHelper.Param("@sdt", txtSdt.Text.Trim()),
                DatabaseHelper.Param("@dc", txtDiaChi.Text.Trim()),
                DatabaseHelper.Param("@nghe", txtNghe.Text.Trim()),
                DatabaseHelper.Param("@nt", txtNguoiThan.Text.Trim()),
                DatabaseHelper.Param("@sdtnt", txtSdtNT.Text.Trim()),
                DatabaseHelper.Param("@tt", txtTinhTrang.Text.Trim())
            };
        }

        /// <summary>
        /// Xử lý khi bấm nút [Thêm]: Tự động sinh mã BN mới và lưu vào bảng BENHNHAN.
        /// </summary>
        void btnThem_Click(object s, EventArgs e)
        {
            if (!Valid()) return;
            try
            {
                // Tự sinh mã tiếp theo, ví dụ: "BN01", "BN02", "BN03"...
                string ma = DatabaseHelper.NextId("BENHNHAN", "MaBN", "BN");

                string sql = @"
                    INSERT INTO BENHNHAN(MaBN, HoTen, NgaySinh, GioiTinh, CCCD, SoDienThoai, DiaChi, NgheNghiep, NguoiThan, SDTNguoiThan, TinhTrang)
                    VALUES(@ma, @ten, @ns, @gt, @cccd, @sdt, @dc, @nghe, @nt, @sdtnt, @tt)";

                DatabaseHelper.ExecuteNonQuery(sql, Ps(ma));
                Reset(); // Nạp lại bảng và làm trống ô nhập
                ShowInfo("Đã thêm mới thành công bệnh nhân " + ma + ".");
            }
            catch (Exception ex) 
            { 
                ShowError(ex); 
            }
        }

        /// <summary>
        /// Xử lý khi bấm nút [Sửa]: Cập nhật thông tin của bệnh nhân đang chọn.
        /// </summary>
        void btnSua_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") 
            { 
                ShowWarning("Vui lòng chọn bệnh nhân cần chỉnh sửa trong danh sách phía dưới."); 
                return; 
            }
            if (!Valid()) return;

            try
            {
                string ma = txtMa.Text;
                string sql = @"
                    UPDATE BENHNHAN 
                    SET HoTen=@ten, NgaySinh=@ns, GioiTinh=@gt, CCCD=@cccd, SoDienThoai=@sdt,
                        DiaChi=@dc, NgheNghiep=@nghe, NguoiThan=@nt, SDTNguoiThan=@sdtnt, TinhTrang=@tt
                    WHERE MaBN=@ma";

                DatabaseHelper.ExecuteNonQuery(sql, Ps(ma));
                LoadData(); // Nạp lại bảng để thấy thông tin mới sửa
                ShowInfo("Đã cập nhật thông tin bệnh nhân " + ma + " thành công.");
            }
            catch (Exception ex) 
            { 
                ShowError(ex); 
            }
        }

        /// <summary>
        /// Xử lý khi bấm nút [Xóa]: Xóa bệnh nhân khỏi hệ thống.
        /// Có cơ chế bảo vệ: Không cho xóa nếu bệnh nhân này đã từng có hồ sơ nhập viện (Ràng buộc khóa ngoại FK).
        /// </summary>
        void btnXoa_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") 
            { 
                ShowWarning("Vui lòng chọn bệnh nhân cần xóa trong danh sách."); 
                return; 
            }

            // Hộp thoại hỏi lại để tránh người dùng bấm nhầm
            if (!Ask("Bạn có chắc chắn muốn xóa bệnh nhân " + txtMa.Text + " - " + txtTen.Text + "?")) 
                return;

            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM BENHNHAN WHERE MaBN=@ma", DatabaseHelper.Param("@ma", txtMa.Text));
                Reset();
                ShowInfo("Đã xóa bệnh nhân thành công.");
            }
            catch (Exception ex) 
            { 
                // Nếu vướng khóa ngoại với bảng NHAPVIEN -> Thông báo thân thiện thay vì văng lỗi kỹ thuật
                ShowError(ex, "Bệnh nhân này đã có hồ sơ nhập viện trong hệ thống nên không thể xóa."); 
            }
        }
    }
}
