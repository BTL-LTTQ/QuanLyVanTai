using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using Core.Security;
using QuanLyVanTai.BLL.Services;
using UserSession = Core.Security.UserSession;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcManagePromotion : UserControl
    {
        private readonly AuditLogService _auditLogService = new();
        private bool _canEditPromotion;
        private bool _canDeletePromotion;

        public UcManagePromotion()
        {
            InitializeComponent();
            if (!AuthorizationGuard.GuardFormAccess(this, SystemMenus.Promotion))
                return;

            SetupDataGridView();
            dgvKhuyenMai.Visible = true;
            WirePromotionSecurityAndAudit();
            this.Load += UcManagePromotion_Load;
            dgvKhuyenMai.CellPainting += dgvKhuyenMai_CellPainting;
            dgvKhuyenMai.CellMouseClick += dgvKhuyenMai_CellMouseClick;

            // Đảm bảo hiển thị header
            dgvKhuyenMai.ColumnHeadersVisible = true;
            dgvKhuyenMai.RowHeadersVisible = false;

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

            dgvKhuyenMai.RowHeadersVisible = false;
            dgvKhuyenMai.ColumnHeadersVisible = true; // Đảm bảo hiển thị header
            dgvKhuyenMai.RowTemplate.Height = 44;
            dgvKhuyenMai.ColumnHeadersHeight = 45; // Tăng chiều cao header

            dgvKhuyenMai.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            // Đảm bảo tên cột hiển thị đúng
            if (dgvKhuyenMai.Columns.Count >= 6)
            {
                dgvKhuyenMai.Columns[0].HeaderText = "Tên Khuyến Mãi";
                dgvKhuyenMai.Columns[1].HeaderText = "Loại";
                dgvKhuyenMai.Columns[2].HeaderText = "Giá Trị";
                dgvKhuyenMai.Columns[3].HeaderText = "Thời Gian";
                dgvKhuyenMai.Columns[4].HeaderText = "Trạng Thái";
                dgvKhuyenMai.Columns[5].HeaderText = "Thao Tác";

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
            }

            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvKhuyenMai_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex != 5 || e.Graphics == null)
                return;

            if (!_canEditPromotion && !_canDeletePromotion)
            {
                e.PaintBackground(e.CellBounds, true);
                e.Handled = true;
                return;
            }

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
            if (_canEditPromotion &&
                e.X >= startX &&
                e.X <= startX + buttonSize)
            {
                SuaKhuyenMai(e.RowIndex);
                return;
            }

            // Xóa
            int xoaStart =
                startX + buttonSize + spacing;

            if (_canDeletePromotion &&
                e.X >= xoaStart &&
                e.X <= xoaStart + buttonSize)
            {
                XoaKhuyenMai(e.RowIndex);
            }
        }
        private void UcManagePromotion_Load(object? sender, EventArgs e)
        {
            if (!AuthorizationGuard.GuardFormAccess(this, SystemMenus.Promotion))
                return;
            ApplyPromotionRoleUi();
        }

        private void WirePromotionSecurityAndAudit()
        {
            btnThemKhuyenMai.Click += BtnThemKhuyenMai_Click;
            ApplyPromotionRoleUi();
        }

        private void ApplyPromotionRoleUi()
        {
            _canEditPromotion = UserSession.HasPermission(SystemMenus.Promotion, PermissionAction.Edit);
            _canDeletePromotion = UserSession.HasPermission(SystemMenus.Promotion, PermissionAction.Delete);
            AuthorizationGuard.ApplyControlSecurity(
                SystemMenus.Promotion,
                btnThemKhuyenMai,
                hideWhenDenied: true);
            dgvKhuyenMai.Invalidate();
        }

        private void BtnThemKhuyenMai_Click(object? sender, EventArgs e)
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Promotion, PermissionAction.Add))
                return;

            dgvKhuyenMai.Rows.Add("Chương trình mới", "Giảm giá %", "10%", DateTime.Now.ToString("dd/MM/yyyy"), "Đang áp dụng");
            int rowIndex = dgvKhuyenMai.Rows.Count - 1;
            _ = WriteAuditTrailAsync("Thêm", rowIndex.ToString(), "Thêm chương trình khuyến mãi");
            MessageBox.Show("Đã thêm chương trình khuyến mãi mẫu.", "Thêm khuyến mãi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SuaKhuyenMai(int rowIndex)
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Promotion, PermissionAction.Edit))
                return;

            string ten =
                dgvKhuyenMai.Rows[rowIndex]
                .Cells[0].Value?.ToString() ?? "";

            string loai =
                dgvKhuyenMai.Rows[rowIndex]
                .Cells[1].Value?.ToString() ?? "";

            string giaTri =
                dgvKhuyenMai.Rows[rowIndex]
                .Cells[2].Value?.ToString() ?? "";

            _ = WriteAuditTrailAsync("Sửa", rowIndex.ToString(), $"Sửa khuyến mãi [{ten}] loại [{loai}] giá trị [{giaTri}]");
            MessageBox.Show(
                $"Tên: {ten}\n" +
                $"Loại: {loai}\n" +
                $"Giá trị: {giaTri}\n\n" +
                "Đã ghi nhật ký chỉnh sửa.",
                "Sửa khuyến mãi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =========================
        // XÓA KHUYẾN MÃI
        // =========================
        private void XoaKhuyenMai(int rowIndex)
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Promotion, PermissionAction.Delete))
                return;

            string ten = dgvKhuyenMai.Rows[rowIndex].Cells[0].Value?.ToString() ?? "";
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa chương trình khuyến mãi này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                dgvKhuyenMai.Rows.RemoveAt(rowIndex);
                _ = WriteAuditTrailAsync("Xóa", rowIndex.ToString(), $"Xóa khuyến mãi [{ten}]");
            }
        }

        private async Task WriteAuditTrailAsync(string action, string recordId, string details)
        {
            await _auditLogService.LogActionAsync(
                action,
                "Khuyến mãi",
                recordId,
                $"{details} | Người thực hiện: {UserSession.CurrentUsername} | Thời điểm: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
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
