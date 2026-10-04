using System.Drawing;
using System.Windows.Forms;

namespace Core.Helpers
{
    public static class DataGridViewHighlightHelper
    {
        private static readonly Color HighlightBackColor = Color.FromArgb(254, 240, 138); // Vàng highlight nhẹ hiện đại
        private static readonly Color HighlightTextColor = Color.FromArgb(113, 63, 18);   // Nâu đậm đọc rõ trên nền vàng

        /// <summary>
        /// Gắn tính năng tự động Highlight các từ khóa tìm kiếm trên DataGridView
        /// </summary>
        public static void AttachSearchHighlighter(DataGridView dgv, Func<string> getSearchTextFunc)
        {
            dgv.CellPainting += (sender, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                string searchText = getSearchTextFunc()?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(searchText))
                    return;

                object rawVal = e.FormattedValue ?? e.Value;
                if (rawVal == null)
                    return;

                string cellText = rawVal.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(cellText))
                    return;

                // Kiểm tra xem cellText có chứa searchText (không phân biệt hoa thường)
                int matchIndex = cellText.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase);
                if (matchIndex < 0)
                    return;

                // Vẽ nền ô trước (theo trạng thái đang chọn hoặc bình thường)
                e.PaintBackground(e.CellBounds, (e.State & DataGridViewElementStates.Selected) != 0);

                // Tính toán vị trí hiển thị text
                Rectangle textRect = e.CellBounds;
                textRect.Offset(2, 2);
                textRect.Width -= 4;
                textRect.Height -= 4;

                // Căn chỉnh lề theo Column Alignment
                StringFormat format = new StringFormat
                {
                    LineAlignment = StringAlignment.Center
                };

                switch (e.CellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.TopCenter:
                    case DataGridViewContentAlignment.BottomCenter:
                        format.Alignment = StringAlignment.Center;
                        break;
                    case DataGridViewContentAlignment.MiddleRight:
                    case DataGridViewContentAlignment.TopRight:
                    case DataGridViewContentAlignment.BottomRight:
                        format.Alignment = StringAlignment.Far;
                        break;
                    default:
                        format.Alignment = StringAlignment.Near;
                        break;
                }

                // Tô highlight từng vị trí xuất hiện của searchText
                using (Font font = e.CellStyle.Font ?? dgv.Font)
                using (Brush normalTextBrush = new SolidBrush(
                    (e.State & DataGridViewElementStates.Selected) != 0 ? e.CellStyle.SelectionForeColor : e.CellStyle.ForeColor))
                using (Brush highlightBgBrush = new SolidBrush(HighlightBackColor))
                using (Brush highlightTextBrush = new SolidBrush(HighlightTextColor))
                {
                    // Vẽ thủ công phân đoạn nếu ô căn trái
                    if (format.Alignment == StringAlignment.Near)
                    {
                        float currentX = textRect.X;
                        float currentY = textRect.Y + (textRect.Height - font.Height) / 2f;
                        int currentIndex = 0;

                        while (currentIndex < cellText.Length)
                        {
                            int foundIdx = cellText.IndexOf(searchText, currentIndex, StringComparison.CurrentCultureIgnoreCase);
                            if (foundIdx < 0)
                            {
                                // Vẽ phần còn lại
                                string remaining = cellText.Substring(currentIndex);
                                e.Graphics.DrawString(remaining, font, normalTextBrush, currentX, currentY);
                                break;
                            }

                            // Vẽ phần trước match
                            if (foundIdx > currentIndex)
                            {
                                string prefix = cellText.Substring(currentIndex, foundIdx - currentIndex);
                                SizeF prefixSize = e.Graphics.MeasureString(prefix, font, 10000, StringFormat.GenericTypographic);
                                e.Graphics.DrawString(prefix, font, normalTextBrush, currentX, currentY, StringFormat.GenericTypographic);
                                currentX += prefixSize.Width;
                            }

                            // Vẽ match được highlight
                            string matched = cellText.Substring(foundIdx, searchText.Length);
                            SizeF matchedSize = e.Graphics.MeasureString(matched, font, 10000, StringFormat.GenericTypographic);

                            // Đổ bóng nền highlight
                            RectangleF hlRect = new RectangleF(currentX, currentY - 1, matchedSize.Width, font.Height + 2);
                            e.Graphics.FillRectangle(highlightBgBrush, hlRect);

                            // Chữ highlight
                            e.Graphics.DrawString(matched, font, highlightTextBrush, currentX, currentY, StringFormat.GenericTypographic);
                            currentX += matchedSize.Width;

                            currentIndex = foundIdx + searchText.Length;
                        }
                    }
                    else
                    {
                        // Fallback vẽ text căn giữa/phải có kèm viền màu cảnh báo
                        e.Graphics.DrawString(cellText, font, normalTextBrush, textRect, format);
                    }
                }

                // Vẽ viền ô
                e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
                e.Handled = true;
            };
        }
    }
}
