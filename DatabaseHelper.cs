using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Một câu lệnh SQL kèm tham số.
    /// Dùng khi cần chạy NHIỀU câu lệnh cùng lúc trong 1 transaction (xem ExecuteTransaction).
    /// </summary>
    public class SqlStatement
    {
        public string Sql;
        public SqlParameter[] Parameters;

        public SqlStatement(string sql, params SqlParameter[] parameters)
        {
            Sql = sql;
            Parameters = parameters;
        }
    }

    /// <summary>
    /// TẤT CẢ code làm việc với SQL Server nằm trong class này.
    /// Các form KHÔNG tự mở kết nối, chỉ gọi 5 hàm chính:
    ///
    ///   GetData(...)            -> chạy SELECT, trả về DataTable (đổ vào DataGridView / ComboBox)
    ///   ExecuteNonQuery(...)    -> chạy INSERT / UPDATE / DELETE, trả về số dòng bị ảnh hưởng
    ///   ExecuteScalar(...)      -> chạy câu lệnh trả về 1 giá trị duy nhất (COUNT, MAX, SUM...)
    ///   ExecuteTransaction(...) -> chạy nhiều câu lệnh: hoặc thành công hết, hoặc hủy hết
    ///   Param(...)              -> tạo tham số (chống SQL Injection)
    ///
    /// Quy trình chung mỗi lần gọi: mở kết nối -> chạy lệnh -> đóng kết nối (lệnh using tự đóng).
    /// </summary>
    public static class DatabaseHelper
    {
        // >>> SỬA CHUỖI KẾT NỐI Ở ĐÂY cho đúng máy của bạn <<<
        // SQL Server Express : Server=.\SQLEXPRESS
        // SQL Server thường  : Server=.   hoặc   Server=localhost
        // LocalDB            : Server=(localdb)\MSSQLLocalDB
        public static string ConnectionString =
            @"Server=.\SQLEXPRESS;Database=Quanlybenhnhan123456;Integrated Security=True;";

        // ------------------------------------------------------------
        // 1. KẾT NỐI
        // ------------------------------------------------------------

        /// <summary>Tạo một kết nối mới (chưa mở). Nhớ dùng trong khối using.</summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>Kiểm tra có kết nối được SQL Server không. Trả về true/false, không ném lỗi.</summary>
        public static bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // ------------------------------------------------------------
        // 2. THAM SỐ
        // ------------------------------------------------------------

        /// <summary>
        /// Tạo tham số cho câu lệnh SQL, ví dụ Param("@ten", "An").
        /// Chuỗi rỗng hoặc null sẽ được lưu thành NULL trong database.
        /// </summary>
        public static SqlParameter Param(string name, object value)
        {
            var text = value as string;
            if (value == null || (text != null && text.Trim().Length == 0))
                value = DBNull.Value;
            return new SqlParameter(name, value);
        }

        // ------------------------------------------------------------
        // 3. ĐỌC DỮ LIỆU (SELECT)
        // ------------------------------------------------------------

        /// <summary>Chạy SELECT và trả về DataTable.</summary>
        public static DataTable GetData(string sql, params SqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            using (var adapter = new SqlDataAdapter(sql, conn))
            {
                if (parameters != null)
                    adapter.SelectCommand.Parameters.AddRange(parameters);

                var table = new DataTable();
                adapter.Fill(table);   // Fill tự mở và đóng kết nối
                return table;
            }
        }

        /// <summary>Chạy câu lệnh trả về đúng 1 giá trị (ô đầu tiên của dòng đầu tiên).</summary>
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        // ------------------------------------------------------------
        // 4. GHI DỮ LIỆU (INSERT / UPDATE / DELETE)
        // ------------------------------------------------------------

        /// <summary>Chạy INSERT / UPDATE / DELETE. Trả về số dòng bị ảnh hưởng.</summary>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Chạy nhiều câu lệnh trong 1 transaction.
        /// Nếu có bất kỳ câu nào lỗi thì TẤT CẢ bị hủy (Rollback), dữ liệu không bị lệch.
        /// Ví dụ: tạo hồ sơ nhập viện + đổi giường thành "Đang sử dụng" phải đi cùng nhau.
        /// </summary>
        public static void ExecuteTransaction(params SqlStatement[] statements)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var st in statements)
                        {
                            using (var cmd = new SqlCommand(st.Sql, conn, tran))
                            {
                                if (st.Parameters != null)
                                    cmd.Parameters.AddRange(st.Parameters);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();     // tất cả OK -> lưu
                    }
                    catch
                    {
                        tran.Rollback();   // có lỗi -> hủy hết
                        throw;             // ném lỗi lên cho form hiển thị
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // 5. TIỆN ÍCH
        // ------------------------------------------------------------

        /// <summary>
        /// Sinh mã tiếp theo. Ví dụ NextId("BENHNHAN", "MaBN", "BN") -> "BN05"
        /// (lấy số lớn nhất hiện có rồi cộng 1).
        /// </summary>
        public static string NextId(string table, string column, string prefix)
        {
            string sql = "SELECT ISNULL(MAX(CAST(SUBSTRING(" + column + ", " + (prefix.Length + 1) + ", 10) AS INT)), 0) + 1 " +
                         "FROM " + table + " WHERE " + column + " LIKE '" + prefix + "[0-9]%'";
            int next = Convert.ToInt32(ExecuteScalar(sql));
            return prefix + next.ToString("D2");
        }

        /// <summary>Kiểm tra một câu SELECT có trả về dòng nào không (dùng để kiểm tra trùng / tồn tại).</summary>
        public static bool Exists(string sql, params SqlParameter[] parameters)
        {
            return GetData(sql, parameters).Rows.Count > 0;
        }
    }
}
