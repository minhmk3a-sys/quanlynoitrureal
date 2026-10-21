using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// Form cơ sở: chứa các hàm tiện ích dùng chung (hiện thông báo, đọc ô của lưới, đổ dữ liệu vào ComboBox).
    /// Các form như BenhNhanForm kế thừa từ đây để dùng lại. Giao diện của từng form nằm trong file .Designer.cs.
    /// </summary>
    public class BaseForm : Form
    {
        // ---------- thông báo ----------

        protected void ShowInfo(string message)
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected void ShowWarning(string message)
        {
            MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>Hỏi Có/Không. Trả về true nếu người dùng chọn Có.</summary>
        protected bool Ask(string message)
        {
            return MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>Hiện lỗi thân thiện. Lỗi 547 = đang được dùng ở bảng khác (khóa ngoại), 2627/2601 = trùng dữ liệu.</summary>
        protected void ShowError(Exception ex, string foreignKeyMessage = null)
        {
            var sqlEx = ex as SqlException;
            if (sqlEx != null && sqlEx.Number == 547)
                ShowWarning(foreignKeyMessage ?? "Dữ liệu đang được dùng ở nơi khác hoặc vi phạm ràng buộc, không thể thực hiện.");
            else if (sqlEx != null && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                ShowWarning("Dữ liệu bị trùng.");
            else
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ---------- tiện ích dữ liệu ----------

        /// <summary>Đọc giá trị một ô của dòng trong DataGridView (null -> chuỗi rỗng).</summary>
        protected static string GetText(DataGridViewRow row, string column)
        {
            if (row == null || !row.DataGridView.Columns.Contains(column)) return "";
            object value = row.Cells[column].Value;
            return (value == null || value == DBNull.Value) ? "" : value.ToString();
        }

        /// <summary>Đặt định dạng hiển thị cho một cột (ví dụ ngày: "dd/MM/yyyy").</summary>
        protected static void FormatColumn(DataGridView grid, string column, string format)
        {
            if (grid.Columns.Contains(column)) grid.Columns[column].DefaultCellStyle.Format = format;
        }

        /// <summary>Đổ DataTable vào ComboBox: display = cột hiển thị, value = cột giá trị (mã).</summary>
        protected void FillCombo(ComboBox combo, DataTable table, string display, string value)
        {
            combo.DataSource = null;
            combo.DisplayMember = display;
            combo.ValueMember = value;
            combo.DataSource = table;
        }

        /// <summary>Lấy mã đang chọn trong ComboBox (null nếu chưa chọn).</summary>
        protected static string GetValue(ComboBox combo)
        {
            return combo.SelectedValue == null ? null : combo.SelectedValue.ToString();
        }
    }
}
