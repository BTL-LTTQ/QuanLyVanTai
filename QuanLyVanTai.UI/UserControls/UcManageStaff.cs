using FontAwesome.Sharp;
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
    public partial class UcManageStaff : UserControl
    {
        public UcManageStaff()
        {
            InitializeComponent();
            SetupDataGridView();
            dgvNhanSu.CellPainting += dgvNhanSu_CellPainting;
            dgvNhanSu.CellMouseClick += dgvNhanSu_CellMouseClick;

            dgvNhanSu.Rows.Add(
            1,
        "Tuấn Hoa",
        "Tài xế",
        "Vận tải",
        ""
    );

            dgvNhanSu.Rows.Add(
                2,
                "Nguyễn Thanh",
                "Lơ xe",
                "Vận tải",
                ""
            );
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void dgvNhanSu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void SetupDataGridView()
        {
            dgvNhanSu.Dock = DockStyle.Fill;

            dgvNhanSu.AllowUserToAddRows = false;
            dgvNhanSu.AllowUserToResizeRows = false;

            dgvNhanSu.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvNhanSu.MultiSelect = false;

            dgvNhanSu.RowTemplate.Height = 44;
            dgvNhanSu.ColumnHeadersHeight = 42;

            // Không Fill toàn bộ
            dgvNhanSu.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            // STT cố định
            dgvNhanSu.Columns[0].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.None;

            dgvNhanSu.Columns[0].Width = 70;

            // Thao tác cố định
            dgvNhanSu.Columns[4].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.None;

            dgvNhanSu.Columns[4].Width = 130;

            // Căn giữa
            dgvNhanSu.Columns[0].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvNhanSu.Columns[4].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dgvNhanSu.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvNhanSu_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex != 4 || e.Graphics == null)
                return;

            using (Brush bg = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(bg, e.CellBounds);
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


            // Nền nút Sửa
            using (GraphicsPath pathSua =
         CreateRoundedRectangle(rectSua, 6))
            using (Brush brushSua =
                new SolidBrush(Color.FromArgb(33, 150, 243)))
            {
                e.Graphics.FillPath(brushSua, pathSua);
            }
            // Nền nút Xóa
            using (GraphicsPath pathXoa =
        CreateRoundedRectangle(rectXoa, 6))
            using (Brush brushXoa =
                new SolidBrush(Color.FromArgb(239, 83, 80)))
            {
                e.Graphics.FillPath(brushXoa, pathXoa);
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

            // Nút Xóa
            using (Font fontXoa =
        new Font("Segoe UI", 15, FontStyle.Bold))
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

        private void dgvNhanSu_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
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
                (dgvNhanSu.Columns[e.ColumnIndex].Width -
                 totalWidth) / 2;

            // Sửa
            if (e.X >= startX &&
                e.X <= startX + buttonSize)
            {
                SuaNhanSu(e.RowIndex);
                return;
            }

            // Xóa
            int xoaStart =
                startX + buttonSize + spacing;

            if (e.X >= xoaStart &&
                e.X <= xoaStart + buttonSize)
            {
                XoaNhanSu(e.RowIndex);
            }
        }
        private void SuaNhanSu(int rowIndex)
        {
            MessageBox.Show(
                $"Sửa nhân sự dòng {rowIndex + 1}");
        }

        private void XoaNhanSu(int rowIndex)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa nhân sự này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // TODO: Xóa nhân sự trong database
                dgvNhanSu.Rows.RemoveAt(rowIndex);
            }
        }
        private GraphicsPath CreateRoundedRectangle(
    Rectangle rect,
    int radius)
        {
            GraphicsPath path = new GraphicsPath();

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
