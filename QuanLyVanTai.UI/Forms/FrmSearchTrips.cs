using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Configs;
using Core.Helpers;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;

namespace QuanLyVanTai.UI.Forms
{
    public partial class FrmSearchTrips : Form
    {
        private readonly CustomerBookingService _bookingService = new();
        private System.Windows.Forms.Timer? _debounceTimer;
        private List<AvailableTripDto> _trips = new();

        public AvailableTripDto? SelectedTrip { get; private set; }

        public FrmSearchTrips(DateTime? initialDate = null, string? initialKeyword = null)
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            this.KeyPreview = true;
            if (initialDate.HasValue)
            {
                dtpDate.Value = initialDate.Value;
            }
            if (!string.IsNullOrWhiteSpace(initialKeyword))
            {
                txtKeyword.Text = initialKeyword;
            }

            SetupUi();
            WireEvents();
        }

        private void SetupUi()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            ThemeConfig.StyleSuccessButton(btnSelect, IconChar.Check);
            ThemeConfig.StyleSecondaryButton(btnReset, IconChar.RotateLeft);
            ThemeConfig.StyleSecondaryButton(btnClose, IconChar.Xmark);

            cboPrice.Items.Clear();
            cboPrice.Items.AddRange(new object[]
            {
                "Tất cả mức giá",
                "Dưới 200.000 đ",
                "200.000 đ - 400.000 đ",
                "Trên 400.000 đ"
            });
            cboPrice.SelectedIndex = 0;

            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvTrips.Columns.Clear();
            dgvTrips.AutoGenerateColumns = false;

            dgvTrips.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCode",
                HeaderText = "Mã Chuyến",
                DataPropertyName = "ScheduleCode",
                Width = 120,
                DefaultCellStyle = { Font = new Font("Segoe UI", 9F, FontStyle.Bold) }
            });

            dgvTrips.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTime",
                HeaderText = "Giờ Khởi Hành",
                DataPropertyName = "DepartureTime",
                Width = 135,
                DefaultCellStyle = { Format = "HH:mm dd/MM/yyyy", Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(15, 23, 42) }
            });

            dgvTrips.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRoute",
                HeaderText = "Tuyến Đường",
                DataPropertyName = "RouteName",
                Width = 220
            });

            dgvTrips.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colVehicle",
                HeaderText = "Xe & Biển Số",
                DataPropertyName = "DisplayTextVehicle",
                Width = 170
            });

            dgvTrips.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSeats",
                HeaderText = "Chỗ Trống",
                DataPropertyName = "DisplayTextSeats",
                Width = 110,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(14, 165, 233) }
            });

            dgvTrips.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrice",
                HeaderText = "Giá Vé (đ)",
                DataPropertyName = "BasePrice",
                Width = 120,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(16, 185, 129) }
            });

            dgvTrips.EnableHeadersVisualStyles = false;
            dgvTrips.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvTrips.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            dgvTrips.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dgvTrips.ColumnHeadersHeight = 36;
            dgvTrips.RowTemplate.Height = 34;
            dgvTrips.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            // Gắn highlighter từ khóa tìm kiếm (Real-time highlight)
            DataGridViewHighlightHelper.AttachSearchHighlighter(dgvTrips, () => txtKeyword.Text);
        }

        private void WireEvents()
        {
            this.Load += async (s, e) =>
            {
                await ExecuteSearchAsync();
                txtKeyword.Focus();
            };

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            };

            // Real-time debounce tìm kiếm: gõ đến đâu lọc đến đó (300ms)
            txtKeyword.TextChanged += (s, e) => ScheduleSearch();
            dtpDate.ValueChanged += (s, e) => ScheduleSearch();
            cboPrice.SelectedIndexChanged += (s, e) => ScheduleSearch();

            btnReset.Click += (s, e) =>
            {
                txtKeyword.Clear();
                dtpDate.Value = DateTime.Today;
                cboPrice.SelectedIndex = 0;
            };

            dgvTrips.CellDoubleClick += (s, e) => ConfirmSelection();
            btnSelect.Click += (s, e) => ConfirmSelection();
            btnClose.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
        }

        private void ScheduleSearch()
        {
            _debounceTimer?.Stop();
            _debounceTimer?.Dispose();
            _debounceTimer = new System.Windows.Forms.Timer { Interval = 300 };
            _debounceTimer.Tick += async (s, e) =>
            {
                _debounceTimer?.Stop();
                _debounceTimer?.Dispose();
                _debounceTimer = null;
                await ExecuteSearchAsync();
            };
            _debounceTimer.Start();
        }

        private async Task ExecuteSearchAsync()
        {
            try
            {
                decimal? minPrice = null;
                decimal? maxPrice = null;

                switch (cboPrice.SelectedIndex)
                {
                    case 1: maxPrice = 200000m; break;
                    case 2: minPrice = 200000m; maxPrice = 400000m; break;
                    case 3: minPrice = 400000m; break;
                }

                var criteria = new TripSearchCriteriaDto
                {
                    Keyword = txtKeyword.Text,
                    DepartureDate = dtpDate.Value.Date,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice
                };

                _trips = await _bookingService.SearchAvailableTripsAsync(criteria);

                // Tạo đối tượng view hiển thị
                var displayList = _trips.Select(t => new
                {
                    t.ScheduleId,
                    t.ScheduleCode,
                    t.DepartureTime,
                    t.RouteName,
                    DisplayTextVehicle = $"{t.LicensePlate} ({t.VehicleType})",
                    DisplayTextSeats = $"{t.AvailableSeats}/{t.TotalSeats} chỗ",
                    t.BasePrice,
                    TripItem = t
                }).ToList();

                dgvTrips.DataSource = displayList;
                lblResultCount.Text = $"Tìm thấy: {_trips.Count} chuyến xe khả dụng";
                dgvTrips.Invalidate();
            }
            catch (Exception ex)
            {
                lblResultCount.Text = $"Lỗi tìm kiếm: {ex.Message}";
            }
        }

        private void ConfirmSelection()
        {
            if (dgvTrips.CurrentRow != null && dgvTrips.CurrentRow.Index >= 0 && dgvTrips.CurrentRow.Index < _trips.Count)
            {
                SelectedTrip = _trips[dgvTrips.CurrentRow.Index];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chuyến xe từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
