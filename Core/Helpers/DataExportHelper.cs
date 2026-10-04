using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Core.Helpers
{
    public static class DataExportHelper
    {
        /// <summary>
        /// Xuất trực tiếp dữ liệu từ DataGridView ra file CSV hoặc Excel XML với dialog lưu file
        /// </summary>
        public static bool ExportDataGridViewWithDialog(DataGridView dgv, string defaultFileName, string title = "Xuất dữ liệu")
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu nào để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            using SaveFileDialog sfd = new SaveFileDialog
            {
                Title = title,
                FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmmss}",
                Filter = "File Excel (*.xls)|*.xls|File CSV (*.csv)|*.csv",
                DefaultExt = "xls"
            };

            if (sfd.ShowDialog() != DialogResult.OK)
                return false;

            try
            {
                if (sfd.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    ExportToCsv(dgv, sfd.FileName);
                }
                else
                {
                    ExportToExcelXml(dgv, sfd.FileName, defaultFileName);
                }

                DialogResult openResult = MessageBox.Show(
                    $"Xuất dữ liệu thành công ra file:\n{sfd.FileName}\n\nBạn có muốn mở file ngay bây giờ không?",
                    "Xuất Dữ Liệu Thành Công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (openResult == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất file:\n{ex.Message}", "Lỗi Xuất Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Xuất DataGridView ra file CSV chuẩn UTF-8 có BOM
        /// </summary>
        public static void ExportToCsv(DataGridView dgv, string filePath)
        {
            var visibleColumns = dgv.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible && !(c is DataGridViewButtonColumn) && c.Name != "colThaoTac")
                .ToList();

            using var sw = new StreamWriter(filePath, false, new UTF8Encoding(true));

            // Header
            var headerLine = string.Join(",", visibleColumns.Select(c => EscapeCsv(c.HeaderText)));
            sw.WriteLine(headerLine);

            // Rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                var line = string.Join(",", visibleColumns.Select(col =>
                {
                    var val = row.Cells[col.Index].FormattedValue ?? row.Cells[col.Index].Value;
                    return EscapeCsv(val?.ToString() ?? string.Empty);
                }));

                sw.WriteLine(line);
            }
        }

        /// <summary>
        /// Xuất DataGridView ra định dạng Excel XML (.xls) độc lập, không cần cài đặt thư viện Office ngoài
        /// </summary>
        public static void ExportToExcelXml(DataGridView dgv, string filePath, string sheetName = "Data")
        {
            var visibleColumns = dgv.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible && !(c is DataGridViewButtonColumn) && c.Name != "colThaoTac")
                .ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<?mso-application progid=\"Excel.Sheet\"?>");
            sb.AppendLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sb.AppendLine(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            sb.AppendLine(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            sb.AppendLine(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sb.AppendLine(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">");

            // Styles
            sb.AppendLine(" <Styles>");
            sb.AppendLine("  <Style ss:ID=\"Default\" ss:Name=\"Normal\">");
            sb.AppendLine("   <Alignment ss:Vertical=\"Center\"/>");
            sb.AppendLine("   <Font ss:FontName=\"Segoe UI\" ss:Size=\"10\" ss:Color=\"#0F172A\"/>");
            sb.AppendLine("  </Style>");
            sb.AppendLine("  <Style ss:ID=\"HeaderStyle\">");
            sb.AppendLine("   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\"/>");
            sb.AppendLine("   <Font ss:FontName=\"Segoe UI\" ss:Size=\"11\" ss:Bold=\"1\" ss:Color=\"#FFFFFF\"/>");
            sb.AppendLine("   <Interior ss:Color=\"#2563EB\" ss:Pattern=\"Solid\"/>");
            sb.AppendLine("  </Style>");
            sb.AppendLine("  <Style ss:ID=\"DataCenter\">");
            sb.AppendLine("   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\"/>");
            sb.AppendLine("  </Style>");
            sb.AppendLine(" </Styles>");

            // Worksheet
            sb.AppendLine($" <Worksheet ss:Name=\"{System.Security.SecurityElement.Escape(sheetName)}\">");
            sb.AppendLine($"  <Table ss:DefaultRowHeight=\"22\">");

            // Columns width
            foreach (var col in visibleColumns)
            {
                double width = Math.Max(col.Width * 0.9, 80);
                sb.AppendLine($"   <Column ss:Width=\"{width:0}\"/>");
            }

            // Header row
            sb.AppendLine("   <Row ss:Height=\"28\">");
            foreach (var col in visibleColumns)
            {
                string title = System.Security.SecurityElement.Escape(col.HeaderText);
                sb.AppendLine($"    <Cell ss:StyleID=\"HeaderStyle\"><Data ss:Type=\"String\">{title}</Data></Cell>");
            }
            sb.AppendLine("   </Row>");

            // Data rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                sb.AppendLine("   <Row ss:Height=\"22\">");
                foreach (var col in visibleColumns)
                {
                    var rawVal = row.Cells[col.Index].FormattedValue ?? row.Cells[col.Index].Value;
                    string text = System.Security.SecurityElement.Escape(rawVal?.ToString() ?? string.Empty);
                    sb.AppendLine($"    <Cell><Data ss:Type=\"String\">{text}</Data></Cell>");
                }
                sb.AppendLine("   </Row>");
            }

            sb.AppendLine("  </Table>");
            sb.AppendLine(" </Worksheet>");
            sb.AppendLine("</Workbook>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private static string EscapeCsv(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "\"\"";

            if (text.Contains(",") || text.Contains("\"") || text.Contains("\n") || text.Contains("\r"))
            {
                return "\"" + text.Replace("\"", "\"\"") + "\"";
            }
            return "\"" + text + "\"";
        }
    }
}
