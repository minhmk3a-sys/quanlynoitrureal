using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>Quản lý bệnh nhân: thêm / sửa / xóa / tìm kiếm.</summary>
    public partial class BenhNhanForm : BaseForm
    {
        public BenhNhanForm()
        {
            InitializeComponent();   // dựng giao diện (code nằm trong BenhNhanForm.Designer.cs)
            cmbGt.SelectedIndex = 0;

            LoadData();
            // Gắn các sự kiện này SAU khi nạp dữ liệu lần đầu để chúng không chạy sớm
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
                @"SELECT MaBN AS [Mã BN], HoTen AS [Họ tên], NgaySinh AS [Ngày sinh], GioiTinh AS [Giới tính],
                         CCCD AS [CCCD], SoDienThoai AS [SĐT], DiaChi AS [Địa chỉ], NgheNghiep AS [Nghề nghiệp],
                         NguoiThan AS [Người thân], SDTNguoiThan AS [SĐT người thân], TinhTrang AS [Tình trạng bệnh]
                  FROM BENHNHAN
                  WHERE MaBN LIKE @k OR HoTen LIKE @k OR CCCD LIKE @k OR SoDienThoai LIKE @k
                  ORDER BY MaBN",
                DatabaseHelper.Param("@k", "%" + txtTim.Text.Trim() + "%"));
            FormatColumn(dgv, "Ngày sinh", "dd/MM/yyyy");
        }

        void FillInputs()
        {
            var r = dgv.CurrentRow;
            if (r == null) return;
            txtMa.Text = GetText(r, "Mã BN");
            txtTen.Text = GetText(r, "Họ tên");
            if (r.Cells["Ngày sinh"].Value is DateTime) dtpNs.Value = (DateTime)r.Cells["Ngày sinh"].Value;
            cmbGt.Text = GetText(r, "Giới tính");
            txtCccd.Text = GetText(r, "CCCD");
            txtSdt.Text = GetText(r, "SĐT");
            txtDiaChi.Text = GetText(r, "Địa chỉ");
            txtNghe.Text = GetText(r, "Nghề nghiệp");
            txtNguoiThan.Text = GetText(r, "Người thân");
            txtSdtNT.Text = GetText(r, "SĐT người thân");
            txtTinhTrang.Text = GetText(r, "Tình trạng bệnh");
        }

        void ClearInputs()
        {
            txtMa.Text = txtTen.Text = txtCccd.Text = txtSdt.Text = txtDiaChi.Text = "";
            txtNghe.Text = txtNguoiThan.Text = txtSdtNT.Text = txtTinhTrang.Text = "";
            dtpNs.Value = DateTime.Today;
            cmbGt.SelectedIndex = 0;
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
            if (txtTen.Text.Trim() == "") { ShowWarning("Vui lòng nhập họ tên."); txtTen.Focus(); return false; }
            if (dtpNs.Value.Date > DateTime.Today) { ShowWarning("Ngày sinh không hợp lệ."); return false; }
            string c = txtCccd.Text.Trim();
            if (c != "" && (c.Length != 12 || !c.All(char.IsDigit))) { ShowWarning("CCCD phải gồm đúng 12 chữ số."); txtCccd.Focus(); return false; }
            return true;
        }

        SqlParameter[] Ps(string ma)
        {
            return new[]
            {
                DatabaseHelper.Param("@ma", ma), DatabaseHelper.Param("@ten", txtTen.Text.Trim()), DatabaseHelper.Param("@ns", dtpNs.Value.Date),
                DatabaseHelper.Param("@gt", cmbGt.Text), DatabaseHelper.Param("@cccd", txtCccd.Text.Trim()), DatabaseHelper.Param("@sdt", txtSdt.Text.Trim()),
                DatabaseHelper.Param("@dc", txtDiaChi.Text.Trim()), DatabaseHelper.Param("@nghe", txtNghe.Text.Trim()),
                DatabaseHelper.Param("@nt", txtNguoiThan.Text.Trim()), DatabaseHelper.Param("@sdtnt", txtSdtNT.Text.Trim()),
                DatabaseHelper.Param("@tt", txtTinhTrang.Text.Trim())
            };
        }

        void btnThem_Click(object s, EventArgs e)
        {
            if (!Valid()) return;
            try
            {
                string ma = DatabaseHelper.NextId("BENHNHAN", "MaBN", "BN");
                DatabaseHelper.ExecuteNonQuery(@"INSERT INTO BENHNHAN(MaBN, HoTen, NgaySinh, GioiTinh, CCCD, SoDienThoai, DiaChi, NgheNghiep, NguoiThan, SDTNguoiThan, TinhTrang)
                          VALUES(@ma, @ten, @ns, @gt, @cccd, @sdt, @dc, @nghe, @nt, @sdtnt, @tt)", Ps(ma));
                Reset();
                ShowInfo("Đã thêm bệnh nhân " + ma + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnSua_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn bệnh nhân cần sửa trong danh sách."); return; }
            if (!Valid()) return;
            try
            {
                string ma = txtMa.Text;
                DatabaseHelper.ExecuteNonQuery(@"UPDATE BENHNHAN SET HoTen=@ten, NgaySinh=@ns, GioiTinh=@gt, CCCD=@cccd, SoDienThoai=@sdt,
                                 DiaChi=@dc, NgheNghiep=@nghe, NguoiThan=@nt, SDTNguoiThan=@sdtnt, TinhTrang=@tt
                          WHERE MaBN=@ma", Ps(ma));
                LoadData();
                ShowInfo("Đã cập nhật bệnh nhân " + ma + ".");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        void btnXoa_Click(object s, EventArgs e)
        {
            if (txtMa.Text == "") { ShowWarning("Hãy chọn bệnh nhân cần xóa."); return; }
            if (!Ask("Xóa bệnh nhân " + txtMa.Text + " - " + txtTen.Text + "?")) return;
            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM BENHNHAN WHERE MaBN=@ma", DatabaseHelper.Param("@ma", txtMa.Text));
                Reset();
            }
            catch (Exception ex) { ShowError(ex, "Bệnh nhân này đã có hồ sơ nhập viện nên không thể xóa."); }
        }
    }
}
