using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Form chính: menu bên trái, form con được nhúng vào pnlContent bên phải.
    /// Phân quyền: nút menu nào không thuộc vai trò của người đăng nhập sẽ bị ẩn.
    /// </summary>
    public partial class MainForm : Form
    {
        public bool LoggedOut;   // true = người dùng bấm Đăng xuất (Program.cs sẽ hiện lại màn hình đăng nhập)

        static readonly Color MenuColor = Color.FromArgb(30, 58, 95);
        static readonly Color MenuActive = Color.FromArgb(52, 101, 164);
        readonly Button[] menuButtons;

        public MainForm()
        {
            InitializeComponent();   // dựng giao diện (code nằm trong MainForm.Designer.cs)

            lblUser.Text = "Xin chào, " + Session.FullName + " (Quản trị viên)  ";
            menuButtons = new[] { 
                btnBenhNhan, btnNhapVien, btnBenhAn, btnDonThuoc, btnVienPhi, btnXuatVien,
                btnKhoa, btnPhong, btnGiuong, btnThuoc, btnDichVu, btnBacSi, btnBaoCao 
            };

            // Bỏ phân quyền: Toàn bộ chức năng luôn hiển thị đầy đủ cho quản trị viên
            foreach (var btn in menuButtons)
            {
                btn.Visible = true;
            }

            // Mở sẵn chức năng đầu tiên
            Shown += (s, e) =>
            {
                foreach (var b in menuButtons)
                    if (b.Visible) { b.PerformClick(); break; }
            };
        }

        // Mỗi nút menu chỉ việc mở form tương ứng
        void btnBenhNhan_Click(object sender, EventArgs e) { OpenChild(() => new BenhNhanForm(), btnBenhNhan, "Bệnh nhân"); }
        void btnNhapVien_Click(object sender, EventArgs e) { OpenChild(() => new NhapVienForm(), btnNhapVien, "Nhập viện"); }
        void btnBenhAn_Click(object sender, EventArgs e) { OpenChild(() => new BenhAnForm(), btnBenhAn, "Bệnh án"); }
        void btnDonThuoc_Click(object sender, EventArgs e) { OpenChild(() => new DonThuocForm(), btnDonThuoc, "Đơn thuốc"); }
        void btnVienPhi_Click(object sender, EventArgs e) { OpenChild(() => new VienPhiForm(), btnVienPhi, "Viện phí"); }
        void btnXuatVien_Click(object sender, EventArgs e) { OpenChild(() => new XuatVienForm(), btnXuatVien, "Quản lý xuất viện"); }
        void btnKhoa_Click(object sender, EventArgs e) { OpenChild(() => new KhoaForm(), btnKhoa, "Quản lý khoa"); }
        void btnPhong_Click(object sender, EventArgs e) { OpenChild(() => new PhongForm(), btnPhong, "Quản lý phòng"); }
        void btnGiuong_Click(object sender, EventArgs e) { OpenChild(() => new GiuongForm(), btnGiuong, "Quản lý giường"); }
        void btnThuoc_Click(object sender, EventArgs e) { OpenChild(() => new ThuocForm(), btnThuoc, "Quản lý thuốc"); }
        void btnDichVu_Click(object sender, EventArgs e) { OpenChild(() => new DichVuForm(), btnDichVu, "Quản lý dịch vụ"); }
        void btnBacSi_Click(object sender, EventArgs e) { OpenChild(() => new BacSiForm(), btnBacSi, "Quản lý bác sĩ"); }
        void btnBaoCao_Click(object sender, EventArgs e) { OpenChild(() => new BaoCaoForm(), btnBaoCao, "Báo cáo"); }

        void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Đăng xuất khỏi hệ thống?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoggedOut = true;
                Close();
            }
        }

        /// <summary>
        /// Nhúng một form con vào pnlContent. "create" là hàm tạo form
        /// (tạo trước để nếu lỗi kết nối thì giữ nguyên màn hình cũ).
        /// </summary>
        void OpenChild(Func<Form> create, Button clicked, string title)
        {
            try
            {
                Form child = create();

                // đổi màu nút đang chọn
                foreach (var b in menuButtons) b.BackColor = MenuColor;
                clicked.BackColor = MenuActive;

                // gỡ form cũ
                while (pnlContent.Controls.Count > 0)
                {
                    Control old = pnlContent.Controls[0];
                    pnlContent.Controls.Remove(old);
                    old.Dispose();
                }

                // nhúng form mới
                child.TopLevel = false;
                child.FormBorderStyle = FormBorderStyle.None;
                child.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(child);
                lblTitle.Text = title;
                child.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được chức năng này:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
