using System.ComponentModel;
using Core.Configs;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.UI.Forms
{
    public partial class FrmCustomerTicketHistory : Form
    {
        private readonly TicketService _ticketService = new();
        private readonly CustomerBookingService _bookingService = new();
        private List<TicketDisplayModel> _displayedTickets = new();
        private string? _initialPhoneOrCode;

        public FrmCustomerTicketHistory() : this(null)
        {
        }

        public FrmCustomerTicketHistory(string? searchKeyword)
        {
            InitializeComponent();
            _initialPhoneOrCode = searchKeyword;

            // 🛡️ BẢO VỆ DESIGNER: Chặn Visual Studio Designer thực thi kết nối CSDL hoặc gọi Service (Mục 2.2 SKILL)
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            this.KeyPreview = true;
            SetupUi();
            WireEvents();
        }

        private void SetupUi()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            // Áp dụng theme button theo chuẩn (ThemeConfig & SKILL Mục 13)
            ThemeConfig.StylePrimaryButton(btnChiTiet, IconChar.CircleInfo);
            btnChiTiet.Size = new Size(150, 42);

            ThemeConfig.StylePrimaryButton(btnTimKiem, IconChar.Search);
            btnTimKiem.Size = new Size(120, 36);

            ThemeConfig.StyleSecondaryButton(btnLamMoi, IconChar.RotateLeft);
            btnLamMoi.Size = new Size(118, 36);

            ThemeConfig.StyleSuccessButton(btnInVe, IconChar.Print);
            btnInVe.Size = new Size(150, 42);

            ThemeConfig.StyleDangerButton(btnHuyVe, IconChar.Trash);
            btnHuyVe.Size = new Size(140, 42);

            ThemeConfig.StyleSecondaryButton(btnDong, IconChar.Xmark);
            btnDong.Size = new Size(110, 42);

            // Cấu hình danh sách trạng thái lọc
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Tất cả", "Booked", "Cancelled", "Completed" });
            cboTrangThai.SelectedIndex = 0;

            if (!string.IsNullOrWhiteSpace(_initialPhoneOrCode))
            {
                txtTimKiem.Text = _initialPhoneOrCode.Trim();
            }

            // Đảm bảo không tự động sinh thêm cột từ model (chỉ giữ đúng 5 cột thiết kế)
            dgvTickets.AutoGenerateColumns = false;
            dgvTickets.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void WireEvents()
        {
            this.Load += async (s, e) => await LoadTicketsAsync();
            this.KeyDown += FrmCustomerTicketHistory_KeyDown;

            // Đồng hồ StatusStrip (Đồng nhất với FormMain_KhachHang)
            timerClock.Interval = 1000;
            timerClock.Tick += (s, e) =>
            {
                lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };
            timerClock.Start();

            // MenuStrip handlers
            mnuLamMoi.Click += async (s, e) => await RefreshDataAsync();
            mnuDong.Click += (s, e) => this.Close();
            mnuChiTiet.Click += (s, e) => ViewSelectedTicketDetail();
            mnuInVe.Click += async (s, e) => await PrintSelectedTicketAsync();
            mnuHuyVe.Click += async (s, e) => await CancelSelectedTicketAsync();

            // Nút bấm tác vụ
            btnTimKiem.Click += async (s, e) => await LoadTicketsAsync();
            btnLamMoi.Click += async (s, e) => await RefreshDataAsync();

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
            btnDong.Click += (s, e) => this.Close();

            // Nhấp đúp chuột vào dòng DataGridView để xem chi tiết vé
            dgvTickets.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    ViewSelectedTicketDetail();
                }
            };

            // Định dạng màu sắc hàng theo trạng thái
            dgvTickets.CellFormatting += DgvTickets_CellFormatting;

            // Tự động bật/tắt các nút In vé, Hủy vé theo trạng thái dòng đang chọn
            dgvTickets.SelectionChanged += (s, e) => UpdateActionButtonsState();
        }

        private async Task RefreshDataAsync()
        {
            txtTimKiem.Clear();
            cboTrangThai.SelectedIndex = 0;
            await LoadTicketsAsync();
        }

        private async Task LoadTicketsAsync()
        {
            SetLoading(true, "Đang tải danh sách vé...");
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
                    CreatedAt = t.CreatedAt.ToLocalTime(),
                    CreatedAtDisplay = t.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm")
                }).ToList();

                dgvTickets.DataSource = null;
                dgvTickets.AutoGenerateColumns = false;
                dgvTickets.DataSource = _displayedTickets;

                lblTotalCount.Text = $"Tổng cộng: {_displayedTickets.Count} vé";
                SetLoading(false, $"Đã tải {_displayedTickets.Count} vé");
                UpdateActionButtonsState();
            }
            catch (Exception ex)
            {
                SetLoading(false, "Lỗi tải vé");
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

            // ⚠️ RÀNG BUỘC NGHIỆP VỤ: Tuyệt đối không cho phép in thẻ lên xe cho vé đã hủy
            if (selected.TicketStatus == "Cancelled")
            {
                MessageBox.Show(
                    $"Vé [{selected.TicketCode}] đã bị HỦY trước đó!\nKhông thể in thẻ lên xe cho vé không còn hiệu lực.",
                    "Vé Đã Bị Hủy",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            SetLoading(true, $"Đang chuẩn bị mẫu in vé [{selected.TicketCode}]...");
            try
            {
                var receipts = await _bookingService.GetTicketReceiptsAsync(new List<int> { selected.Id });
                if (receipts.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin chi tiết của vé này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var previewForm = new FrmTicketPrintPreview(receipts);
                previewForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị in vé: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false, "Sẵn sàng");
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

            // 1. Kiểm tra trạng thái đã hủy chưa
            if (selected.TicketStatus == "Cancelled")
            {
                MessageBox.Show($"Vé [{selected.TicketCode}] đã được hủy trước đó rồi!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Kiểm tra giờ xuất bến
            if (selected.DepartureTime <= DateTime.Now)
            {
                MessageBox.Show($"Chuyến xe đã xuất bến vào lúc {selected.DepartureTime:HH:mm dd/MM/yyyy}.\nKhông thể hủy vé đối với chuyến xe đã chạy!",
                    "Không Thể Hủy Vé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Quy tắc cốt lõi: Chỉ cho phép hủy trước tối thiểu 1 giờ (60 phút)
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

            // 4. Xác nhận hủy vé
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

            SetLoading(true, $"Đang tiến hành hủy vé [{selected.TicketCode}]...");
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
                SetLoading(false, "Sẵn sàng");
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
            detailForm.ShowDialog(this);
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
                mnuChiTiet.Enabled = false;
                mnuInVe.Enabled = false;
                mnuHuyVe.Enabled = false;
                return;
            }

            btnChiTiet.Enabled = true;
            mnuChiTiet.Enabled = true;

            // Ràng buộc nghiệp vụ: Vé đã hủy thì không được In vé và không được Hủy thêm lần nữa
            bool isCancelled = selected.TicketStatus == "Cancelled";
            btnInVe.Enabled = !isCancelled;
            btnHuyVe.Enabled = !isCancelled;
            mnuInVe.Enabled = !isCancelled;
            mnuHuyVe.Enabled = !isCancelled;
        }

        private void DgvTickets_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvTickets.Rows.Count) return;

            var row = dgvTickets.Rows[e.RowIndex];
            if (row.DataBoundItem is TicketDisplayModel item)
            {
                if (item.TicketStatus == "Cancelled")
                {
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(156, 163, 175);
                    if (dgvTickets.Columns[e.ColumnIndex].Name == "colTicketStatus")
                    {
                        e.CellStyle!.ForeColor = Color.FromArgb(220, 38, 38);
                        e.CellStyle!.Font = new Font(dgvTickets.Font, FontStyle.Bold);
                    }
                }
                else if (item.TicketStatus == "Booked")
                {
                    if (dgvTickets.Columns[e.ColumnIndex].Name == "colTicketStatus")
                    {
                        e.CellStyle!.ForeColor = Color.FromArgb(16, 124, 65);
                        e.CellStyle!.Font = new Font(dgvTickets.Font, FontStyle.Bold);
                    }
                }
            }
        }

        private void FrmCustomerTicketHistory_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                this.Close();
            }
            else if (e.KeyCode == Keys.F5)
            {
                e.Handled = true;
                _ = RefreshDataAsync();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                e.Handled = true;
                ViewSelectedTicketDetail();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                _ = CancelSelectedTicketAsync();
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                e.Handled = true;
                _ = PrintSelectedTicketAsync();
            }
        }

        private void SetLoading(bool isLoading, string statusText = "Đang xử lý...")
        {
            prgAction.Visible = isLoading;
            lblStatus.Text = statusText;
            btnTimKiem.Enabled = !isLoading;
            btnLamMoi.Enabled = !isLoading;
            if (isLoading)
            {
                btnChiTiet.Enabled = false;
                btnInVe.Enabled = false;
                btnHuyVe.Enabled = false;
            }
            else
            {
                UpdateActionButtonsState();
            }
        }

        public class TicketDisplayModel
        {
            public int Id { get; set; }
            public string TicketCode { get; set; } = string.Empty;
            public string CustomerName { get; set; } = string.Empty;
            public string CustomerPhone { get; set; } = string.Empty;
            public string RouteName { get; set; } = string.Empty;
            public string LicensePlate { get; set; } = string.Empty;
            public string SeatNumber { get; set; } = string.Empty;
            public DateTime DepartureTime { get; set; }
            public string DepartureTimeDisplay { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public string PriceDisplay { get; set; } = string.Empty;
            public string TicketStatus { get; set; } = string.Empty;
            public string TicketStatusDisplay { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
            public string CreatedAtDisplay { get; set; } = string.Empty;
        }
    }
}
