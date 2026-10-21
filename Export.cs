using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace QuanLyNoiTru
{
    /// <summary>
    /// In ấn + xuất file, KHÔNG cần thư viện ngoài:
    ///  - In / xem trước: PrintDocument + PrintPreviewDialog
    ///  - Xuất PDF: in ra máy in ảo "Microsoft Print to PDF" (có sẵn trên Windows 10/11)
    ///  - Xuất Excel: tự ghi file .xlsx (OpenXML)
    /// </summary>
    public static class Export
    {
        static readonly CultureInfo Vn = new CultureInfo("vi-VN");

        public static string Cell(object v)
        {
            if (v == null || v == DBNull.Value) return "";
            if (v is DateTime)
            {
                var d = (DateTime)v;
                return d.TimeOfDay.Ticks == 0 ? d.ToString("dd/MM/yyyy") : d.ToString("dd/MM/yyyy HH:mm");
            }
            if (v is bool) return (bool)v ? "Có" : "Không";
            if (v is decimal) return ((decimal)v).ToString("N0", Vn);
            return v.ToString();
        }

        // ---------------- in bảng dữ liệu ----------------

        public static PrintDocument TableDoc(string title, DataTable dt)
        {
            var doc = new PrintDocument();
            doc.DefaultPageSettings.Landscape = true;
            doc.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
            doc.DocumentName = title;

            int row = 0;
            float[] weights = null;
            doc.BeginPrint += (s, e) => { row = 0; weights = Weights(dt); };
            doc.PrintPage += (s, e) =>
            {
                Graphics g = e.Graphics;
                Rectangle b = e.MarginBounds;
                using (Font fT = new Font("Arial", 15, FontStyle.Bold), fH = new Font("Arial", 9, FontStyle.Bold), fB = new Font("Arial", 9))
                using (var fmt = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
                {
                    float y = b.Top;
                    g.DrawString(title, fT, Brushes.Black, b.Left, y); y += 30;
                    g.DrawString("Ngày in: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "   -   Người in: " + Session.FullName,
                        fB, Brushes.Black, b.Left, y);
                    y += 28;

                    float total = 0;
                    foreach (float w in weights) total += w;
                    var xs = new float[weights.Length + 1];
                    xs[0] = b.Left;
                    for (int i = 0; i < weights.Length; i++) xs[i + 1] = xs[i] + b.Width * weights[i] / total;

                    const float rh = 24;
                    g.FillRectangle(Brushes.LightSteelBlue, b.Left, y, b.Width, rh);
                    for (int i = 0; i < weights.Length; i++)
                        g.DrawString(dt.Columns[i].ColumnName, fH, Brushes.Black,
                            new RectangleF(xs[i] + 2, y + 4, xs[i + 1] - xs[i] - 4, rh), fmt);
                    y += rh;

                    while (row < dt.Rows.Count && y + rh <= b.Bottom)
                    {
                        for (int i = 0; i < weights.Length; i++)
                            g.DrawString(Cell(dt.Rows[row][i]), fB, Brushes.Black,
                                new RectangleF(xs[i] + 2, y + 4, xs[i + 1] - xs[i] - 4, rh), fmt);
                        g.DrawLine(Pens.LightGray, b.Left, y + rh, b.Right, y + rh);
                        y += rh;
                        row++;
                    }
                    e.HasMorePages = row < dt.Rows.Count;
                }
            };
            return doc;
        }

        static float[] Weights(DataTable dt)
        {
            var w = new float[dt.Columns.Count];
            for (int i = 0; i < w.Length; i++)
            {
                int len = dt.Columns[i].ColumnName.Length;
                for (int r = 0; r < Math.Min(dt.Rows.Count, 50); r++)
                    len = Math.Max(len, Cell(dt.Rows[r][i]).Length);
                w[i] = Math.Max(6, Math.Min(len, 28));
            }
            return w;
        }

        // ---------------- in văn bản (hóa đơn) ----------------
        // Dòng bắt đầu bằng '#' được in chữ to, đậm (tiêu đề).

        public static PrintDocument TextDoc(IList<string> lines)
        {
            var doc = new PrintDocument();
            int idx = 0;
            doc.BeginPrint += (s, e) => { idx = 0; };
            doc.PrintPage += (s, e) =>
            {
                using (Font fN = new Font("Consolas", 11), fT = new Font("Consolas", 16, FontStyle.Bold))
                {
                    float y = e.MarginBounds.Top;
                    while (idx < lines.Count)
                    {
                        bool title = lines[idx].StartsWith("#");
                        Font f = title ? fT : fN;
                        float lh = f.GetHeight(e.Graphics) + 4;
                        if (y + lh > e.MarginBounds.Bottom) break;
                        e.Graphics.DrawString(title ? lines[idx].Substring(1) : lines[idx], f, Brushes.Black, e.MarginBounds.Left, y);
                        y += lh;
                        idx++;
                    }
                    e.HasMorePages = idx < lines.Count;
                }
            };
            return doc;
        }

        public static void Preview(PrintDocument doc, string title)
        {
            using (var dlg = new PrintPreviewDialog { Document = doc, Text = title, Width = 1000, Height = 720 })
                dlg.ShowDialog();
        }

        // ---------------- xuất PDF ----------------

        /// <summary>Trả về false nếu máy không có "Microsoft Print to PDF".</summary>
        public static bool ExportPdf(PrintDocument doc, string path)
        {
            const string pdfPrinter = "Microsoft Print to PDF";
            bool found = false;
            foreach (string p in PrinterSettings.InstalledPrinters)
                if (p == pdfPrinter) found = true;
            if (!found) return false;

            doc.PrinterSettings.PrinterName = pdfPrinter;
            doc.PrinterSettings.PrintToFile = true;
            doc.PrinterSettings.PrintFileName = path;
            doc.Print();
            return true;
        }

        // ---------------- xuất Excel (.xlsx) ----------------

        public static void WriteXlsx(string path, DataTable dt)
        {
            var sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            sb.Append("<worksheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\"><sheetData>");

            sb.Append("<row r=\"1\">");
            for (int c = 0; c < dt.Columns.Count; c++)
                sb.Append(StrCell(ColName(c) + "1", dt.Columns[c].ColumnName));
            sb.Append("</row>");

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                int rn = r + 2;
                sb.Append("<row r=\"" + rn + "\">");
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    object v = dt.Rows[r][c];
                    if (v == null || v == DBNull.Value) continue;
                    string rf = ColName(c) + rn;
                    if (v is int || v is long || v is short || v is byte || v is decimal || v is double || v is float)
                        sb.Append("<c r=\"" + rf + "\"><v>" + Convert.ToString(v, CultureInfo.InvariantCulture) + "</v></c>");
                    else
                        sb.Append(StrCell(rf, Cell(v)));
                }
                sb.Append("</row>");
            }
            sb.Append("</sheetData></worksheet>");

            const string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";
            using (var fs = new FileStream(path, FileMode.Create))
            using (var zip = new ZipArchive(fs, ZipArchiveMode.Create))
            {
                AddEntry(zip, "[Content_Types].xml", xml +
                    "<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">" +
                    "<Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/>" +
                    "<Default Extension=\"xml\" ContentType=\"application/xml\"/>" +
                    "<Override PartName=\"/xl/workbook.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml\"/>" +
                    "<Override PartName=\"/xl/worksheets/sheet1.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml\"/>" +
                    "</Types>");
                AddEntry(zip, "_rels/.rels", xml +
                    "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
                    "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"xl/workbook.xml\"/>" +
                    "</Relationships>");
                AddEntry(zip, "xl/workbook.xml", xml +
                    "<workbook xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">" +
                    "<sheets><sheet name=\"BaoCao\" sheetId=\"1\" r:id=\"rId1\"/></sheets></workbook>");
                AddEntry(zip, "xl/_rels/workbook.xml.rels", xml +
                    "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
                    "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet\" Target=\"worksheets/sheet1.xml\"/>" +
                    "</Relationships>");
                AddEntry(zip, "xl/worksheets/sheet1.xml", sb.ToString());
            }
        }

        static void AddEntry(ZipArchive z, string name, string content)
        {
            var entry = z.CreateEntry(name);
            using (var w = new StreamWriter(entry.Open(), new UTF8Encoding(false)))
                w.Write(content);
        }

        static string StrCell(string rf, string text)
        {
            return "<c r=\"" + rf + "\" t=\"inlineStr\"><is><t xml:space=\"preserve\">" + SecurityElement.Escape(text) + "</t></is></c>";
        }

        static string ColName(int i)
        {
            string s = "";
            i++;
            while (i > 0)
            {
                int m = (i - 1) % 26;
                s = (char)('A' + m) + s;
                i = (i - 1) / 26;
            }
            return s;
        }
    }
}
