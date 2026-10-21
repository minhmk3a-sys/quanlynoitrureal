using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Form Quản lý Đơn thuốc nội trú:
    /// - Quy trình nghiệp vụ: Chọn Bệnh án khám bệnh -> Chọn đơn thuốc hoặc Tạo đơn thuốc mới -> Kê thêm thuốc vào đơn.
    /// - Quản lý Kho dược tự động (Sử dụng Transaction an toàn tuyệt đối):
    ///   + Khi thêm thuốc vào đơn: Tự động TRỪ số lượng tồn kho của thuốc tương ứng.
    ///   + Khi sửa số lượng thuốc: Tự động TÍNH ĐỘ CHÊNH LỆCH và bù/trừ tồn kho.
    ///   + Khi xóa thuốc khỏi đơn: Tự động HOÀN TRẢ số lượng thuốc về tồn kho.
    /// - Tự động tính thành tiền từng loại thuốc và tổng tiền toàn bộ đơn thuốc.
    /// 
    /// ====================================================================================================
    /// 📚 HƯỚNG DẪN CỐT LÕI CHO NGƯỜI MỚI HỌC C#: QUAN HỆ MASTER - DETAIL VÀ QUẢN LÝ TỒN KHO DƯỢC
    /// ====================================================================================================
    /// 1. MÔ HÌNH MASTER - DETAIL (BẢNG CHA - BẢNG CON):
    ///    - Bảng Cha (Master): 'DONTHUOC' lưu thông tin chung của cả đơn: Mã đơn, Mã bệnh án, Ngày kê, Ghi chú.
    ///    - Bảng Con (Detail): 'CT_DONTHUOC' lưu từng món thuốc cụ thể trong đơn: Mã đơn, Mã thuốc, Số lượng, Đơn giá, Thành tiền.
    ///    - Mối quan hệ 1-N: 1 Đơn thuốc có thể chứa nhiều dòng chi tiết thuốc.
    ///    - Cách lập trình giao diện:
    ///      + ComboBox 'cmbDon': Hiển thị danh sách các đơn thuốc của bệnh án đang chọn.
    ///      + DataGridView 'dgv': Hiển thị toàn bộ các thuốc thuộc đơn thuốc đang được chọn trong 'cmbDon'.
    /// 
    /// 2. THUẬT TOÁN ĐỒNG BỘ TỒN KHO DƯỢC BẰNG TRANSACTION:
    ///    - Thêm thuốc vào đơn:
    ///      + Bước 1: Kiểm tra tồn kho 'TonKho(maThuoc)'. Nếu tồn kho < số lượng cần kê -> Cảnh báo ngay và dừng lại.
    ///      + Bước 2: Kiểm tra xem thuốc này đã có trong đơn chưa (tránh kê trùng lặp 2 dòng cùng 1 loại thuốc).
    ///      + Bước 3: Chạy Transaction gồm 2 câu lệnh SQL:
    ///          Lệnh 1: INSERT INTO CT_DONTHUOC (lưu chi tiết thuốc).
    ///          Lệnh 2: UPDATE THUOC SET SoLuongTon = SoLuongTon - @sl (trừ bớt số lượng trong kho).
    /// 
    ///    - Sửa số lượng thuốc đã kê:
    ///      + Ta tính số chênh lệch: int chenh = soLuongMoi - soLuongCu;
    ///      + Nếu chenh > 0 (kê thêm thuốc): Kiểm tra kho còn đủ thuốc không.
    ///      + Chạy Transaction:
    ///          Lệnh 1: UPDATE CT_DONTHUOC SET SoLuong = @soLuongMoi...
    ///          Lệnh 2: UPDATE THUOC SET SoLuongTon = SoLuongTon - @chenh
    ///          (Nếu chenh > 0 thì kho bị trừ bớt; nếu chenh < 0 thì trừ số âm tương đương với CỘNG trả lại kho!).
    /// 
    ///    - Xóa thuốc khỏi đơn:
    ///      + Chạy Transaction:
    ///          Lệnh 1: DELETE FROM CT_DONTHUOC WHERE MaDon=@d AND MaThuoc=@t
    ///          Lệnh 2: UPDATE THUOC SET SoLuongTon = SoLuongTon + @sl (hoàn trả 100% thuốc về kho).
    /// ====================================================================================================
    /// </summary>
    public partial class DonThuocForm : BaseForm
    {
        bool loading; // Cờ ngăn chặn các sự kiện lặp khi nạp dữ liệu động

        /// <summary>
        /// Khởi tạo form Đơn thuốc, tải danh mục thuốc còn hoạt động, nạp danh sách bệnh án và gán các sự kiện.
        /// </summary>
        public DonThuocForm()
        {
            InitializeComponent();   // dựng giao diện (code nằm trong DonThuocForm.Designer.cs)
            LoadThuoc();

            // Nạp danh sách các ca bệnh án vào ComboBox
            FillCombo(cmbBA,
                DatabaseHelper.GetData(@"SELECT ba.MaBA, ba.MaBA + N' - ' + bn.HoTen + N' - ' + ISNULL(ba.ChanDoan, N'') AS Hien
                           FROM BENHAN ba
                           JOIN NHAPVIEN nv ON nv.MaNhapVien = ba.MaNhapVien
                           JOIN BENHNHAN bn ON bn.MaBN = nv.MaBN
                           ORDER BY ba.NgayLap DESC, ba.MaBA DESC"),
                "Hien", "MaBA");

            // Đăng ký các sự kiện tương tác
            cmbBA.SelectedIndexChanged += (s, e) => { if (!loading) LoadDon(null); };
            cmbDon.SelectedIndexChanged += (s, e) => { if (!loading) LoadCT(); };
            cmbThuoc.SelectedIndexChanged += (s, e) => ShowGia();
            dgv.SelectionChanged += (s, e) => FillFromGrid();

            LoadDon(null);
        }

        // ====================================================================
        // NẠP DỮ LIỆU
        // ====================================================================

        /// <summary>
        /// Nạp danh sách các loại thuốc đang kinh doanh (TrangThai = 1) kèm số lượng tồn kho vào ComboBox.
        /// </summary>
        void LoadThuoc()
        {
            string cur = GetValue(cmbThuoc);
            FillCombo(cmbThuoc,
                DatabaseHelper.GetData(@"SELECT MaThuoc, TenThuoc + N' (tồn: ' + CAST(SoLuongTon AS NVARCHAR(10)) + N')' AS Hien, DonGia
                           FROM THUOC WHERE TrangThai = 1 ORDER BY TenThuoc"),
                "Hien", "MaThuoc");
            if (cur != null) cmbThuoc.SelectedValue = cur;
            ShowGia();
        }

        /// <summary>
        /// Hiển thị đơn giá của loại thuốc đang được chọn lên nhãn lblGia.
        /// </summary>
        void ShowGia()
        {
            var drv = cmbThuoc.SelectedItem as DataRowView;
            lblGia.Text = drv == null ? "0 đ" : Fmt.Money(Convert.ToDecimal(drv["DonGia"]));
        }

        /// <summary>
        /// Tải danh sách các đơn thuốc thuộc về bệnh án đang chọn:
        /// Sau đó tự động kích hoạt nạp chi tiết các thuốc trong đơn.
        /// </summary>
        /// <param name="chon">Mã đơn thuốc muốn chọn ngay sau khi nạp (hoặc null nếu chọn đơn đầu tiên).</param>
        void LoadDon(string chon)
        {
            loading = true;
            FillCombo(cmbDon,
                DatabaseHelper.GetData(@"SELECT MaDon, MaDon + N' (' + CONVERT(NVARCHAR(10), NgayKeDon, 103) + N')' AS Hien
                           FROM DONTHUOC WHERE MaBA = @ba ORDER BY NgayKeDon DESC, MaDon DESC", DatabaseHelper.Param("@ba", GetValue(cmbBA))),
                "Hien", "MaDon");
            if (chon != null) cmbDon.SelectedValue = chon;
            loading = false;
            LoadCT();
        }

        /// <summary>
        /// Tải bảng chi tiết các thuốc trong đơn đang chọn (CT_DONTHUOC) lên DataGridView và tính tổng tiền toàn đơn.
        /// </summary>
        void LoadCT()
        {
            var dt = DatabaseHelper.GetData(
                @"SELECT ct.MaThuoc AS [Mã thuốc], t.TenThuoc AS [Tên thuốc], ct.SoLuong AS [Số lượng],
                         ct.LieuDung AS [Liều dùng], ct.CachDung AS [Cách dùng], ct.DonGia AS [Đơn giá], ct.ThanhTien AS [Thành tiền]
                  FROM CT_DONTHUOC ct JOIN THUOC t ON t.MaThuoc = ct.MaThuoc
                  WHERE ct.MaDon = @d ORDER BY t.TenThuoc", DatabaseHelper.Param("@d", GetValue(cmbDon)));
            dgv.DataSource = dt;
            FormatColumn(dgv, "Đơn giá", "N0");
            FormatColumn(dgv, "Thành tiền", "N0");

            decimal tong = 0;
            foreach (DataRow r in dt.Rows) tong += Convert.ToDecimal(r["Thành tiền"]);
            lblTong.Text = "Tổng tiền đơn: " + Fmt.Money(tong);
        }

        /// <summary>
        /// Đưa thông tin của dòng thuốc đang chọn từ DataGridView vào các điều khiển kê đơn (thuốc, số lượng, liều dùng, cách dùng).
        /// </summary>
        void FillFromGrid()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;
            if (GetText(r, "Mã thuốc") != "") cmbThuoc.SelectedValue = GetText(r, "Mã thuốc");
            int sl;
            if (int.TryParse(GetText(r, "Số lượng"), out sl) && sl >= 1 && sl <= nudSL.Maximum) nudSL.Value = sl;
            txtLieu.Text = GetText(r, "Liều dùng");
            txtCach.Text = GetText(r, "Cách dùng");
        }

        // ====================================================================
        // NGHIỆP VỤ KÊ ĐƠN VÀ QUẢN LÝ KHO THUỐC
        // ====================================================================

        /// <summary>
        /// Tạo một đơn thuốc mới cho bệnh án đang chọn với mã sinh tự động (DT...).
        /// </summary>
        void btnTaoDon_Click(object s, EventArgs e)
        {
            string ba = GetValue(cmbBA);
            if (ba == null) { ShowWarning("Hãy chọn bệnh án trước."); return; }
            try
            {
                string ma = DatabaseHelper.NextId("DONTHUOC", "MaDon", "DT");
                DatabaseHelper.ExecuteNonQuery("INSERT INTO DONTHUOC(MaDon, MaBA, GhiChu) VALUES(@ma, @ba, @gc)",
                    DatabaseHelper.Param("@ma", ma), DatabaseHelper.Param("@ba", ba), DatabaseHelper.Param("@gc", txtGhiChuDon.Text.Trim()));
                txtGhiChuDon.Text = "";
                LoadDon(ma);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        /// <summary>
        /// Lấy số lượng tồn kho hiện tại của một loại thuốc trong CSDL.
        /// </summary>
        int TonKho(string maThuoc)
        {
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar("SELECT SoLuongTon FROM THUOC WHERE MaThuoc=@t", DatabaseHelper.Param("@t", maThuoc)));
        }

        /// <summary>
        /// Kê thêm thuốc vào đơn:
        /// 1. Kiểm tra số lượng tồn kho có đủ không.
        /// 2. Kiểm tra thuốc đã có trong đơn chưa (nếu đã có thì yêu cầu Cập nhật).
        /// 3. Thực thi Transaction: INSERT CT_DONTHUOC và TRỪ SoLuongTon trong bảng THUOC.
        /// 4. Làm mới danh sách chi tiết đơn và cập nhật lại ComboBox hiển thị số tồn kho mới.
        /// </summary>
        void btnThemThuoc_Click(object s, EventArgs e)
        {
            string don = GetValue(cmbDon), th = GetValue(cmbThuoc);
            if (don == null) { ShowWarning("Hãy tạo đơn mới hoặc chọn một đơn thuốc trước."); return; }
            if (th == null) { ShowWarning("Hãy chọn thuốc."); return; }
            int sl = (int)nudSL.Value;

            try
            {
                int ton = TonKho(th);
                if (ton < sl) { ShowWarning("Không đủ thuốc trong kho (còn " + ton + ")."); return; }

                int da = Convert.ToInt32(DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM CT_DONTHUOC WHERE MaDon=@d AND MaThuoc=@t", DatabaseHelper.Param("@d", don), DatabaseHelper.Param("@t", th)));
                if (da > 0) { ShowWarning("Thuốc này đã có trong đơn. Hãy chọn dòng đó rồi bấm Cập nhật."); return; }

                DatabaseHelper.ExecuteTransaction(
                    new SqlStatement(@"INSERT INTO CT_DONTHUOC(MaDon, MaThuoc, SoLuong, LieuDung, CachDung, DonGia)
                               SELECT @d, @t, @sl, @lieu, @cach, DonGia FROM THUOC WHERE MaThuoc = @t",
                        DatabaseHelper.Param("@d", don), DatabaseHelper.Param("@t", th), DatabaseHelper.Param("@sl", sl),
                        DatabaseHelper.Param("@lieu", txtLieu.Text.Trim()), DatabaseHelper.Param("@cach", txtCach.Text.Trim())),
                    new SqlStatement("UPDATE THUOC SET SoLuongTon = SoLuongTon - @sl WHERE MaThuoc = @t", DatabaseHelper.Param("@sl", sl), DatabaseHelper.Param("@t", th)));

                LoadCT();
                LoadThuoc();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        /// <summary>
        /// Cập nhật số lượng hoặc liều dùng của một loại thuốc đã kê trong đơn:
        /// Tính chênh lệch giữa số lượng mới và cũ để trừ thêm hoặc hoàn lại tồn kho trong Transaction.
        /// </summary>
        void btnCapNhat_Click(object s, EventArgs e)
        {
            var r = dgv.CurrentRow;
            string don = GetValue(cmbDon);
            if (r == null || don == null) { ShowWarning("Hãy chọn một dòng thuốc trong đơn để cập nhật."); return; }

            string th = GetText(r, "Mã thuốc");
            int cu = int.Parse(GetText(r, "Số lượng"));
            int moi = (int)nudSL.Value;
            int chenh = moi - cu;

            try
            {
                if (chenh > TonKho(th)) { ShowWarning("Không đủ thuốc trong kho để tăng số lượng."); return; }
                DatabaseHelper.ExecuteTransaction(
                    new SqlStatement("UPDATE CT_DONTHUOC SET SoLuong=@sl, LieuDung=@lieu, CachDung=@cach WHERE MaDon=@d AND MaThuoc=@t",
                        DatabaseHelper.Param("@sl", moi), DatabaseHelper.Param("@lieu", txtLieu.Text.Trim()), DatabaseHelper.Param("@cach", txtCach.Text.Trim()),
                        DatabaseHelper.Param("@d", don), DatabaseHelper.Param("@t", th)),
                    new SqlStatement("UPDATE THUOC SET SoLuongTon = SoLuongTon - @c WHERE MaThuoc = @t", DatabaseHelper.Param("@c", chenh), DatabaseHelper.Param("@t", th)));
                LoadCT();
                LoadThuoc();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        /// <summary>
        /// Xóa một loại thuốc khỏi đơn:
        /// Tự động thực thi Transaction xóa dòng CT_DONTHUOC và HOÀN TRẢ số lượng thuốc về tồn kho trong THUOC.
        /// </summary>
        void btnXoaThuoc_Click(object s, EventArgs e)
        {
            var r = dgv.CurrentRow;
            string don = GetValue(cmbDon);
            if (r == null || don == null) { ShowWarning("Hãy chọn một dòng thuốc trong đơn để xóa."); return; }
            if (!Ask("Xóa thuốc " + GetText(r, "Tên thuốc") + " khỏi đơn? (số lượng sẽ được hoàn lại kho)")) return;

            string th = GetText(r, "Mã thuốc");
            int sl = int.Parse(GetText(r, "Số lượng"));
            try
            {
                DatabaseHelper.ExecuteTransaction(
                    new SqlStatement("DELETE FROM CT_DONTHUOC WHERE MaDon=@d AND MaThuoc=@t", DatabaseHelper.Param("@d", don), DatabaseHelper.Param("@t", th)),
                    new SqlStatement("UPDATE THUOC SET SoLuongTon = SoLuongTon + @sl WHERE MaThuoc = @t", DatabaseHelper.Param("@sl", sl), DatabaseHelper.Param("@t", th)));
                LoadCT();
                LoadThuoc();
            }
            catch (Exception ex) { ShowError(ex); }
        }
    }
}
