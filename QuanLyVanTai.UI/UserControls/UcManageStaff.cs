using Core.Configs;
using Core.Helpers;
using Core.Security;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UserSession = Core.Security.UserSession;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcManageStaff : UserControl
    {
        // Modern UI Controls
        private readonly Panel pnlHeader = new();
        private readonly Panel pnlStatsCards = new();
        private readonly Panel pnlToolbar = new();
        private readonly DataGridView dgvNhanSu = new();
        private readonly TextBox txtTimKiem = new();
        private readonly ComboBox cboChucVu = new();
        private readonly ComboBox cboTrangThai = new();
        private readonly IconButton btnTimKiem = new();
        private readonly IconButton btnThemNhanSu = new();
        private readonly IconButton btnExport = new();

        // Demo data
        private List<StaffMember> _allStaff = new();
        private List<StaffMember> _filteredStaff = new();

        public UcManageStaff()
        {
            InitializeComponent();
            BuildModernLayout(); // Tạo layout trước (để add controls vào form)
            SetupDataGridView(); // Setup DataGridView columns sau khi đã add vào form
            LoadDemoData(); // Load data sau cùng
            ApplySecurity();
        }

        private void BuildModernLayout()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = Color.FromArgb(248, 250, 252);

            // ==========================================
            // 1. PAGE HEADER
            // ==========================================
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 60;
            pnlHeader.BackColor = Color.White;
            pnlHeader.Padding = new Padding(25, 15, 25, 15);

            Label lblPageTitle = new Label
            {
                Text = "👥 QUẢN LÝ NHÂN SỰ",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Dock = DockStyle.Fill
            };
            pnlHeader.Controls.Add(lblPageTitle);
            this.Controls.Add(pnlHeader);

            // ==========================================
            // 2. STATISTICS CARDS
            // ==========================================
            pnlStatsCards.Dock = DockStyle.Top;
            pnlStatsCards.Height = 115;
            pnlStatsCards.BackColor = Color.FromArgb(248, 250, 252);
            pnlStatsCards.Padding = new Padding(25, 12, 25, 12);

            // Stat Cards
            Panel cardTotal = CreateStatCard("👨‍💼 Tổng Nhân Sự", "0", Color.FromArgb(59, 130, 246), 0);
            Panel cardActive = CreateStatCard("✅ Đang Làm Việc", "0", Color.FromArgb(16, 185, 129), 310);
            Panel cardDriver = CreateStatCard("🚗 Tài Xế", "0", Color.FromArgb(245, 158, 11), 620);
            Panel cardConductor = CreateStatCard("🎫 Phụ Xe", "0", Color.FromArgb(139, 92, 246), 930);

            pnlStatsCards.Controls.AddRange(new Control[] { cardTotal, cardActive, cardDriver, cardConductor });
            this.Controls.Add(pnlStatsCards);

            // ==========================================
            // 3. TOOLBAR với Search & Filters
            // ==========================================
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Height = 95;
            pnlToolbar.BackColor = Color.White;
            pnlToolbar.Padding = new Padding(25, 15, 25, 15);

            // Search Box
            Label lblSearch = new Label
            {
                Text = "Tìm kiếm:",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(25, 15),
                AutoSize = true
            };
            pnlToolbar.Controls.Add(lblSearch);

            txtTimKiem.Location = new Point(25, 37);
            txtTimKiem.Size = new Size(300, 35);
            txtTimKiem.Font = new Font("Segoe UI", 10.5F);
            txtTimKiem.PlaceholderText = "🔍 Tìm theo tên, chức vụ...";
            txtTimKiem.BorderStyle = BorderStyle.FixedSingle;
            pnlToolbar.Controls.Add(txtTimKiem);

            // Chức vụ Filter
            Label lblChucVu = new Label
            {
                Text = "Chức vụ:",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(345, 15),
                AutoSize = true
            };
            pnlToolbar.Controls.Add(lblChucVu);

            cboChucVu.Location = new Point(345, 37);
            cboChucVu.Size = new Size(180, 35);
            cboChucVu.Font = new Font("Segoe UI", 10F);
            cboChucVu.DropDownStyle = ComboBoxStyle.DropDownList;
            cboChucVu.Items.AddRange(new string[] { "Tất cả", "Tài xế", "Phụ xe", "Quản lý", "Kế toán", "IT" });
            cboChucVu.SelectedIndex = 0;
            pnlToolbar.Controls.Add(cboChucVu);

            // Trạng thái Filter
            Label lblTrangThai = new Label
            {
                Text = "Trạng thái:",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(545, 15),
                AutoSize = true
            };
            pnlToolbar.Controls.Add(lblTrangThai);

            cboTrangThai.Location = new Point(545, 37);
            cboTrangThai.Size = new Size(170, 35);
            cboTrangThai.Font = new Font("Segoe UI", 10F);
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Items.AddRange(new string[] { "Tất cả", "Đang làm việc", "Nghỉ phép", "Đã nghỉ" });
            cboTrangThai.SelectedIndex = 0;
            pnlToolbar.Controls.Add(cboTrangThai);

            // Search Button
            ThemeConfig.StylePrimaryButton(btnTimKiem, IconChar.MagnifyingGlass);
            btnTimKiem.Text = " Tìm Kiếm";
            btnTimKiem.Size = new Size(130, 42);
            btnTimKiem.Location = new Point(735, 34);
            btnTimKiem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTimKiem.Click += (s, e) => FilterStaff();
            pnlToolbar.Controls.Add(btnTimKiem);

            // Add Button
            ThemeConfig.StyleSuccessButton(btnThemNhanSu, IconChar.UserPlus);
            btnThemNhanSu.Text = " Thêm Nhân Sự";
            btnThemNhanSu.Size = new Size(160, 42);
            btnThemNhanSu.Location = new Point(885, 34);
            btnThemNhanSu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnThemNhanSu.Click += BtnThemNhanSu_Click;
            pnlToolbar.Controls.Add(btnThemNhanSu);

            // Export Button
            ThemeConfig.StyleSecondaryButton(btnExport, IconChar.FileExcel);
            btnExport.Text = " Xuất Excel";
            btnExport.Size = new Size(140, 42);
            btnExport.Location = new Point(1060, 34);
            btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExport.BackColor = Color.FromArgb(16, 185, 129);
            btnExport.Click += (s, e) => DataExportHelper.ExportDataGridViewWithDialog(dgvNhanSu, "DanhSach_NhanSu");
            pnlToolbar.Controls.Add(btnExport);

            this.Controls.Add(pnlToolbar);

            // ==========================================
            // 4. DATA GRID VIEW
            // ==========================================
            dgvNhanSu.Dock = DockStyle.Fill;
            dgvNhanSu.BackgroundColor = Color.White;
            dgvNhanSu.BorderStyle = BorderStyle.None;
            dgvNhanSu.AllowUserToAddRows = false;
            dgvNhanSu.AllowUserToDeleteRows = false;
            dgvNhanSu.AllowUserToResizeRows = false;
            dgvNhanSu.ReadOnly = false; // Allow editing for actions
            dgvNhanSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanSu.MultiSelect = false;
            dgvNhanSu.RowHeadersVisible = false;
            dgvNhanSu.RowTemplate.Height = 50;
            dgvNhanSu.ColumnHeadersHeight = 45;
            dgvNhanSu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvNhanSu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvNhanSu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvNhanSu.ColumnHeadersDefaultCellStyle.ForeColor = ThemeConfig.TextMain;
            dgvNhanSu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvNhanSu.EnableHeadersVisualStyles = false;
            dgvNhanSu.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvNhanSu.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvNhanSu.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvNhanSu.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            
            this.Controls.Add(dgvNhanSu);
        }

        private Panel CreateStatCard(string title, string value, Color accentColor, int xPos)
        {
            Panel card = new Panel
            {
                Size = new Size(290, 90),
                Location = new Point(xPos, 0),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(15, 12, 15, 12)
            };

            // Accent bar
            Panel accentBar = new Panel
            {
                Size = new Size(4, 90),
                Location = new Point(0, 0),
                BackColor = accentColor
            };
            card.Controls.Add(accentBar);

            // Title
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(20, 15),
                AutoSize = true
            };
            card.Controls.Add(lblTitle);

            // Value
            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = accentColor,
                Location = new Point(20, 40),
                AutoSize = true,
                Tag = "statValue"
            };
            card.Controls.Add(lblValue);

            card.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(226, 232, 240), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
            };

            return card;
        }

        private void SetupDataGridView()
        {
            dgvNhanSu.Columns.Clear();

            dgvNhanSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                Visible = false
            });

            dgvNhanSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSTT",
                HeaderText = "STT",
                Width = 60,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvNhanSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAvatar",
                HeaderText = "👤",
                Width = 50,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvNhanSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHoTen",
                HeaderText = "Họ Tên",
                Width = 200,
                DefaultCellStyle = { Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) }
            });

            dgvNhanSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colChucVu",
                HeaderText = "Chức Vụ",
                Width = 140,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvNhanSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPhongBan",
                HeaderText = "Phòng Ban",
                Width = 150
            });

            dgvNhanSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoDienThoai",
                HeaderText = "Số Điện Thoại",
                Width = 130,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvNhanSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colEmail",
                HeaderText = "Email",
                Width = 200
            });

            dgvNhanSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTrangThai",
                HeaderText = "Trạng Thái",
                Width = 130,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvNhanSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colThaoTac",
                HeaderText = "Thao Tác",
                Width = 150,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvNhanSu.CellPainting += DgvNhanSu_CellPainting;
            dgvNhanSu.CellMouseClick += DgvNhanSu_CellMouseClick;
        }

        private void LoadDemoData()
        {
            _allStaff = new List<StaffMember>
            {
                new StaffMember { Id = 1, HoTen = "Nguyễn Văn An", ChucVu = "Tài xế", PhongBan = "Vận tải", SoDienThoai = "0901234567", Email = "an.nv@vantai.com", TrangThai = "Đang làm việc" },
                new StaffMember { Id = 2, HoTen = "Trần Thị Bình", ChucVu = "Phụ xe", PhongBan = "Vận tải", SoDienThoai = "0912345678", Email = "binh.tt@vantai.com", TrangThai = "Đang làm việc" },
                new StaffMember { Id = 3, HoTen = "Lê Quang Cường", ChucVu = "Tài xế", PhongBan = "Vận tải", SoDienThoai = "0923456789", Email = "cuong.lq@vantai.com", TrangThai = "Đang làm việc" },
                new StaffMember { Id = 4, HoTen = "Phạm Minh Đức", ChucVu = "Quản lý", PhongBan = "Điều hành", SoDienThoai = "0934567890", Email = "duc.pm@vantai.com", TrangThai = "Đang làm việc" },
                new StaffMember { Id = 5, HoTen = "Hoàng Thị Em", ChucVu = "Kế toán", PhongBan = "Tài chính", SoDienThoai = "0945678901", Email = "em.ht@vantai.com", TrangThai = "Đang làm việc" },
                new StaffMember { Id = 6, HoTen = "Đỗ Văn Phong", ChucVu = "Phụ xe", PhongBan = "Vận tải", SoDienThoai = "0956789012", Email = "phong.dv@vantai.com", TrangThai = "Nghỉ phép" },
                new StaffMember { Id = 7, HoTen = "Vũ Thị Giang", ChucVu = "IT", PhongBan = "Công nghệ", SoDienThoai = "0967890123", Email = "giang.vt@vantai.com", TrangThai = "Đang làm việc" },
                new StaffMember { Id = 8, HoTen = "Bùi Minh Hải", ChucVu = "Tài xế", PhongBan = "Vận tải", SoDienThoai = "0978901234", Email = "hai.bm@vantai.com", TrangThai = "Đã nghỉ" }
            };

            _filteredStaff = new List<StaffMember>(_allStaff);
            FilterStaff();
        }

        private void FilterStaff()
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            string chucVu = cboChucVu.SelectedItem?.ToString() ?? "Tất cả";
            string trangThai = cboTrangThai.SelectedItem?.ToString() ?? "Tất cả";

            _filteredStaff = _allStaff.Where(s =>
            {
                bool matchKeyword = string.IsNullOrEmpty(keyword) ||
                                    s.HoTen.ToLower().Contains(keyword) ||
                                    s.ChucVu.ToLower().Contains(keyword) ||
                                    s.PhongBan.ToLower().Contains(keyword);

                bool matchChucVu = chucVu == "Tất cả" || s.ChucVu == chucVu;
                bool matchTrangThai = trangThai == "Tất cả" || s.TrangThai == trangThai;

                return matchKeyword && matchChucVu && matchTrangThai;
            }).ToList();

            RenderStaffGrid();
            UpdateStatistics();
        }

        private void RenderStaffGrid()
        {
            dgvNhanSu.Rows.Clear();

            for (int i = 0; i < _filteredStaff.Count; i++)
            {
                var staff = _filteredStaff[i];
                dgvNhanSu.Rows.Add(
                    staff.Id,
                    i + 1,
                    "👤",
                    staff.HoTen,
                    staff.ChucVu,
                    staff.PhongBan,
                    staff.SoDienThoai,
                    staff.Email,
                    staff.TrangThai,
                    "" // Action column - painted custom
                );

                // Color coding for status
                var row = dgvNhanSu.Rows[i];
                var statusCell = row.Cells["colTrangThai"];
                
                if (staff.TrangThai == "Đang làm việc")
                {
                    statusCell.Style.ForeColor = Color.FromArgb(16, 185, 129);
                    statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                else if (staff.TrangThai == "Nghỉ phép")
                {
                    statusCell.Style.ForeColor = Color.FromArgb(245, 158, 11);
                    statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                else
                {
                    statusCell.Style.ForeColor = Color.FromArgb(239, 68, 68);
                    statusCell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        private void UpdateStatistics()
        {
            try
            {
                int totalStaff = _allStaff.Count;
                int activeStaff = _allStaff.Count(s => s.TrangThai == "Đang làm việc");
                int drivers = _allStaff.Count(s => s.ChucVu == "Tài xế");
                int conductors = _allStaff.Count(s => s.ChucVu == "Phụ xe");

                // Kiểm tra pnlStatsCards đã có controls chưa
                if (pnlStatsCards.Controls.Count >= 4)
                {
                    UpdateCardValue(pnlStatsCards.Controls[0], totalStaff.ToString());
                    UpdateCardValue(pnlStatsCards.Controls[1], activeStaff.ToString());
                    UpdateCardValue(pnlStatsCards.Controls[2], drivers.ToString());
                    UpdateCardValue(pnlStatsCards.Controls[3], conductors.ToString());
                }
            }
            catch (Exception ex)
            {
                // Log error nhưng không crash app
                System.Diagnostics.Debug.WriteLine($"UpdateStatistics error: {ex.Message}");
            }
        }

        private void UpdateCardValue(Control card, string newValue)
        {
            if (card == null) return;

            foreach (Control ctrl in card.Controls)
            {
                if (ctrl is Label lbl && lbl.Tag?.ToString() == "statValue")
                {
                    lbl.Text = newValue;
                    break;
                }
            }
        }

        private void DgvNhanSu_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // Kiểm tra column tồn tại
            if (dgvNhanSu.Columns["colThaoTac"] == null)
                return;

            if (e.ColumnIndex != dgvNhanSu.Columns["colThaoTac"].Index)
                return;

            // Kiểm tra Graphics không null
            if (e.Graphics == null)
                return;

            e.PaintBackground(e.CellBounds, true);

            // Edit button
            Rectangle btnEdit = new Rectangle(e.CellBounds.X + 20, e.CellBounds.Y + 12, 50, 26);
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(59, 130, 246)))
            {
                e.Graphics.FillRectangle(brush, btnEdit);
            }
            TextRenderer.DrawText(e.Graphics, "✏ Sửa", dgvNhanSu.Font, btnEdit, Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // Delete button
            Rectangle btnDelete = new Rectangle(e.CellBounds.X + 80, e.CellBounds.Y + 12, 50, 26);
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(239, 68, 68)))
            {
                e.Graphics.FillRectangle(brush, btnDelete);
            }
            TextRenderer.DrawText(e.Graphics, "🗑 Xóa", dgvNhanSu.Font, btnDelete, Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            e.Handled = true;
        }

        private void DgvNhanSu_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // Kiểm tra column tồn tại
            if (dgvNhanSu.Columns["colThaoTac"] == null)
                return;

            if (e.ColumnIndex != dgvNhanSu.Columns["colThaoTac"].Index)
                return;

            var cellBounds = dgvNhanSu.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Rectangle btnEdit = new Rectangle(cellBounds.X + 20, cellBounds.Y + 12, 50, 26);
            Rectangle btnDelete = new Rectangle(cellBounds.X + 80, cellBounds.Y + 12, 50, 26);

            Point clickPoint = dgvNhanSu.PointToClient(Cursor.Position);
            clickPoint.X -= cellBounds.X;
            clickPoint.Y -= cellBounds.Y;

            // Kiểm tra cell value không null
            var cellValue = dgvNhanSu.Rows[e.RowIndex].Cells["colId"].Value;
            if (cellValue == null)
                return;

            int staffId = Convert.ToInt32(cellValue);
            var staff = _allStaff.FirstOrDefault(s => s.Id == staffId);

            if (staff == null) return;

            if (btnEdit.Contains(clickPoint))
            {
                EditStaff(staff);
            }
            else if (btnDelete.Contains(clickPoint))
            {
                DeleteStaff(staff);
            }
        }

        private void BtnThemNhanSu_Click(object? sender, EventArgs e)
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Staff, PermissionAction.Add))
                return;

            MessageBox.Show("Chức năng thêm nhân sự mới!\n\nSẽ mở form nhập thông tin nhân viên.", 
                "Thêm Nhân Sự", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EditStaff(StaffMember staff)
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Staff, PermissionAction.Edit))
                return;

            MessageBox.Show($"Chỉnh sửa nhân sự:\n\n{staff.HoTen}\n{staff.ChucVu} - {staff.PhongBan}", 
                "Chỉnh Sửa Nhân Sự", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeleteStaff(StaffMember staff)
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Staff, PermissionAction.Delete))
                return;

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân sự:\n\n{staff.HoTen} ({staff.ChucVu})?", 
                "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                _allStaff.Remove(staff);
                FilterStaff();
                MessageBox.Show("Đã xóa nhân sự thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ApplySecurity()
        {
            AuthorizationGuard.ApplyControlSecurity(SystemMenus.Staff, btnThemNhanSu, btnExport);
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
        }

        private void dgvNhanSu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // Staff Member Model
        private class StaffMember
        {
            public int Id { get; set; }
            public string HoTen { get; set; } = "";
            public string ChucVu { get; set; } = "";
            public string PhongBan { get; set; } = "";
            public string SoDienThoai { get; set; } = "";
            public string Email { get; set; } = "";
            public string TrangThai { get; set; } = "";
        }
    }
}
