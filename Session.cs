using System;
using System.Globalization;
using System.Text;

namespace QuanLyNoiTru
{
    /// <summary>Thông tin người đang đăng nhập (lưu tạm trong bộ nhớ).</summary>
    public static class Session
    {
        public static string User, FullName, Role;

        /// <summary>Kiểm tra vai trò hiện tại có nằm trong danh sách vai trò cho phép không.</summary>
        public static bool Is(params string[] roles)
        {
            string me = (Role ?? "").Normalize(NormalizationForm.FormC);
            foreach (var r in roles)
                if (string.Equals(r.Normalize(NormalizationForm.FormC), me, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
    }

    /// <summary>Định dạng hiển thị.</summary>
    public static class Fmt
    {
        static readonly CultureInfo Vn = new CultureInfo("vi-VN");

        /// <summary>1500000 -> "1.500.000 đ"</summary>
        public static string Money(decimal d) { return d.ToString("N0", Vn) + " đ"; }
    }
}
