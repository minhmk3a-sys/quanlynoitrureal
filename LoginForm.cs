using System;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();   // dựng giao diện (code nằm trong LoginForm.Designer.cs)
        }

        void btnLogin_Click(object sender, EventArgs e)
        {
            // Bước 1: kiểm tra kết nối để báo lỗi rõ ràng
            if (!DatabaseHelper.TestConnection())
            {
                MessageBox.Show("Không kết nối được SQL Server.\nKiểm tra ConnectionString trong file DatabaseHelper.cs và đã chạy Database.sql chưa.",
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Bước 2: tìm tài khoản khớp (phân biệt hoa/thường mật khẩu)
                var table = DatabaseHelper.GetData(
                    "SELECT TenDangNhap, HoTen, VaiTro FROM TAIKHOAN " +
                    "WHERE TenDangNhap = @u AND MatKhau COLLATE Latin1_General_CS_AS = @p AND TrangThai = 1",
                    DatabaseHelper.Param("@u", txtUser.Text.Trim()), DatabaseHelper.Param("@p", txtPass.Text));

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu.", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Bước 3: lưu thông tin người đăng nhập rồi đóng form với kết quả OK
                Session.User = table.Rows[0]["TenDangNhap"].ToString();
                Session.FullName = table.Rows[0]["HoTen"].ToString();
                Session.Role = table.Rows[0]["VaiTro"].ToString();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
