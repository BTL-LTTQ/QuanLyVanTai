using System.ComponentModel;
using Core.Configs;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.UI.Forms;
using QuanLyVanTai.UI.Models;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcCustomerTicketHistory : UserControl
    {
        private readonly TicketService _ticketService = new();
        private readonly CustomerBookingService _bookingService = new();
        private List<TicketDisplayModel> _displayedTickets = new();

        public event Action<string, bool>? OnStatusChanged;
        public event Action? OnNavigateToBooking;

        public UcCustomerTicketHistory() : this(null)
        {
        }

        public UcCustomerTicketHistory(string? searchKeyword)
        {
            InitializeComponent();

            // Chống crash Visual Studio Designer
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            SetupUi();
            WireEvents();

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                txtTimKiem.Text = searchKeyword.Trim();
            }
        }

        public void SetSearchKeyword(string? keyword)
        {
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                txtTimKiem.Text = keyword.Trim();
                _ = LoadTicketsAsync();
            }
        }

        private void SetupUi()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            // Áp dụng theme button theo chuẩn ThemeConfig
            ThemeConfig.StylePrimaryButton(btnChiTiet, IconChar.CircleInfo);
            ThemeConfig.StylePrimaryButton(btnTimKiem, IconChar.Search);
            ThemeConfig.StyleSecondaryButton(btnLamMoi, IconChar.RotateLeft);
            ThemeConfig.StyleSuccessButton(btnInVe, IconChar.Print);
            ThemeConfig.StyleDangerButton(btnHuyVe, IconChar.Trash);
            ThemeConfig.StyleSecondaryButton(btnQuayLai, IconChar.ArrowLeft);

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Tất cả", "Booked", "Cancelled", "Completed" });
            cboTrangThai.SelectedIndex = 0;

            dgvTickets.AutoGenerateColumns = false;
            dgvTickets.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
        }

        private void WireEvents()
        {
            this.Load += async (s, e) => await LoadTicketsAsync();

            btnTimKiem.Click += async (s, e) => await LoadTicketsAsync();
            btnLamMoi.Click += async (s, e) => await RefreshDataAsync();
            btnQuayLai.Click += (s, e) => OnNavigateToBooking?.Invoke();

            txtTimKiem.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await LoadTicketsAsync();
                }
            };

            cboTrangThai.SelectedIndexChanged += async (s, e) => await LoadTicketsAsync();

            btnChiTiet.Click += (s, e) => ViewSelectedTicketDetail();
            btnInVe.Click += async (s, e) => await PrintSelectedTicketAsync();
            btnHuyVe.Click += async (s, e) => await CancelSelectedTicketAsync();

            dgvTickets.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    ViewSelectedTicketDetail();
                }
            };

            dgvTickets.CellFormatting += DgvTickets_CellFormatting;
            dgvTickets.SelectionChanged += (s, e) => UpdateActionButtonsState();
        }

        public async Task RefreshDataAsync()
        {
            txtTimKiem.Clear();
            cboTrangThai.SelectedIndex = 0;
            await LoadTicketsAsync();
        }

        public async Task LoadTicketsAsync()
        {
            SetStatus("Đang tải danh sách vé...", true);
            try
            {
                string kw = txtTimKiem.Text.Trim();
                string status = cboTrangThai.SelectedItem?.ToString() ?? "Tất cả";

                var criteria = new TicketFilterCriteria
                {
                    Keyword = string.IsNullOrWhiteSpace(kw) ? null : kw,
                    TicketStatus = status
                };

                var rawList = await _ticketService.SearchAndFilterTicketsAsync(criteria);

                _displayedTickets = rawList.Select(t => new TicketDisplayModel
                {
                    Id = t.Id,
                    TicketCode = t.TicketCode,
                    CustomerName = t.CustomerName,
                    CustomerPhone = t.CustomerPhone,
                    RouteName = t.Route?.RouteName ?? "Tuyến liên tỉnh",
                    LicensePlate = t.Vehicle?.LicensePlate ?? "Chưa xếp xe",
                    SeatNumber = t.SeatNumber,
                    DepartureTime = t.DepartureTime,
                    DepartureTimeDisplay = t.DepartureTime.ToString("dd/MM/yyyy HH:mm"),
                    Price = t.Price,
                    PriceDisplay = $"{t.Price:N0} đ",
                    TicketStatus = t.TicketStatus,
                    TicketStatusDisplay = t.TicketStatus switch
                    {
                        "Booked" => "Đã đặt",
                        "Cancelled" => "Đã hủy",
                        "Completed" => "Đã hoàn thành",
                        _ => t.TicketStatus
                    },
                    PaymentStatus = t.PaymentStatus,
                    PaymentStatusDisplay = t.PaymentStatus switch
                    {
                        "Paid" or "Đã thanh toán" => "Đã thanh toán",
                        "Pending" or "Chờ thanh toán" => "Chờ thanh toán",
                        "Failed" or "Thất bại" => "Thất bại",
                        "Refunded" or "Đã hoàn tiền" => "Đã hoàn tiền",
                        _ => t.PaymentStatus
                    },
                    CreatedAt = t.CreatedAt.ToLocalTime(),
                    CreatedAtDisplay = t.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm")
                }).ToList();

                dgvTickets.DataSource = null;
                dgvTickets.AutoGenerateColumns = false;
                dgvTickets.DataSource = _displayedTickets;

                SetStatus($"Tổng cộng: {_displayedTickets.Count} vé", false);
                UpdateActionButtonsState();
            }
            catch (Exception ex)
            {
                SetStatus("Lỗi tải vé", false);
                MessageBox.Show("Lỗi tải danh sách lịch sử vé: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task PrintSelectedTicketAsync()
        {
            var selected = GetSelectedTicket();
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn 1 chiếc vé trên bảng để in!", "Chưa chọn vé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selected.TicketStatus == "Cancelled")
            {
                MessageBox.Show(
                    $"Vé [{selected.TicketCode}] đã bị HỦY trước đó!\nKhông thể in thẻ lên xe cho vé không còn hiệu lực.",
                    "Vé Đã Bị Hủy",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            SetStatus($"Đang chuẩn bị mẫu in vé [{selected.TicketCode}]...", true);
            try
            {
                var receipts = await _bookingService.GetTicketReceiptsAsync(new List<int> { selected.Id });
                if (receipts.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin chi tiết của vé này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var previewForm = new FrmTicketPrintPreview(receipts);
                previewForm.ShowDialog(this.FindForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị in vé: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetStatus("Sẵn sàng", false);
            }
        }

        private async Task CancelSelectedTicketAsync()
        {
            var selected = GetSelectedTicket();
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn 1 chiếc vé trên bảng cần hủy!", "Chưa chọn vé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selected.TicketStatus == "Cancelled")
            {
                MessageBox.Show($"Vé [{selected.TicketCode}] đã được hủy trước đó rồi!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selected.DepartureTime <= DateTime.Now)
            {
                MessageBox.Show($"Chuyến xe đã xuất bến vào lúc {selected.DepartureTime:HH:mm dd/MM/yyyy}.\nKhông thể hủy vé đối với chuyến xe đã chạy!",
                    "Không Thể Hủy Vé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeSpan timeUntilDeparture = selected.DepartureTime - DateTime.Now;
            if (timeUntilDeparture < TimeSpan.FromHours(1))
            {
                int minutesLeft = Math.Max(0, (int)timeUntilDeparture.TotalMinutes);
                string warnMsg = $"KHÔNG THỂ HỦY VÉ!\n\n" +
                                 $"• Chuyến xe khởi hành lúc: {selected.DepartureTime:HH:mm dd/MM/yyyy}\n" +
                                 $"• Thời gian còn lại: chỉ còn {minutesLeft} phút nữa là xe xuất bến.\n\n" +
                                 $"Theo quy định của nhà xe, quý khách CHỈ ĐƯỢC HỦY VÉ TRƯỚC GIỜ KHỞI HÀNH TỐI THIỂU 1 GIỜ (60 phút) để bảo đảm quyền lợi xếp xe.";

                MessageBox.Show(warnMsg, "Vi Phạm Quy Định Hủy Vé (Dưới 1 Giờ)", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string confirmMsg = $"XÁC NHẬN HỦY VÉ XE:\n\n" +
                                $"• Mã vé: {selected.TicketCode}\n" +
                                $"• Tuyến xe: {selected.RouteName}\n" +
                                $"• Số ghế: {selected.SeatNumber} (Xe: {selected.LicensePlate})\n" +
                                $"• Khởi hành: {selected.DepartureTime:dd/MM/yyyy HH:mm} (còn {(int)timeUntilDeparture.TotalHours} giờ {timeUntilDeparture.Minutes} phút)\n" +
                                $"• Cước hoàn trả: {selected.Price:N0} VNĐ\n\n" +
                                $"Quý khách có chắc chắn muốn hủy chiếc vé này không?\n(Chỗ ngồi sẽ được mở lại cho hành khách khác trên hệ thống).";

            if (MessageBox.Show(confirmMsg, "Xác nhận hủy vé", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            SetStatus($"Đang tiến hành hủy vé [{selected.TicketCode}]...", true);
            try
            {
                var result = await _ticketService.CancelTicketAsync(selected.Id);

                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Hủy Vé Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadTicketsAsync();
                }
                else
                {
                    MessageBox.Show(result.Message, "Không Thể Hủy Vé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình hủy vé: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetStatus("Sẵn sàng", false);
            }
        }

        private TicketDisplayModel? GetSelectedTicket()
        {
            if (dgvTickets.CurrentRow?.DataBoundItem is TicketDisplayModel item)
            {
                return item;
            }
            return null;
        }

        private void ViewSelectedTicketDetail()
        {
            var selected = GetSelectedTicket();
            if (selected == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn 1 chiếc vé trên danh sách để xem chi tiết!",
                    "Chưa Chọn Vé",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using var detailForm = new FrmTicketDetail(selected.Id);
            detailForm.ShowDialog(this.FindForm());
            if (detailForm.WasCancelled)
            {
                _ = LoadTicketsAsync();
            }
        }

        private void UpdateActionButtonsState()
        {
            var selected = GetSelectedTicket();
            if (selected == null)
            {
                btnChiTiet.Enabled = false;
                btnInVe.Enabled = false;
                btnHuyVe.Enabled = false;
                return;
            }

            btnChiTiet.Enabled = true;

            bool isCancelled = selected.TicketStatus == "Cancelled";
            btnInVe.Enabled = !isCancelled;
            btnHuyVe.Enabled = !isCancelled;
        }

        private void DgvTickets_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvTickets.Rows.Count) return;

            var row = dgvTickets.Rows[e.RowIndex];
            if (row.DataBoundItem is TicketDisplayModel item)
            {
                if (item.TicketStatus == "Cancelled")
                {
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(148, 163, 184);
                    row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(148, 163, 184);
                }
                else if (item.TicketStatus == "Completed")
                {
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
                }
                else
                {
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
                }
            }
        }

        private void SetStatus(string msg, bool isLoading)
        {
            OnStatusChanged?.Invoke(msg, isLoading);
        }
    }
}
