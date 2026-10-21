using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Quản lý thuốc: thêm / sửa / xóa / tìm kiếm.
    /// Bao gồm đơn giá, số lượng tồn kho, và % bảo hiểm.
    /// </summary>
    public partial class ThuocForm : BaseForm
    {
        public ThuocForm()
        {
            InitializeComponent();
            LoadData();
            dgv.SelectionChanged += (s, e) => FillInputs();
            txtTim.TextChanged += (s, e) => LoadData();
        }

        void LoadData()
        {
            dgv.DataSource = DatabaseHelper.GetData(
                @"SELECT MaThuoc AS [Mã thuốc], TenThuoc AS [Tên thuốc],
                         SoLuongTon AS [Tồn kho], DonGia AS [Đơn giá],
                         PT_BaoHiem AS [% BH]
                  FROM THUOC
                  WHERE MaThuoc LIKE @k OR TenThuoc LIKE @k
                  ORDER BY MaThuoc",
                DatabaseHelper.Param("@k", "%" + txtTim.Text.Trim() + "%"));
            FormatColumn(dgv, "Đơn giá", "N0");
        }

        void FillInputs()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;
            txtMa.Text = GetText(r, "Mã thuốc");
            txtTen.Text = GetText(r, "Tên thuốc");
            txtTonKho.Text = GetText(r, "Tồn kho");
            txtDonGia.Text = GetText(r, "Đơn giá");
            txtBH.Text = GetText(r, "% BH");
        }

        void ClearInputs()
        {
            txtMa.Text = txtTen.Text = txtTonKho.Text = txtDonGia.Text = txtBH.Text = "";
        }

        void Reset()
        {
            txtTim.Text = "";
            LoadData();
            dgv.CurrentCell = null;
            ClearInputs();
        }

        void btnLamMoi_Click(object s, EventArgs e) { Reset(); }

        bool Valid()
        {
            if (txtTen.Text.Trim() == "") { ShowWarning("Vui lòng nhập tên thuốc."); txtTen.Focus(); return false; }
            int sl;
            if (!int.TryParse(txtTonKho.Text, out sl) || sl < 0)
            { ShowWarning("Tồn kho phải là số nguyên >= 0."); txtTonKho.Focus(); return false; }
            decimal dg;
            if (!decimal.TryParse(txtDonGia.Text, out dg) || dg < 0)
            { ShowWarning("Đơn giá phải là số >= 0."); txtDonGia.Focus(); return false; }
            return true;
        }

        SqlParameter[] Ps(string ma)
        {
            double? bh = null;
            double bhVal;
            if (double.TryParse(txtBH.Text, out bhVal)) bh = bhVal;

            return new[]
            {
                DatabaseHelper.Param("@ma", ma),
                DatabaseHelper.Param("@ten", txtTen.Text.Trim()),
                DatabaseHelper.Param("@sl", int.Parse(txtTonKho.Text)),
                DatabaseHelper.Param("@gia", decimal.Parse(txtDonGia.Text)),
                DatabaseHelper.Param("@bh", (object)bh ?? DBNull.Value)
            };
        }

        void btnThem_Click(object s, EventArgs e)
        {
            if (!Valid()) return;
            try
            {
                string ma = DatabaseHelper.NextId("THUOC", "MaThuoc", "TH");
                DatabaseHelper.ExecuteNonQuery(
                    "INSERT INTO THUOC(MaThuoc, TenThuoc, SoLuongTon, DonGia, PT_BaoHiem) VALUES(@ma, @ten, @sl, @gia, @bh)", Ps(ma));
                Reset();
                ShowInfo("Đã thêm thuốc " + ma + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnSua_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn thuốc cần sửa."); return; }
            if (!Valid()) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery(
                    "UPDATE THUOC SET TenThuoc=@ten, SoLuongTon=@sl, DonGia=@gia, PT_BaoHiem=@bh WHERE MaThuoc=@ma", Ps(txtMa.Text));
                LoadData();
                ShowInfo("Đã cập nhật thuốc " + txtMa.Text + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnXoa_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn thuốc cần xóa."); return; }
            if (!Ask("Xóa thuốc " + txtMa.Text + " - " + txtTen.Text + "?")) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM THUOC WHERE MaThuoc=@ma", DatabaseHelper.Param("@ma", txtMa.Text));
                Reset();
            }
            catch (Exception ex)
            {
                ShowError(ex, "Thuốc này đang được sử dụng trong đơn thuốc nên không thể xóa.");
            }
        }
    }
}
