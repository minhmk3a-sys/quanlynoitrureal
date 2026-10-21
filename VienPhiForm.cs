using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>Viện phí: tiền phòng = số ngày nằm x đơn giá phòng (tính cả ngày vào và ra, tối thiểu 1 ngày); tiền thuốc = tổng các đơn thuốc; tiền khám / dịch vụ nhập tay.</summary>
    public partial class VienPhiForm : BaseForm
    {
        // dữ liệu tính toán hiện tại
        decimal tienPhong, tienThuoc;
        int soNgay;
        string maVP = "", trangThai = "Chưa lập";
        string maBN = "", tenBN = "", tenBS = "", tenPhong = "";
        DateTime vao;
        DateTime? ra;
        bool loading;

        public VienPhiForm()
        {
            InitializeComponent();   // dựng giao diện (code nằm trong VienPhiForm.Designer.cs)
            FillCombo(cmbNV,
                DatabaseHelper.GetData(@"SELECT nv.MaNhapVien,
                                  nv.MaNhapVien + N' - ' + bn.HoTen + N' (' + nv.TrangThai + N')' AS Hien
                           FROM NHAPVIEN nv JOIN BENHNHAN bn ON bn.MaBN = nv.MaBN
                           WHERE nv.TrangThai <> N'Hủy'
                           ORDER BY nv.NgayNhapVien DESC"),
                "Hien", "MaNhapVien");

            LoadData();
            cmbNV.SelectedIndexChanged += (s, e) => Tinh(true);
            nudKham.ValueChanged += (s, e) => UpdateTong();
            nudDV.ValueChanged += (s, e) => UpdateTong();
            dgv.SelectionChanged += (s, e) =>
            {
                var r = dgv.CurrentRow;
                if (r != null && GetText(r, "Mã NV") != "" && GetValue(cmbNV) != GetText(r, "Mã NV"))
                    cmbNV.SelectedValue = GetText(r, "Mã NV");
            };

            Tinh(true);
        }

        void btnTinhTien_Click(object s, EventArgs e)
        {
            Tinh(false);
        }

        void btnLamMoi_Click(object s, EventArgs e)
        {
            LoadData();
            Tinh(true);
        }

        void LoadData()
        {
            dgv.DataSource = DatabaseHelper.GetData(
                @"SELECT vp.MaVP AS [Mã VP], vp.MaNhapVien AS [Mã NV], bn.HoTen AS [Bệnh nhân],
                         vp.TienPhong AS [Tiền phòng], vp.TienThuoc AS [Tiền thuốc], vp.TienKham AS [Tiền khám],
                         vp.TienDichVu AS [Tiền dịch vụ], vp.TongTien AS [Tổng tiền], vp.NgayLap AS [Ngày lập],
                         vp.TrangThai AS [Trạng thái]
                  FROM VIENPHI vp
                  JOIN NHAPVIEN nv ON nv.MaNhapVien = vp.MaNhapVien
                  JOIN BENHNHAN bn ON bn.MaBN = nv.MaBN
                  ORDER BY vp.NgayLap DESC");
            foreach (var c in new[] { "Tiền phòng", "Tiền thuốc", "Tiền khám", "Tiền dịch vụ", "Tổng tiền" }) FormatColumn(dgv, c, "N0");
            FormatColumn(dgv, "Ngày lập", "dd/MM/yyyy HH:mm");
        }

        /// <summary>Tính tiền phòng + thuốc từ dữ liệu. loadSaved = true: nạp luôn tiền khám/dịch vụ đã lưu.</summary>
        void Tinh(bool loadSaved)
        {
            string nv = GetValue(cmbNV);
            if (nv == null) return;

            var dt = DatabaseHelper.GetData(
                @"SELECT nv.NgayNhapVien, nv.NgayRaVien, bn.MaBN, bn.HoTen AS TenBN,
                         ISNULL(bs.HoTen, N'') AS TenBS, ISNULL(p.TenPhong, N'') AS TenPhong, ISNULL(p.DonGia, 0) AS DonGiaPhong,
                         ISNULL((SELECT SUM(ct.ThanhTien)
                                 FROM CT_DONTHUOC ct
                                 JOIN DONTHUOC d ON d.MaDon = ct.MaDon
                                 JOIN BENHAN ba ON ba.MaBA = d.MaBA
                                 WHERE ba.MaNhapVien = nv.MaNhapVien), 0) AS TienThuoc
                  FROM NHAPVIEN nv
                  JOIN BENHNHAN bn ON bn.MaBN = nv.MaBN
                  LEFT JOIN BACSI bs ON bs.MaBS = nv.MaBS
                  LEFT JOIN GIUONG g ON g.MaGiuong = nv.MaGiuong
                  LEFT JOIN PHONG p ON p.MaPhong = g.MaPhong
                  WHERE nv.MaNhapVien = @nv", DatabaseHelper.Param("@nv", nv));
            if (dt.Rows.Count == 0) return;

            var r = dt.Rows[0];
            vao = (DateTime)r["NgayNhapVien"];
            ra = r["NgayRaVien"] == DBNull.Value ? (DateTime?)null : (DateTime)r["NgayRaVien"];
            maBN = r["MaBN"].ToString();
            tenBN = r["TenBN"].ToString();
            tenBS = r["TenBS"].ToString();
            tenPhong = r["TenPhong"].ToString();
            decimal donGia = Convert.ToDecimal(r["DonGiaPhong"]);

            DateTime den = ra ?? DateTime.Now;
            soNgay = Math.Max(1, (den.Date - vao.Date).Days + 1);
            tienPhong = soNgay * donGia;
            tienThuoc = Convert.ToDecimal(r["TienThuoc"]);

            // hóa đơn đã lưu (nếu có)
            var vp = DatabaseHelper.GetData("SELECT MaVP, TienPhong, TienThuoc, TienKham, TienDichVu, TrangThai FROM VIENPHI WHERE MaNhapVien=@nv", DatabaseHelper.Param("@nv", nv));
            loading = true;
            if (vp.Rows.Count > 0)
            {
                maVP = vp.Rows[0]["MaVP"].ToString();
                trangThai = vp.Rows[0]["TrangThai"].ToString();
                if (trangThai == "Đã thanh toán")   // đã thanh toán: giữ nguyên số liệu đã chốt
                {
                    tienPhong = Convert.ToDecimal(vp.Rows[0]["TienPhong"]);
                    tienThuoc = Convert.ToDecimal(vp.Rows[0]["TienThuoc"]);
                }
                if (loadSaved || trangThai == "Đã thanh toán")
                {
                    nudKham.Value = Convert.ToDecimal(vp.Rows[0]["TienKham"]);
                    nudDV.Value = Convert.ToDecimal(vp.Rows[0]["TienDichVu"]);
                }
            }
            else
            {
                maVP = "";
                trangThai = "Chưa lập";
                if (loadSaved) { nudKham.Value = 0; nudDV.Value = 0; }
            }
            loading = false;

            txtMaVP.Text = maVP;
            txtTrangThai.Text = trangThai;
            txtSoNgay.Text = soNgay.ToString();
            txtDonGia.Text = Fmt.Money(donGia);
            txtPhong.Text = Fmt.Money(tienPhong);
            txtThuoc.Text = Fmt.Money(tienThuoc);
            UpdateTong();
        }

        void UpdateTong()
        {
            if (loading) return;
            txtTong.Text = Fmt.Money(tienPhong + tienThuoc + nudKham.Value + nudDV.Value);
        }

        void btnLuu_Click(object s, EventArgs e)
        {
            string nv = GetValue(cmbNV);
            if (nv == null) { ShowWarning("Hãy chọn hồ sơ nhập viện."); return; }
            try
            {
                Tinh(false);
                if (trangThai == "Đã thanh toán") { ShowWarning("Hóa đơn đã thanh toán, không thể sửa."); return; }

                if (maVP == "")
                {
                    string ma = DatabaseHelper.NextId("VIENPHI", "MaVP", "VP");
                    DatabaseHelper.ExecuteNonQuery(@"INSERT INTO VIENPHI(MaVP, MaNhapVien, TienPhong, TienThuoc, TienKham, TienDichVu, TrangThai)
                              VALUES(@ma, @nv, @phong, @thuoc, @kham, @dv, N'Chưa thanh toán')",
                        DatabaseHelper.Param("@ma", ma), DatabaseHelper.Param("@nv", nv), DatabaseHelper.Param("@phong", tienPhong), DatabaseHelper.Param("@thuoc", tienThuoc),
                        DatabaseHelper.Param("@kham", nudKham.Value), DatabaseHelper.Param("@dv", nudDV.Value));
                }
                else
                {
                    DatabaseHelper.ExecuteNonQuery(@"UPDATE VIENPHI SET TienPhong=@phong, TienThuoc=@thuoc, TienKham=@kham, TienDichVu=@dv, NgayLap=GETDATE()
                              WHERE MaVP=@ma",
                        DatabaseHelper.Param("@phong", tienPhong), DatabaseHelper.Param("@thuoc", tienThuoc),
                        DatabaseHelper.Param("@kham", nudKham.Value), DatabaseHelper.Param("@dv", nudDV.Value), DatabaseHelper.Param("@ma", maVP));
                }
                LoadData();
                Tinh(true);
                ShowInfo("Đã lưu hóa đơn viện phí.");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnThanhToan_Click(object s, EventArgs e)
        {
            if (maVP == "") { ShowWarning("Chưa có hóa đơn. Hãy bấm \"Lưu hóa đơn\" trước."); return; }
            if (trangThai == "Đã thanh toán") { ShowInfo("Hóa đơn này đã thanh toán rồi."); return; }
            if (!Ask("Xác nhận bệnh nhân đã thanh toán hóa đơn " + maVP + "?")) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery("UPDATE VIENPHI SET TrangThai = N'Đã thanh toán' WHERE MaVP = @ma", DatabaseHelper.Param("@ma", maVP));
                LoadData();
                Tinh(true);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnInHoaDon_Click(object s, EventArgs e)
        {
            if (GetValue(cmbNV) == null) { ShowWarning("Hãy chọn hồ sơ nhập viện."); return; }
            try
            {
                Tinh(false);
                decimal tong = tienPhong + tienThuoc + nudKham.Value + nudDV.Value;
                var L = new List<string>();
                L.Add("#HÓA ĐƠN VIỆN PHÍ");
                L.Add("");
                L.Add("Mã hóa đơn : " + (maVP == "" ? "(chưa lưu)" : maVP));
                L.Add("Ngày in    : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                L.Add("Người lập  : " + Session.FullName);
                L.Add("Bệnh nhân  : " + tenBN + " (" + maBN + ")");
                L.Add("Mã nhập viện: " + GetValue(cmbNV));
                L.Add("Bác sĩ     : " + tenBS);
                L.Add("Phòng      : " + tenPhong);
                L.Add("Vào viện   : " + vao.ToString("dd/MM/yyyy HH:mm"));
                L.Add("Ra viện    : " + (ra.HasValue ? ra.Value.ToString("dd/MM/yyyy HH:mm") : "(đang điều trị)"));
                L.Add(new string('-', 60));
                L.Add(Row("Tiền phòng (" + soNgay + " ngày)", tienPhong));
                L.Add(Row("Tiền thuốc", tienThuoc));
                L.Add(Row("Tiền khám", nudKham.Value));
                L.Add(Row("Tiền dịch vụ", nudDV.Value));
                L.Add(new string('-', 60));
                L.Add(Row("TỔNG CỘNG", tong));
                L.Add("");
                L.Add("Trạng thái: " + trangThai);
                L.Add("");
                L.Add("      Người nộp tiền                     Người lập hóa đơn");
                L.Add("      (ký, ghi rõ họ tên)                (ký, ghi rõ họ tên)");

                Export.Preview(Export.TextDoc(L), "Hóa đơn viện phí");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        static string Row(string label, decimal amount)
        {
            return label.PadRight(32) + Fmt.Money(amount).PadLeft(28);
        }
    }
}
