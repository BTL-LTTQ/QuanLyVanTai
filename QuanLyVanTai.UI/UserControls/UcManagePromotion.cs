using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcManagePromotion : UserControl
    {
        public UcManagePromotion()
        {
            InitializeComponent();
            SetupDataGridView();
            dgvKhuyenMai.CellPainting += dgvKhuyenMai_CellPainting;
            dgvKhuyenMai.CellMouseClick += dgvKhuyenMai_CellMouseClick;

            // Dữ liệu mẫu
            dgvKhuyenMai.Rows.Add(
                "Giảm 20% vé Tết",
                "Giảm giá %",
                "20%",
                "01/10/2026 - 31/10/2026",
                "Đang áp dụng"
            );

            dgvKhuyenMai.Rows.Add(
                "Mừng ngày 30/4",
                "Giảm giá %",
                "15%",
                "25/04/2026 - 05/05/2026",
                "Đã kết thúc"
            );
        }
        private void SetupDataGridView()
        {
            dgvKhuyenMai.Dock = DockStyle.Fill;

            dgvKhuyenMai.AllowUserToAddRows = false;
            dgvKhuyenMai.AllowUserToResizeRows = false;

            dgvKhuyenMai.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvKhuyenMai.MultiSelect = false;

            dgvKhuyenMai.RowTemplate.Height = 44;
            dgvKhuyenMai.ColumnHeadersHeight = 42;

            dgvKhuyenMai.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            // Cột thao tác
            dgvKhuyenMai.Columns[5].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.None;

            dgvKhuyenMai.Columns[5].Width = 130;

            // Căn giữa
            dgvKhuyenMai.Columns[2].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvKhuyenMai.Columns[3].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvKhuyenMai.Columns[4].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvKhuyenMai.Columns[5].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvKhuyenMai_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex != 5 || e.Graphics == null)
                return;

            using (Brush bg = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(
                    bg,
                    e.CellBounds);
            }

            int buttonWidth = 38;
            int buttonHeight = 30;
            int spacing = 10;

            int totalWidth =
                buttonWidth * 2 + spacing;

            int startX =
                e.CellBounds.X +
                (e.CellBounds.Width - totalWidth) / 2;

            int startY =
                e.CellBounds.Y +
                (e.CellBounds.Height - buttonHeight) / 2;

            Rectangle rectSua = new Rectangle(
                startX,
                startY,
                buttonWidth,
                buttonHeight);

            Rectangle rectXoa = new Rectangle(
                startX + buttonWidth + spacing,
                startY,
                buttonWidth,
                buttonHeight);

            // Nút Sửa
            using (GraphicsPath pathSua =
                CreateRoundedRectangle(rectSua, 6))
            using (Brush brushSua =
                new SolidBrush(
                    Color.FromArgb(33, 150, 243)))
            {
                e.Graphics.FillPath(
                    brushSua,
                    pathSua);
            }

            // Nút Xóa
            using (GraphicsPath pathXoa =
                CreateRoundedRectangle(rectXoa, 6))
            using (Brush brushXoa =
                new SolidBrush(
                    Color.FromArgb(239, 83, 80)))
            {
                e.Graphics.FillPath(
                    brushXoa,
                    pathXoa);
            }

            // Icon Sửa
            using (Font fontSua =
                new Font("Segoe UI Symbol", 15))
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    "✎",
                    fontSua,
                    rectSua,
                    Color.White,
                    TextFormatFlags.HorizontalCenter |
                    TextFormatFlags.VerticalCenter);
            }

            // Icon Xóa
            using (Font fontXoa =
                new Font(
                    "Segoe UI",
                    15,
                    FontStyle.Bold))
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    "×",
                    fontXoa,
                    rectXoa,
                    Color.White,
                    TextFormatFlags.HorizontalCenter |
                    TextFormatFlags.VerticalCenter);
            }

            e.Handled = true;
        }

        private void dgvKhuyenMai_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex != 5)
                return;

            int buttonSize = 38;
            int spacing = 10;

            int totalWidth =
                buttonSize * 2 + spacing;

            int startX =
                (dgvKhuyenMai.Columns[e.ColumnIndex].Width -
                 totalWidth) / 2;

            // Sửa
            if (e.X >= startX &&
                e.X <= startX + buttonSize)
            {
                SuaKhuyenMai(e.RowIndex);
                return;
            }

            // Xóa
            int xoaStart =
                startX + buttonSize + spacing;

            if (e.X >= xoaStart &&
                e.X <= xoaStart + buttonSize)
            {
                XoaKhuyenMai(e.RowIndex);
            }
        }
        private void SuaKhuyenMai(int rowIndex)
        {
            string ten =
                dgvKhuyenMai.Rows[rowIndex]
                .Cells[0].Value?.ToString() ?? "";

            string loai =
                dgvKhuyenMai.Rows[rowIndex]
                .Cells[1].Value?.ToString() ?? "";

            string giaTri =
                dgvKhuyenMai.Rows[rowIndex]
                .Cells[2].Value?.ToString() ?? "";

            MessageBox.Show(
                $"Tên: {ten}\n" +
                $"Loại: {loai}\n" +
                $"Giá trị: {giaTri}\n\n" +
                "Đang mở chức năng sửa...",
                "Sửa khuyến mãi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =========================
        // XÓA KHUYẾN MÃI
        // =========================
        private void XoaKhuyenMai(int rowIndex)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa chương trình khuyến mãi này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                dgvKhuyenMai.Rows.RemoveAt(rowIndex);
            }
        }

        // =========================
        // BO GÓC
        // =========================
        private GraphicsPath CreateRoundedRectangle(
            Rectangle rect,
            int radius)
        {
            GraphicsPath path =
                new GraphicsPath();

            int diameter = radius * 2;

            path.AddArc(
                rect.X,
                rect.Y,
                diameter,
                diameter,
                180,
                90);

            path.AddArc(
                rect.Right - diameter,
                rect.Y,
                diameter,
                diameter,
                270,
                90);

            path.AddArc(
                rect.Right - diameter,
                rect.Bottom - diameter,
                diameter,
                diameter,
                0,
                90);

            path.AddArc(
                rect.X,
                rect.Bottom - diameter,
                diameter,
                diameter,
                90,
                90);

            path.CloseFigure();

            return path;
        }
    }
}
