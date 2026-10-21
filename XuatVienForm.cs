using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Chức năng Xuất viện hoàn chỉnh (PRD B4 & BR07):
    /// - Quản lý danh sách bệnh nhân xuất viện
    /// - Bảng tổng hợp chi phí (tiền giường, tiền thuốc, tiền dịch vụ, BHYT trả, BN trả)
    /// - Bắt buộc thanh toán 100% viện phí trước khi cho xuất viện
    /// - Khi hoàn tất xuất viện: tự động trả giường về Trống và cập nhật trạng thái bệnh nhân
    /// </summary>
    public partial class XuatVienForm : BaseForm
    {
        // Thông tin ca nhập viện đang chọn
        string maNV = "", maBN = "", tenBN = "", maGiuong = "", tenGiuong = "", tenPhong = "", tenKhoa = "";
        DateTime ngayVao;
        int soNgayNam = 1;
        decimal tienGiuong = 0, tienThuoc = 0, tienDichVu = 0, tienKham = 0;
        decimal bhGiuong = 0, bhThuoc = 0, bhDichVu = 0;
        decimal tongTien = 0, tongBHTra = 0, tongBNTra = 0;
        string trangThaiNV = "", trangThaiVP = "Chưa thanh toán";

        public XuatVienForm()
        {
            InitializeComponent();
            dtpNgayRa.Value = DateTime.Now;

            LoadDanhSach();
            dgv.SelectionChanged += (s, e) => ChonDong();
            txtTim.TextChanged += (s, e) => LoadDanhSach();
            dtpNgayRa.ValueChanged += (s, e) => TinhChiPhi();
        }

        // ====================================================================
        // 1. TẢI DỮ LIỆU
        // ====================================================================

        void LoadDanhSach()
        {
            string sql = @"
                SELECT nv.MaNhapVien AS [Mã NV], bn.MaBN AS [Mã BN], bn.HoTen AS [Bệnh nhân],
                       bn.BHYT AS [Số thẻ BHYT], k.TenKhoa AS [Khoa], p.TenPhong AS [Phòng],
                       g.TenGiuong AS [Giường], g.MaGiuong, nv.NgayNhapVien AS [Ngày vào],
                       nv.NgayRaVien AS [Ngày ra], nv.TrangThai AS [Trạng thái NV],
                       ISNULL(vp.TrangThai, N'Chưa lập VP') AS [Trạng thái viện phí],
                       ISNULL(vp.TongTien, 0) AS [Tổng tiền]
                FROM NHAPVIEN nv
                JOIN BENHNHAN bn ON bn.MaBN = nv.MaBN
                LEFT JOIN GIUONG g ON g.MaGiuong = nv.MaGiuong
                LEFT JOIN PHONG p ON p.MaPhong = g.MaPhong
                LEFT JOIN KHOA k ON k.MaKhoa = p.MaKhoa
                LEFT JOIN VIENPHI vp ON vp.MaNhapVien = nv.MaNhapVien
                WHERE (nv.MaNhapVien LIKE @k OR bn.HoTen LIKE @k OR bn.MaBN LIKE @k)
                ORDER BY CASE WHEN nv.TrangThai = N'Đang điều trị' THEN 1 
                              WHEN nv.TrangThai = N'Chờ thanh toán' THEN 2 ELSE 3 END,
                         nv.NgayNhapVien DESC";

            dgv.DataSource = DatabaseHelper.GetData(sql, DatabaseHelper.Param("@k", "%" + txtTim.Text.Trim() + "%"));
            FormatColumn(dgv, "Ngày vào", "dd/MM/yyyy HH:mm");
            FormatColumn(dgv, "Ngày ra", "dd/MM/yyyy HH:mm");
            FormatColumn(dgv, "Tổng tiền", "N0");
        }

        void ChonDong()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;

            maNV = GetText(r, "Mã NV");
            maBN = GetText(r, "Mã BN");
            tenBN = GetText(r, "Bệnh nhân");
            maGiuong = GetText(r, "MaGiuong");
            tenGiuong = GetText(r, "Giường");
            tenPhong = GetText(r, "Phòng");
            tenKhoa = GetText(r, "Khoa");
            trangThaiNV = GetText(r, "Trạng thái NV");

            txtMaNV.Text = maNV;
            txtTenBN.Text = tenBN + " (" + maBN + ")";
            txtBHYT.Text = GetText(r, "Số thẻ BHYT");
            txtKhoaPhong.Text = tenKhoa + " - " + tenPhong + " (" + tenGiuong + ")";
            lblTrangThaiNV.Text = trangThaiNV;

            object v = r.Cells["Ngày vào"].Value;
            if (v is DateTime) ngayVao = (DateTime)v;
            else ngayVao = DateTime.Today;

            txtNgayVao.Text = ngayVao.ToString("dd/MM/yyyy HH:mm");

            object rVal = r.Cells["Ngày ra"].Value;
            if (rVal is DateTime) dtpNgayRa.Value = (DateTime)rVal;
            else dtpNgayRa.Value = DateTime.Now;

            TinhChiPhi();
        }

        // ====================================================================
        // 2. TÍNH TOÁN CHI PHÍ XUẤT VIỆN (PRD B4 & 11.1)
        // ====================================================================

        void TinhChiPhi()
        {
            if (string.IsNullOrEmpty(maNV)) return;

            DateTime ngayRa = dtpNgayRa.Value;
            if (ngayRa < ngayVao) ngayRa = ngayVao;

            // Số ngày nằm viện theo PRD: Ngày xuất - Ngày nhập (tối thiểu 1 ngày nếu ra cùng ngày)
            int diffDays = (ngayRa.Date - ngayVao.Date).Days;
            soNgayNam = Math.Max(1, diffDays);
            txtSoNgayNam.Text = soNgayNam.ToString() + " ngày";

            // Đơn giá giường: lấy đơn giá giường, nếu = 0 thì lấy đơn giá phòng
            decimal donGiaGiuong = 0;
            var dtG = DatabaseHelper.GetData(@"
                SELECT CASE WHEN ISNULL(g.DonGia, 0) > 0 THEN g.DonGia ELSE ISNULL(p.DonGia, 0) END AS DonGiaApDung
                FROM NHAPVIEN nv
                LEFT JOIN GIUONG g ON g.MaGiuong = nv.MaGiuong
                LEFT JOIN PHONG p ON p.MaPhong = g.MaPhong
                WHERE nv.MaNhapVien = @nv", DatabaseHelper.Param("@nv", maNV));

            if (dtG.Rows.Count > 0 && dtG.Rows[0]["DonGiaApDung"] != DBNull.Value)
                donGiaGiuong = Convert.ToDecimal(dtG.Rows[0]["DonGiaApDung"]);

            tienGiuong = soNgayNam * donGiaGiuong;

            // BHYT: Nếu có thẻ BHYT thì được giảm theo quy định (mặc định 80% phần giường chuẩn)
            bool coBHYT = !string.IsNullOrWhiteSpace(txtBHYT.Text);
            bhGiuong = coBHYT ? (tienGiuong * 0.8m) : 0;

            // Tiền thuốc: Lấy từ các đơn thuốc đã kê
            var dtThuoc = DatabaseHelper.GetData(@"
                SELECT ISNULL(SUM(ct.ThanhTien), 0) AS TongTienThuoc,
                       ISNULL(SUM(ct.ThanhTien * ISNULL(t.PT_BaoHiem, 0) / 100.0), 0) AS BHTienThuoc
                FROM CT_DONTHUOC ct
                JOIN THUOC t ON t.MaThuoc = ct.MaThuoc
                JOIN DONTHUOC dt ON dt.MaDon = ct.MaDon
                JOIN BENHAN ba ON ba.MaBA = dt.MaBA
                WHERE ba.MaNhapVien = @nv", DatabaseHelper.Param("@nv", maNV));

            if (dtThuoc.Rows.Count > 0)
            {
                tienThuoc = Convert.ToDecimal(dtThuoc.Rows[0]["TongTienThuoc"]);
                bhThuoc = Convert.ToDecimal(dtThuoc.Rows[0]["BHTienThuoc"]);
            }
            else
            {
                tienThuoc = 0;
                bhThuoc = 0;
            }

            // Tiền dịch vụ: Lấy từ bảng LICH_DIEU_TRI (nếu có)
            var dtDV = DatabaseHelper.GetData(@"
                SELECT ISNULL(SUM(ldt.SoLuong * ldt.DonGia), 0) AS TongDV,
                       ISNULL(SUM(ldt.SoLuong * ldt.DonGia * ISNULL(ldt.PT_BaoHiem, 0) / 100.0), 0) AS BHTienDV
                FROM LICH_DIEU_TRI ldt
                WHERE ldt.MaNhapVien = @nv", DatabaseHelper.Param("@nv", maNV));

            if (dtDV.Rows.Count > 0)
            {
                tienDichVu = Convert.ToDecimal(dtDV.Rows[0]["TongDV"]);
                bhDichVu = Convert.ToDecimal(dtDV.Rows[0]["BHTienDV"]);
            }
            else
            {
                tienDichVu = 0;
                bhDichVu = 0;
            }

            // Tổng hợp
            tongTien = tienGiuong + tienThuoc + tienDichVu + tienKham;
            tongBHTra = bhGiuong + bhThuoc + bhDichVu;
            tongBNTra = Math.Max(0, tongTien - tongBHTra);

            // Hiển thị lên giao diện
            txtTienGiuong.Text = Fmt.Money(tienGiuong);
            txtTienThuoc.Text = Fmt.Money(tienThuoc);
            txtTienDichVu.Text = Fmt.Money(tienDichVu);
            txtTongTien.Text = Fmt.Money(tongTien);
            txtBHTra.Text = Fmt.Money(tongBHTra);
            txtBNTra.Text = Fmt.Money(tongBNTra);

            // Kiểm tra trạng thái viện phí từ database
            var dtVP = DatabaseHelper.GetData(
                "SELECT TrangThai, BNTra, TongTien FROM VIENPHI WHERE MaNhapVien = @nv",
                DatabaseHelper.Param("@nv", maNV));

            if (dtVP.Rows.Count > 0)
            {
                trangThaiVP = dtVP.Rows[0]["TrangThai"].ToString();
            }
            else
            {
                trangThaiVP = "Chưa thanh toán";
            }

            lblTrangThaiVP.Text = trangThaiVP;
            lblTrangThaiVP.ForeColor = (trangThaiVP == "Đã thanh toán") ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }

        // ====================================================================
        // 3. CÁC NÚT THAO TÁC
        // ====================================================================

        // Lưu / cập nhật viện phí
        void btnLuuVienPhi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maNV)) { ShowWarning("Hãy chọn hồ sơ nhập viện cần tính viện phí."); return; }
            if (trangThaiVP == "Đã thanh toán") { ShowWarning("Viện phí đã thanh toán, không cần lưu lại."); return; }

            try
            {
                TinhChiPhi();

                // Kiểm tra xem đã có bản ghi VIENPHI chưa
                bool exists = DatabaseHelper.Exists("SELECT 1 FROM VIENPHI WHERE MaNhapVien = @nv", DatabaseHelper.Param("@nv", maNV));
                if (!exists)
                {
                    string maVP = DatabaseHelper.NextId("VIENPHI", "MaVP", "VP");
                    DatabaseHelper.ExecuteNonQuery(@"
                        INSERT INTO VIENPHI(MaVP, MaNhapVien, NgayLap, SoNgay, TienPhong, TienThuoc, TienKham, TienDichVu, TongTien, BHTra, BNTra, TrangThai)
                        VALUES(@mvp, @nv, GETDATE(), @sn, @tp, @tt, @tk, @tdv, @tong, @bh, @bn, N'Chưa thanh toán')",
                        DatabaseHelper.Param("@mvp", maVP), DatabaseHelper.Param("@nv", maNV), DatabaseHelper.Param("@sn", soNgayNam),
                        DatabaseHelper.Param("@tp", tienGiuong), DatabaseHelper.Param("@tt", tienThuoc), DatabaseHelper.Param("@tk", tienKham),
                        DatabaseHelper.Param("@tdv", tienDichVu), DatabaseHelper.Param("@tong", tongTien), DatabaseHelper.Param("@bh", tongBHTra),
                        DatabaseHelper.Param("@bn", tongBNTra));
                }
                else
                {
                    DatabaseHelper.ExecuteNonQuery(@"
                        UPDATE VIENPHI 
                        SET SoNgay=@sn, TienPhong=@tp, TienThuoc=@tt, TienKham=@tk, TienDichVu=@tdv, TongTien=@tong, BHTra=@bh, BNTra=@bn, NgayLap=GETDATE()
                        WHERE MaNhapVien=@nv",
                        DatabaseHelper.Param("@sn", soNgayNam), DatabaseHelper.Param("@tp", tienGiuong), DatabaseHelper.Param("@tt", tienThuoc),
                        DatabaseHelper.Param("@tk", tienKham), DatabaseHelper.Param("@tdv", tienDichVu), DatabaseHelper.Param("@tong", tongTien),
                        DatabaseHelper.Param("@bh", tongBHTra), DatabaseHelper.Param("@bn", tongBNTra), DatabaseHelper.Param("@nv", maNV));
                }

                // Cập nhật trạng thái hồ sơ thành 'Chờ thanh toán' nếu đang điều trị
                if (trangThaiNV == "Đang điều trị")
                {
                    DatabaseHelper.ExecuteNonQuery("UPDATE NHAPVIEN SET TrangThai = N'Chờ thanh toán' WHERE MaNhapVien = @nv", DatabaseHelper.Param("@nv", maNV));
                }

                LoadDanhSach();
                ShowInfo("Đã lưu bảng tổng hợp chi phí thành công.");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        // Thanh toán viện phí (bắt buộc trước khi xuất viện)
        void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maNV)) { ShowWarning("Hãy chọn hồ sơ cần thanh toán."); return; }
            if (trangThaiVP == "Đã thanh toán") { ShowInfo("Hồ sơ này đã thanh toán 100% rồi."); return; }

            // Nếu chưa lưu bảng kê thì lưu trước
            btnLuuVienPhi_Click(null, null);

            if (!Ask("Xác nhận thu tiền viện phí: " + Fmt.Money(tongBNTra) + " từ bệnh nhân " + tenBN + "?")) return;

            try
            {
                DatabaseHelper.ExecuteNonQuery(@"
                    UPDATE VIENPHI 
                    SET TrangThai = N'Đã thanh toán', PhuongThucTT = N'Tiền mặt', NgayThanhToan = GETDATE()
                    WHERE MaNhapVien = @nv", DatabaseHelper.Param("@nv", maNV));

                LoadDanhSach();
                TinhChiPhi();
                ShowInfo("Thanh toán thành công! Hiện tại bệnh nhân đã đủ điều kiện để Hoàn tất xuất viện.");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        // HOÀN TẤT XUẤT VIỆN (Áp dụng BR07: Chặn nếu chưa thanh toán đủ 100%)
        void btnHoanTatXuatVien_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maNV)) { ShowWarning("Hãy chọn hồ sơ cần xuất viện."); return; }
            if (trangThaiNV == "Đã xuất viện") { ShowInfo("Bệnh nhân này đã xuất viện rồi."); return; }

            // ⚠️ BR07: Kiểm tra thanh toán 100%
            if (trangThaiVP != "Đã thanh toán")
            {
                ShowWarning("KHÔNG THỂ XUẤT VIỆN!\n\nTheo quy tắc BR07: Bệnh nhân bắt buộc phải thanh toán đủ 100% viện phí trước khi hoàn tất thủ tục xuất viện.\n\nHiện tại trạng thái là: [" + trangThaiVP + "]. Vui lòng bấm 'Thanh toán viện phí' trước!");
                return;
            }

            if (!Ask("Xác nhận HOÀN TẤT XUẤT VIỆN cho bệnh nhân " + tenBN + "?\nGiường " + tenGiuong + " sẽ tự động trả về trạng thái Trống."))
                return;

            try
            {
                // Thực hiện đồng bộ trong Transaction
                DatabaseHelper.ExecuteTransaction(
                    // 1. Cập nhật hồ sơ nhập viện
                    new SqlStatement(@"
                        UPDATE NHAPVIEN 
                        SET TrangThai = N'Đã xuất viện', NgayRaVien = @ra 
                        WHERE MaNhapVien = @nv",
                        DatabaseHelper.Param("@ra", dtpNgayRa.Value), DatabaseHelper.Param("@nv", maNV)),

                    // 2. Trả giường về trạng thái trống (TrangThai = N'Trống')
                    new SqlStatement(@"
                        UPDATE GIUONG 
                        SET TrangThai = N'Trống' 
                        WHERE MaGiuong = @g",
                        DatabaseHelper.Param("@g", maGiuong)),

                    // 3. Cập nhật trạng thái bệnh nhân
                    new SqlStatement(@"
                        UPDATE BENHNHAN 
                        SET TrangThai = N'Đã xuất viện' 
                        WHERE MaBN = @bn",
                        DatabaseHelper.Param("@bn", maBN))
                );

                LoadDanhSach();
                ShowInfo("ĐÃ HOÀN TẤT XUẤT VIỆN THÀNH CÔNG!\n- Hồ sơ nhập viện: Đã xuất viện\n- Trạng thái giường: Đã trả về Trống\n- Trạng thái bệnh nhân: Đã xuất viện");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        // In phiếu xuất viện & bảng kê chi phí
        void btnInPhieu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maNV)) { ShowWarning("Hãy chọn hồ sơ nhập viện để in phiếu."); return; }

            try
            {
                TinhChiPhi();
                var L = new List<string>();
                L.Add("============================================================");
                L.Add("            BỆNH VIỆN ĐA KHOA NỘI TRÚ");
                L.Add("        GIẤY XUẤT VIỆN VÀ BẢNG TỔNG HỢP CHI PHÍ");
                L.Add("============================================================");
                L.Add("");
                L.Add("Mã hồ sơ NV : " + maNV);
                L.Add("Bệnh nhân   : " + tenBN + " (Mã: " + maBN + ")");
                L.Add("Số thẻ BHYT : " + (string.IsNullOrEmpty(txtBHYT.Text) ? "Không có" : txtBHYT.Text));
                L.Add("Khoa phòng  : " + tenKhoa + " - " + tenPhong + " (Giường: " + tenGiuong + ")");
                L.Add("Ngày vào    : " + ngayVao.ToString("dd/MM/yyyy HH:mm"));
                L.Add("Ngày ra     : " + dtpNgayRa.Value.ToString("dd/MM/yyyy HH:mm"));
                L.Add("Số ngày nằm : " + soNgayNam + " ngày");
                L.Add(new string('-', 60));
                L.Add("CHI TIẾT CÁC KHOẢN CHI PHÍ:");
                L.Add("1. Tiền giường/phòng : " + Fmt.Money(tienGiuong));
                L.Add("2. Tiền thuốc        : " + Fmt.Money(tienThuoc));
                L.Add("3. Tiền dịch vụ y tế : " + Fmt.Money(tienDichVu));
                L.Add(new string('-', 60));
                L.Add("TỔNG CHI PHÍ        : " + Fmt.Money(tongTien));
                L.Add("BẢO HIỂM CHI TRẢ    : " + Fmt.Money(tongBHTra));
                L.Add("BỆNH NHÂN TỰ TRẢ    : " + Fmt.Money(tongBNTra));
                L.Add(new string('-', 60));
                L.Add("Trạng thái viện phí : " + trangThaiVP);
                L.Add("Trạng thái ra viện  : " + trangThaiNV);
                L.Add("");
                L.Add("   Người nộp tiền                         Bác sĩ / Kế toán");
                L.Add(" (ký, ghi rõ họ tên)                     (ký, ghi rõ họ tên)");

                Export.Preview(Export.TextDoc(L), "Phiếu xuất viện - " + maNV);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTim.Text = "";
            LoadDanhSach();
        }
    }
}
