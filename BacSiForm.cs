using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>Quản lý bác sĩ: thêm / sửa / xóa / tìm kiếm.</summary>
    public partial class BacSiForm : BaseForm
    {
        public BacSiForm()
        {
            InitializeComponent();   // dựng giao diện (code nằm trong BacSiForm.Designer.cs)
            cmbGt.SelectedIndex = 0;
            FillCombo(cmbKhoa, DatabaseHelper.GetData("SELECT MaKhoa, TenKhoa FROM KHOA ORDER BY MaKhoa"), "TenKhoa", "MaKhoa");

            LoadData();
            dgv.SelectionChanged += (s, e) => FillInputs();
            txtTim.TextChanged += (s, e) => LoadData();
        }

        void btnLamMoi_Click(object s, EventArgs e)
        {
            Reset();
        }

        void LoadData()
        {
            dgv.DataSource = DatabaseHelper.GetData(
                @"SELECT b.MaBS AS [Mã BS], b.HoTen AS [Họ tên], b.NgaySinh AS [Ngày sinh], b.GioiTinh AS [Giới tính],
                         b.ChuyenKhoa AS [Chuyên khoa], b.SoDienThoai AS [SĐT], b.Email AS [Email],
                         b.MaKhoa AS [Mã khoa], k.TenKhoa AS [Khoa], b.TrangThai AS [Đang làm việc]
                  FROM BACSI b LEFT JOIN KHOA k ON k.MaKhoa = b.MaKhoa
                  WHERE b.MaBS LIKE @k OR b.HoTen LIKE @k OR b.ChuyenKhoa LIKE @k
                  ORDER BY b.MaBS",
                DatabaseHelper.Param("@k", "%" + txtTim.Text.Trim() + "%"));
            FormatColumn(dgv, "Ngày sinh", "dd/MM/yyyy");
        }

        void FillInputs()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;
            txtMa.Text = GetText(r, "Mã BS");
            txtTen.Text = GetText(r, "Họ tên");
            if (r.Cells["Ngày sinh"].Value is DateTime) dtpNs.Value = (DateTime)r.Cells["Ngày sinh"].Value;
            cmbGt.Text = GetText(r, "Giới tính");
            txtChuyenKhoa.Text = GetText(r, "Chuyên khoa");
            txtSdt.Text = GetText(r, "SĐT");
            txtEmail.Text = GetText(r, "Email");
            if (GetText(r, "Mã khoa") != "") cmbKhoa.SelectedValue = GetText(r, "Mã khoa");
            chkLam.Checked = r.Cells["Đang làm việc"].Value is bool && (bool)r.Cells["Đang làm việc"].Value;
        }

        void ClearInputs()
        {
            txtMa.Text = txtTen.Text = txtChuyenKhoa.Text = txtSdt.Text = txtEmail.Text = "";
            dtpNs.Value = DateTime.Today;
            cmbGt.SelectedIndex = 0;
            chkLam.Checked = true;
        }

        void Reset()
        {
            txtTim.Text = "";
            LoadData();
            dgv.CurrentCell = null;
            ClearInputs();
        }

        bool Valid()
        {
            if (txtTen.Text.Trim() == "") { ShowWarning("Vui lòng nhập họ tên bác sĩ."); txtTen.Focus(); return false; }
            if (GetValue(cmbKhoa) == null) { ShowWarning("Vui lòng chọn khoa."); return false; }
            return true;
        }

        SqlParameter[] Ps(string ma)
        {
            return new[]
            {
                DatabaseHelper.Param("@ma", ma), DatabaseHelper.Param("@ten", txtTen.Text.Trim()), DatabaseHelper.Param("@ns", dtpNs.Value.Date),
                DatabaseHelper.Param("@gt", cmbGt.Text), DatabaseHelper.Param("@ck", txtChuyenKhoa.Text.Trim()), DatabaseHelper.Param("@sdt", txtSdt.Text.Trim()),
                DatabaseHelper.Param("@email", txtEmail.Text.Trim()), DatabaseHelper.Param("@khoa", GetValue(cmbKhoa)), DatabaseHelper.Param("@tt", chkLam.Checked)
            };
        }

        void btnThem_Click(object s, EventArgs e)
        {
            if (!Valid()) return;
            try
            {
                string ma = DatabaseHelper.NextId("BACSI", "MaBS", "BS");
                DatabaseHelper.ExecuteNonQuery(@"INSERT INTO BACSI(MaBS, HoTen, NgaySinh, GioiTinh, ChuyenKhoa, SoDienThoai, Email, MaKhoa, TrangThai)
                          VALUES(@ma, @ten, @ns, @gt, @ck, @sdt, @email, @khoa, @tt)", Ps(ma));
                Reset();
                ShowInfo("Đã thêm bác sĩ " + ma + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnSua_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn bác sĩ cần sửa trong danh sách."); return; }
            if (!Valid()) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery(@"UPDATE BACSI SET HoTen=@ten, NgaySinh=@ns, GioiTinh=@gt, ChuyenKhoa=@ck, SoDienThoai=@sdt,
                                 Email=@email, MaKhoa=@khoa, TrangThai=@tt
                          WHERE MaBS=@ma", Ps(txtMa.Text));
                LoadData();
                ShowInfo("Đã cập nhật bác sĩ " + txtMa.Text + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnXoa_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn bác sĩ cần xóa."); return; }
            if (!Ask("Xóa bác sĩ " + txtMa.Text + " - " + txtTen.Text + "?")) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM BACSI WHERE MaBS=@ma", DatabaseHelper.Param("@ma", txtMa.Text));
                Reset();
            }
            catch (Exception ex)
            {
                ShowError(ex, "Bác sĩ này đã phụ trách hồ sơ nhập viện nên không thể xóa.\nHãy bỏ chọn \"Đang làm việc\" rồi bấm Sửa.");
            }
        }
    }
}
