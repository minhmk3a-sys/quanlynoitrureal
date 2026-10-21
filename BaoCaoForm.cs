using System;
using System.Diagnostics;
using System.Data;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>Báo cáo dạng bảng: xem, in, xuất PDF, xuất Excel.</summary>
    public partial class BaoCaoForm : BaseForm
    {
        DataTable current;   // dữ liệu của báo cáo đang hiển thị

        static readonly string[] Names =
        {
            "Danh sách bệnh nhân đang điều trị",
            "Danh sách tất cả bệnh nhân",
            "Danh sách hồ sơ nhập viện",
            "Danh sách viện phí"
        };

        static readonly string[] Queries =
        {
            @"SELECT bn.MaBN AS [Mã BN], bn.HoTen AS [Họ tên], bn.NgaySinh AS [Ngày sinh], bn.GioiTinh AS [Giới tính],
                     k.TenKhoa AS [Khoa], p.TenPhong AS [Phòng], g.SoGiuong AS [Giường], bs.HoTen AS [Bác sĩ],
                     nv.NgayNhapVien AS [Ngày vào viện], bn.TinhTrang AS [Tình trạng bệnh]
              FROM NHAPVIEN nv
              JOIN BENHNHAN bn ON bn.MaBN = nv.MaBN
              LEFT JOIN BACSI bs ON bs.MaBS = nv.MaBS
              LEFT JOIN GIUONG g ON g.MaGiuong = nv.MaGiuong
              LEFT JOIN PHONG p ON p.MaPhong = g.MaPhong
              LEFT JOIN KHOA k ON k.MaKhoa = p.MaKhoa
              WHERE nv.TrangThai = N'Đang điều trị'
              ORDER BY nv.NgayNhapVien",

            @"SELECT MaBN AS [Mã BN], HoTen AS [Họ tên], NgaySinh AS [Ngày sinh], GioiTinh AS [Giới tính], CCCD AS [CCCD],
                     SoDienThoai AS [SĐT], DiaChi AS [Địa chỉ], NgheNghiep AS [Nghề nghiệp], TinhTrang AS [Tình trạng bệnh]
              FROM BENHNHAN ORDER BY MaBN",

            @"SELECT nv.MaNhapVien AS [Mã NV], bn.HoTen AS [Bệnh nhân], bs.HoTen AS [Bác sĩ], p.TenPhong AS [Phòng],
                     nv.MaGiuong AS [Giường], nv.NgayNhapVien AS [Ngày vào viện], nv.NgayRaVien AS [Ngày ra viện],
                     nv.LyDoNhapVien AS [Lý do], nv.TrangThai AS [Trạng thái]
              FROM NHAPVIEN nv
              JOIN BENHNHAN bn ON bn.MaBN = nv.MaBN
              LEFT JOIN BACSI bs ON bs.MaBS = nv.MaBS
              LEFT JOIN GIUONG g ON g.MaGiuong = nv.MaGiuong
              LEFT JOIN PHONG p ON p.MaPhong = g.MaPhong
              ORDER BY nv.NgayNhapVien DESC",

            @"SELECT vp.MaVP AS [Mã VP], bn.HoTen AS [Bệnh nhân], vp.TienPhong AS [Tiền phòng], vp.TienThuoc AS [Tiền thuốc],
                     vp.TienKham AS [Tiền khám], vp.TienDichVu AS [Tiền dịch vụ], vp.TongTien AS [Tổng tiền],
                     vp.NgayLap AS [Ngày lập], vp.TrangThai AS [Trạng thái]
              FROM VIENPHI vp
              JOIN NHAPVIEN nv ON nv.MaNhapVien = vp.MaNhapVien
              JOIN BENHNHAN bn ON bn.MaBN = nv.MaBN
              ORDER BY vp.NgayLap DESC"
        };

        public BaoCaoForm()
        {
            InitializeComponent();   // dựng giao diện (code nằm trong BaoCaoForm.Designer.cs)
            cmbBC.Items.AddRange(Names);
            cmbBC.SelectedIndex = 0;
            LoadReport();
            cmbBC.SelectedIndexChanged += (s, e) => LoadReport();
        }

        void LoadReport()
        {
            try
            {
                current = DatabaseHelper.GetData(Queries[cmbBC.SelectedIndex]);
                dgv.DataSource = current;
                foreach (DataGridViewColumn c in dgv.Columns)
                    if (c.ValueType == typeof(decimal)) c.DefaultCellStyle.Format = "N0";
                FormatColumn(dgv, "Ngày sinh", "dd/MM/yyyy");
                FormatColumn(dgv, "Ngày vào viện", "dd/MM/yyyy HH:mm");
                FormatColumn(dgv, "Ngày ra viện", "dd/MM/yyyy HH:mm");
                FormatColumn(dgv, "Ngày lập", "dd/MM/yyyy HH:mm");
            }
            catch (Exception ex) { ShowError(ex); }
        }

        string TieuDe { get { return cmbBC.Text.ToUpper(); } }

        void btnXem_Click(object s, EventArgs e)
        {
            LoadReport();
        }

        void btnIn_Click(object s, EventArgs e)
        {
            if (current == null) return;
            Export.Preview(Export.TableDoc(TieuDe, current), "Xem trước khi in");
        }

        void btnPdf_Click(object s, EventArgs e)
        {
            if (current == null) return;
            using (var dlg = new SaveFileDialog { Filter = "PDF (*.pdf)|*.pdf", FileName = "BaoCao_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".pdf" })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                try
                {
                    if (!Export.ExportPdf(Export.TableDoc(TieuDe, current), dlg.FileName))
                    {
                        ShowWarning("Máy chưa có máy in \"Microsoft Print to PDF\".\nHãy bấm \"In danh sách\" rồi chọn máy in PDF bất kỳ để lưu.");
                        return;
                    }
                    ShowInfo("Đã xuất PDF:\n" + dlg.FileName);
                    TryOpen(dlg.FileName);
                }
                catch (Exception ex) { ShowError(ex); }
            }
        }

        void btnExcel_Click(object s, EventArgs e)
        {
            if (current == null) return;
            using (var dlg = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = "BaoCao_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx" })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                try
                {
                    Export.WriteXlsx(dlg.FileName, current);
                    ShowInfo("Đã xuất Excel:\n" + dlg.FileName);
                    TryOpen(dlg.FileName);
                }
                catch (Exception ex) { ShowError(ex); }
            }
        }

        static void TryOpen(string path)
        {
            try { Process.Start(path); } catch { }
        }
    }
}
