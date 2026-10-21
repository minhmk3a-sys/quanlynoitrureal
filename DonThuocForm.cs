using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>Đơn thuốc: chọn bệnh án -> chọn / tạo đơn -> thêm thuốc. Thêm / sửa / xóa thuốc tự trừ / cộng lại tồn kho (transaction).</summary>
    public partial class DonThuocForm : BaseForm
    {
        bool loading;

        public DonThuocForm()
        {
            InitializeComponent();   // dựng giao diện (code nằm trong DonThuocForm.Designer.cs)
            LoadThuoc();
            FillCombo(cmbBA,
                DatabaseHelper.GetData(@"SELECT ba.MaBA, ba.MaBA + N' - ' + bn.HoTen + N' - ' + ISNULL(ba.ChanDoan, N'') AS Hien
                           FROM BENHAN ba
                           JOIN NHAPVIEN nv ON nv.MaNhapVien = ba.MaNhapVien
                           JOIN BENHNHAN bn ON bn.MaBN = nv.MaBN
                           ORDER BY ba.NgayLap DESC, ba.MaBA DESC"),
                "Hien", "MaBA");

            cmbBA.SelectedIndexChanged += (s, e) => { if (!loading) LoadDon(null); };
            cmbDon.SelectedIndexChanged += (s, e) => { if (!loading) LoadCT(); };
            cmbThuoc.SelectedIndexChanged += (s, e) => ShowGia();
            dgv.SelectionChanged += (s, e) => FillFromGrid();

            LoadDon(null);
        }

        // ---------- nạp dữ liệu ----------

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

        void ShowGia()
        {
            var drv = cmbThuoc.SelectedItem as DataRowView;
            lblGia.Text = drv == null ? "0 đ" : Fmt.Money(Convert.ToDecimal(drv["DonGia"]));
        }

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

        // ---------- nghiệp vụ ----------

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

        int TonKho(string maThuoc)
        {
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar("SELECT SoLuongTon FROM THUOC WHERE MaThuoc=@t", DatabaseHelper.Param("@t", maThuoc)));
        }

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
