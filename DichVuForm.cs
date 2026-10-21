using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Quản lý dịch vụ (X-quang, siêu âm...): thêm / sửa / xóa / tìm kiếm.
    /// Bao gồm đơn giá gốc và % bảo hiểm.
    /// </summary>
    public partial class DichVuForm : BaseForm
    {
        public DichVuForm()
        {
            InitializeComponent();
            LoadData();
            dgv.SelectionChanged += (s, e) => FillInputs();
            txtTim.TextChanged += (s, e) => LoadData();
        }

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

        void ClearInputs()
        {
            txtMa.Text = txtTen.Text = txtDonGia.Text = txtBH.Text = txtGhiChu.Text = "";
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
            if (txtTen.Text.Trim() == "") { ShowWarning("Vui lòng nhập tên dịch vụ."); txtTen.Focus(); return false; }
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
                DatabaseHelper.Param("@gia", decimal.Parse(txtDonGia.Text)),
                DatabaseHelper.Param("@bh", (object)bh ?? DBNull.Value),
                DatabaseHelper.Param("@gc", txtGhiChu.Text.Trim())
            };
        }

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
