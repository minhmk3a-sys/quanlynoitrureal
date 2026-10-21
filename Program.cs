using System;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true)
            {
                using (var login = new LoginForm())
                {
                    if (login.ShowDialog() != DialogResult.OK) break;
                }

                using (var main = new MainForm())
                {
                    Application.Run(main);
                    if (!main.LoggedOut) break;   // đóng cửa sổ = thoát; Đăng xuất = quay lại màn hình đăng nhập
                }
            }
        }
    }
}
