using Core.Configs;
using Core.Helpers;
using Core.Security;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UserSession = Core.Security.UserSession;

namespace QuanLyVanTai.UI
{
    public partial class UcManageService : UserControl
    {
        // UI Controls
        private readonly Panel pnlHeader = new();
        private readonly FlowLayoutPanel flpServiceCards = new();
        private readonly TextBox txtSearch = new();
        private readonly ComboBox cboFilterCategory = new();
        private readonly IconButton btnAddService = new();
        private readonly IconButton btnRefresh = new();

        // Service Categories & Data (Demo data - có thể thay bằng database)
        private List<ServiceItem> _allServices = new();
        private List<ServiceItem> _filteredServices = new();

        public UcManageService()
        {
            InitializeComponent();
            BuildModernLayout();
            LoadDemoServices();
            ApplySecurity();
        }

        private void BuildModernLayout()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = Color.FromArgb(248, 250, 252);

            // ==========================================
            // 1. HEADER với Search & Filters
            // ==========================================
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 140;
            pnlHeader.BackColor = Color.White;
            pnlHeader.Padding = new Padding(25, 20, 25, 20);

            // Page Title
            Label lblPageTitle = new Label
            {
                Text = "🎯 QUẢN LÝ DỊCH VỤ VẬN TẢI",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(25, 20),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblPageTitle);

            // Search Box
            Label lblSearch = new Label
            {
                Text = "Tìm kiếm dịch vụ:",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(25, 60),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblSearch);

            txtSearch.Location = new Point(25, 82);
            txtSearch.Size = new Size(350, 35);
            txtSearch.Font = new Font("Segoe UI", 10.5F);
            txtSearch.PlaceholderText = "🔍 Nhập tên dịch vụ, mô tả...";
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.TextChanged += (s, e) => FilterServices();
            pnlHeader.Controls.Add(txtSearch);

            // Category Filter
            Label lblCategory = new Label
            {
                Text = "Loại dịch vụ:",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(395, 60),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblCategory);

            cboFilterCategory.Location = new Point(395, 82);
            cboFilterCategory.Size = new Size(220, 35);
            cboFilterCategory.Font = new Font("Segoe UI", 10F);
            cboFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterCategory.Items.AddRange(new string[] { "Tất cả", "Vận chuyển", "Bảo hiểm", "Ưu đãi", "Hỗ trợ" });
            cboFilterCategory.SelectedIndex = 0;
            cboFilterCategory.SelectedIndexChanged += (s, e) => FilterServices();
            pnlHeader.Controls.Add(cboFilterCategory);

            // Add Button
            ThemeConfig.StyleSuccessButton(btnAddService, IconChar.PlusCircle);
            btnAddService.Text = " Thêm Dịch Vụ";
            btnAddService.Size = new Size(150, 42);
            btnAddService.Location = new Point(640, 78);
            btnAddService.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddService.Click += BtnAddService_Click;
            pnlHeader.Controls.Add(btnAddService);

            // Refresh Button
            ThemeConfig.StyleSecondaryButton(btnRefresh, IconChar.ArrowsRotate);
            btnRefresh.Text = " Làm Mới";
            btnRefresh.Size = new Size(130, 42);
            btnRefresh.Location = new Point(800, 78);
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.BackColor = Color.FromArgb(100, 116, 139);
            btnRefresh.Click += (s, e) => { LoadDemoServices(); FilterServices(); };
            pnlHeader.Controls.Add(btnRefresh);

            this.Controls.Add(pnlHeader);

            // ==========================================
            // 2. CARD GRID Layout (FlowLayoutPanel)
            // ==========================================
            flpServiceCards.Dock = DockStyle.Fill;
            flpServiceCards.BackColor = Color.FromArgb(248, 250, 252);
            flpServiceCards.Padding = new Padding(25, 20, 25, 20);
            flpServiceCards.AutoScroll = true;
            flpServiceCards.WrapContents = true;

            this.Controls.Add(flpServiceCards);
        }

        private void LoadDemoServices()
        {
            _allServices = new List<ServiceItem>
            {
                new ServiceItem
                {
                    Id = 1,
                    Name = "Vận chuyển Express",
                    Description = "Dịch vụ vận chuyển nhanh trong 24h, ưu tiên hàng khách",
                    Category = "Vận chuyển",
                    Price = 150000,
                    Icon = IconChar.TruckFast,
                    IconColor = Color.FromArgb(59, 130, 246),
                    IsActive = true
                },
                new ServiceItem
                {
                    Id = 2,
                    Name = "Bảo hiểm hành khách",
                    Description = "Bảo hiểm tai nạn cho hành khách trong suốt hành trình",
                    Category = "Bảo hiểm",
                    Price = 50000,
                    Icon = IconChar.Shield,
                    IconColor = Color.FromArgb(16, 185, 129),
                    IsActive = true
                },
                new ServiceItem
                {
                    Id = 3,
                    Name = "Giao hàng tận nơi",
                    Description = "Dịch vụ giao hàng từ bến xe đến tận nhà khách hàng",
                    Category = "Vận chuyển",
                    Price = 80000,
                    Icon = IconChar.BoxOpen,
                    IconColor = Color.FromArgb(245, 158, 11),
                    IsActive = true
                },
                new ServiceItem
                {
                    Id = 4,
                    Name = "Ưu đãi sinh viên",
                    Description = "Giảm giá 20% cho sinh viên có thẻ học sinh, sinh viên",
                    Category = "Ưu đãi",
                    Price = 0,
                    Icon = IconChar.GraduationCap,
                    IconColor = Color.FromArgb(139, 92, 246),
                    IsActive = true
                },
                new ServiceItem
                {
                    Id = 5,
                    Name = "Hỗ trợ 24/7",
                    Description = "Tổng đài hỗ trợ khách hàng hoạt động 24/7 mọi lúc mọi nơi",
                    Category = "Hỗ trợ",
                    Price = 0,
                    Icon = IconChar.Headset,
                    IconColor = Color.FromArgb(236, 72, 153),
                    IsActive = true
                },
                new ServiceItem
                {
                    Id = 6,
                    Name = "Vận chuyển hàng cồng kềnh",
                    Description = "Vận chuyển hàng hóa lớn, hành lý quá khổ với xe chuyên dụng",
                    Category = "Vận chuyển",
                    Price = 200000,
                    Icon = IconChar.Dolly,
                    IconColor = Color.FromArgb(239, 68, 68),
                    IsActive = false
                },
                new ServiceItem
                {
                    Id = 7,
                    Name = "Combo vé khứ hồi",
                    Description = "Đặt vé khứ hồi tiết kiệm 15% so với đặt lẻ",
                    Category = "Ưu đãi",
                    Price = 0,
                    Icon = IconChar.Ticket,
                    IconColor = Color.FromArgb(20, 184, 166),
                    IsActive = true
                },
                new ServiceItem
                {
                    Id = 8,
                    Name = "Bảo hiểm hành lý",
                    Description = "Bảo hiểm mất mát, hư hỏng hành lý trong quá trình vận chuyển",
                    Category = "Bảo hiểm",
                    Price = 30000,
                    Icon = IconChar.Briefcase,
                    IconColor = Color.FromArgb(251, 146, 60),
                    IsActive = true
                }
            };

            _filteredServices = new List<ServiceItem>(_allServices);
        }

        private void FilterServices()
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            string category = cboFilterCategory.SelectedItem?.ToString() ?? "Tất cả";

            _filteredServices = _allServices.Where(s =>
            {
                bool matchKeyword = string.IsNullOrEmpty(keyword) ||
                                    s.Name.ToLower().Contains(keyword) ||
                                    s.Description.ToLower().Contains(keyword);

                bool matchCategory = category == "Tất cả" || s.Category == category;

                return matchKeyword && matchCategory;
            }).ToList();

            RenderServiceCards();
        }

        private void RenderServiceCards()
        {
            flpServiceCards.Controls.Clear();

            if (_filteredServices.Count == 0)
            {
                // Empty state
                Panel emptyState = new Panel
                {
                    Size = new Size(400, 200),
                    BackColor = Color.White
                };

                Label lblEmpty = new Label
                {
                    Text = "😔 Không tìm thấy dịch vụ nào",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(148, 163, 184),
                    AutoSize = true,
                    Location = new Point(80, 80)
                };
                emptyState.Controls.Add(lblEmpty);
                flpServiceCards.Controls.Add(emptyState);
                return;
            }

            foreach (var service in _filteredServices)
            {
                Panel card = CreateServiceCard(service);
                flpServiceCards.Controls.Add(card);
            }
        }

        private Panel CreateServiceCard(ServiceItem service)
        {
            Panel card = new Panel
            {
                Size = new Size(380, 180),
                BackColor = Color.White,
                Margin = new Padding(10, 10, 10, 10),
                Padding = new Padding(20, 20, 20, 20),
                BorderStyle = BorderStyle.None,
                Cursor = Cursors.Hand
            };

            // Shadow effect
            card.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(226, 232, 240), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
            };

            // Icon
            IconPictureBox iconBox = new IconPictureBox
            {
                IconChar = service.Icon,
                IconColor = service.IconColor,
                IconSize = 42,
                Size = new Size(50, 50),
                Location = new Point(20, 20)
            };
            card.Controls.Add(iconBox);

            // Service Name
            Label lblName = new Label
            {
                Text = service.Name,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(80, 22),
                Size = new Size(280, 25)
            };
            card.Controls.Add(lblName);

            // Category Badge
            Label lblCategory = new Label
            {
                Text = service.Category,
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                ForeColor = service.IconColor,
                BackColor = Color.FromArgb(240, 253, 244),
                Location = new Point(80, 50),
                AutoSize = true,
                Padding = new Padding(8, 3, 8, 3)
            };
            card.Controls.Add(lblCategory);

            // Description
            Label lblDescription = new Label
            {
                Text = service.Description,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(20, 85),
                Size = new Size(340, 40)
            };
            card.Controls.Add(lblDescription);

            // Price
            Label lblPrice = new Label
            {
                Text = service.Price > 0 ? $"{service.Price:N0} VNĐ" : "Miễn phí",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = service.Price > 0 ? Color.FromArgb(239, 68, 68) : Color.FromArgb(16, 185, 129),
                Location = new Point(20, 135),
                AutoSize = true
            };
            card.Controls.Add(lblPrice);

            // Status Badge
            Label lblStatus = new Label
            {
                Text = service.IsActive ? "✓ Hoạt động" : "⊗ Tạm ngừng",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = service.IsActive ? Color.FromArgb(16, 185, 129) : Color.FromArgb(239, 68, 68),
                BackColor = service.IsActive ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242),
                Location = new Point(155, 135),
                AutoSize = true,
                Padding = new Padding(8, 4, 8, 4)
            };
            card.Controls.Add(lblStatus);

            // Action Buttons (Edit & Delete) - Show on hover
            IconButton btnEdit = new IconButton
            {
                IconChar = IconChar.PenToSquare,
                IconColor = Color.White,
                IconSize = 16,
                Size = new Size(35, 35),
                Location = new Point(290, 130),
                BackColor = Color.FromArgb(59, 130, 246),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
            btnEdit.Click += (s, e) => EditService(service);
            card.Controls.Add(btnEdit);

            IconButton btnDelete = new IconButton
            {
                IconChar = IconChar.TrashAlt,
                IconColor = Color.White,
                IconSize = 16,
                Size = new Size(35, 35),
                Location = new Point(330, 130),
                BackColor = Color.FromArgb(239, 68, 68),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
            btnDelete.Click += (s, e) => DeleteService(service);
            card.Controls.Add(btnDelete);

            // Hover effect
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(249, 250, 251);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            return card;
        }

        private void BtnAddService_Click(object? sender, EventArgs e)
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Service, PermissionAction.Add))
                return;

            MessageBox.Show("Chức năng thêm dịch vụ mới!\n\nSẽ mở form nhập thông tin dịch vụ.", 
                "Thêm Dịch Vụ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EditService(ServiceItem service)
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Service, PermissionAction.Edit))
                return;

            MessageBox.Show($"Chỉnh sửa dịch vụ:\n\n{service.Name}\n{service.Description}", 
                "Chỉnh Sửa Dịch Vụ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeleteService(ServiceItem service)
        {
            if (!AuthorizationGuard.CheckAccess(SystemMenus.Service, PermissionAction.Delete))
                return;

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ:\n\n{service.Name}?", 
                "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                _allServices.Remove(service);
                _filteredServices.Remove(service);
                RenderServiceCards();
                MessageBox.Show("Đã xóa dịch vụ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ApplySecurity()
        {
            AuthorizationGuard.ApplyControlSecurity(SystemMenus.Service, btnAddService);
        }

        // Service Item Model
        private class ServiceItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public string Category { get; set; } = "";
            public decimal Price { get; set; }
            public IconChar Icon { get; set; }
            public Color IconColor { get; set; }
            public bool IsActive { get; set; }
        }
    }
}

