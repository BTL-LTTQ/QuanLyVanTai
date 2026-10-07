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
    public partial class UcManageTicket : UserControl
    {
        public UcManageTicket()
        {
            InitializeComponent();
            SetupDataGridView();
            dgvGiaVe.CellPainting += dgvGiaVe_CellPainting;
            dgvGiaVe.CellMouseClick += dgvGiaVe_CellMouseClick;
            dgvGiaVe.Rows.Add(
                "Hà Nội - Hải Phòng",
                "Người lớn",
                "150.000",
                "Đang áp dụng"
            );

            dgvGiaVe.Rows.Add(
                "Hà Nội - Hải Phòng",
                "Trẻ em",
                "100.000",
                "Đang áp dụng"
            );
        }
        private void SetupDataGridView()
        {
            dgvGiaVe.Dock = DockStyle.Fill;

            dgvGiaVe.AllowUserToAddRows = false;
            dgvGiaVe.AllowUserToResizeRows = false;

            dgvGiaVe.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvGiaVe.MultiSelect = false;

            dgvGiaVe.RowTemplate.Height = 44;
            dgvGiaVe.ColumnHeadersHeight = 42;

            dgvGiaVe.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            // Cột Thao tác
            dgvGiaVe.Columns[4].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.None;

            dgvGiaVe.Columns[4].Width = 130;

            // Căn giữa
            dgvGiaVe.Columns[2].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvGiaVe.Columns[3].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvGiaVe.Columns[4].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvGiaVe.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvGiaVe_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // Cột Thao tác
            if (e.ColumnIndex != 4 || e.Graphics == null)
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

            // =========================
            // NÚT SỬA
            // =========================
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

            // =========================
            // NÚT XÓA
            // =========================
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

            // Icon sửa
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

            // Icon xóa
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

        private void dgvGiaVe_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex != 4)
                return;

            int buttonSize = 38;
            int spacing = 10;

            int totalWidth =
                buttonSize * 2 + spacing;

            int startX =
                (dgvGiaVe.Columns[e.ColumnIndex].Width -
                 totalWidth) / 2;

            // =========================
            // CLICK SỬA
            // =========================
            if (e.X >= startX &&
                e.X <= startX + buttonSize)
            {
                SuaGiaVe(e.RowIndex);
                return;
            }

            // =========================
            // CLICK XÓA
            // =========================
            int xoaStart =
                startX + buttonSize + spacing;

            if (e.X >= xoaStart &&
                e.X <= xoaStart + buttonSize)
            {
                XoaGiaVe(e.RowIndex);
            }
        }

        // =========================
        // SỬA GIÁ VÉ
        // =========================
        private void SuaGiaVe(int rowIndex)
        {
            string tuyen =
                dgvGiaVe.Rows[rowIndex].Cells[0].Value?.ToString() ?? "";

            string loaiVe =
                dgvGiaVe.Rows[rowIndex].Cells[1].Value?.ToString() ?? "";

            string gia =
                dgvGiaVe.Rows[rowIndex].Cells[2].Value?.ToString() ?? "";

            MessageBox.Show(
                $"Tuyến: {tuyen}\n" +
                $"Loại vé: {loaiVe}\n" +
                $"Giá: {gia}\n\n" +
                "Đang mở chức năng sửa...",
                "Sửa giá vé",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =========================
        // XÓA GIÁ VÉ
        // =========================
        private void XoaGiaVe(int rowIndex)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa giá vé này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                dgvGiaVe.Rows.RemoveAt(rowIndex);
            }
        }

        // =========================
        // BO GÓC BUTTON
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
