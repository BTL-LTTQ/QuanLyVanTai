using System;
using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Configs;
using Core.Security;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;

namespace QuanLyVanTai.UI.Forms
{
    public partial class FrmPassengerCheckIn : Form
    {
        private readonly PassengerCheckInService _checkInService = new();
        private string? _lastScannedTicketCode = null;

        public FrmPassengerCheckIn()
        {
            InitializeComponent();

            // Chống crash Visual Studio Designer
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

            // Nút bấm
            ThemeConfig.StyleSuccessButton(btnCheckIn, IconChar.Check);
            ThemeConfig.StylePrimaryButton(btnScanFile, IconChar.FolderOpen);
            ThemeConfig.StyleSecondaryButton(btnRevert, IconChar.RotateLeft);
            ThemeConfig.StyleSecondaryButton(btnRefreshList, IconChar.RotateLeft);
            ThemeConfig.StyleSecondaryButton(btnClose, IconChar.Xmark);

            // Bảng danh sách
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvCheckedIn.Columns.Clear();
            dgvCheckedIn.AutoGenerateColumns = false;

            dgvCheckedIn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTicketCode",
                HeaderText = "Mã Vé",
                DataPropertyName = "TicketCode",
                Width = 115,
                DefaultCellStyle = { Font = new Font("Segoe UI", 9F, FontStyle.Bold) }
            });

            dgvCheckedIn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCustomer",
                HeaderText = "Hành Khách",
                DataPropertyName = "CustomerName",
                Width = 135
            });

            dgvCheckedIn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSeat",
                HeaderText = "Ghế",
                DataPropertyName = "SeatNumber",
                Width = 65,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.FromArgb(14, 165, 233) }
            });

            dgvCheckedIn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPhone",
                HeaderText = "Số Điện Thoại",
                DataPropertyName = "CustomerPhone",
                Width = 105,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvCheckedIn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRoute",
                HeaderText = "Tuyến Đường",
                DataPropertyName = "RouteName",
                Width = 160
            });

            dgvCheckedIn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colVehicle",
                HeaderText = "Biển Số",
                DataPropertyName = "VehiclePlate",
                Width = 95,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvCheckedIn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTime",
                HeaderText = "Giờ Check-in",
                DataPropertyName = "CheckInTime",
                Width = 125,
                DefaultCellStyle = { Format = "HH:mm:ss dd/MM", Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvCheckedIn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStaff",
                HeaderText = "Nhân Viên",
                DataPropertyName = "CheckedInBy",
                Width = 95
            });

            dgvCheckedIn.EnableHeadersVisualStyles = false;
            dgvCheckedIn.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvCheckedIn.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            dgvCheckedIn.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dgvCheckedIn.ColumnHeadersHeight = 36;
            dgvCheckedIn.RowTemplate.Height = 32;
            dgvCheckedIn.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        }

        private void WireEvents()
        {
            this.Load += async (s, e) =>
            {
                await LoadCheckedInListAsync();
                txtQrInput.Focus();
            };

            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            };

            txtQrInput.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await HandleCheckInActionAsync();
                }
            };

            btnCheckIn.Click += async (s, e) => await HandleCheckInActionAsync();
            btnScanFile.Click += async (s, e) => await HandleScanFileAsync();
            btnRevert.Click += async (s, e) => await HandleRevertActionAsync();
            btnRefreshList.Click += async (s, e) => await LoadCheckedInListAsync();
            btnClose.Click += (s, e) => this.Close();
        }

        private async Task HandleCheckInActionAsync()
        {
            string rawInput = txtQrInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(rawInput))
            {
                MessageBox.Show("Vui lòng nhập mã vé hoặc đưa mã QR trước đầu đọc scanner!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQrInput.Focus();
                return;
            }

            lblFooterStatus.Text = "Đang đối chiếu cơ sở dữ liệu và xử lý check-in...";
            string staffName = UserSession.CurrentUsername ?? "StaffCheckIn";

            try
            {
                var result = await _checkInService.ProcessCheckInAsync(rawInput, staffName);
                DisplayCheckInResult(result);

                if (result.Success)
                {
                    _lastScannedTicketCode = result.TicketCode;
                    lblFooterStatus.Text = $"Check-in thành công vé {result.TicketCode} lúc {DateTime.Now:HH:mm:ss}";
                    await LoadCheckedInListAsync();
                }
                else
                {
                    lblFooterStatus.Text = result.Message;
                }
            }
            catch (Exception ex)
            {
                lblFooterStatus.Text = $"Lỗi xử lý: {ex.Message}";
                MessageBox.Show($"Lỗi trong quá trình check-in: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtQrInput.SelectAll();
                txtQrInput.Focus();
            }
        }

        private async Task HandleScanFileAsync()
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Chọn hình ảnh mã QR vé xe hoặc vé điện tử",
                Filter = "Tệp hình ảnh (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|Tất cả tệp (*.*)|*.*"
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                lblFooterStatus.Text = $"Đang đọc mã QR từ tệp: {System.IO.Path.GetFileName(ofd.FileName)}...";
                try
                {
                    string? decoded = _checkInService.DecodeQrFromFile(ofd.FileName);
                    if (string.IsNullOrWhiteSpace(decoded))
                    {
                        MessageBox.Show("Không thể giải mã mã QR từ tệp hình ảnh đã chọn. Vui lòng chọn ảnh rõ nét hơn!",
                                        "Lỗi đọc mã QR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        lblFooterStatus.Text = "Giải mã thất bại từ tệp hình ảnh.";
                        return;
                    }

                    txtQrInput.Text = decoded;
                    await HandleCheckInActionAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi đọc file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task HandleRevertActionAsync()
        {
            if (string.IsNullOrWhiteSpace(_lastScannedTicketCode))
            {
                MessageBox.Show("Chưa có lượt check-in nào trong phiên để hoàn tác!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn hoàn tác trạng thái check-in của vé '{_lastScannedTicketCode}' trở lại 'Booked'?",
                                          "Xác nhận hoàn tác", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                bool reverted = await _checkInService.RevertCheckInAsync(_lastScannedTicketCode, UserSession.CurrentUsername);
                if (reverted)
                {
                    MessageBox.Show($"Đã hoàn tác check-in vé {_lastScannedTicketCode} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetCardStatus();
                    _lastScannedTicketCode = null;
                    await LoadCheckedInListAsync();
                }
                else
                {
                    MessageBox.Show("Không thể hoàn tác vé này (có thể vé không ở trạng thái Boarded).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DisplayCheckInResult(CheckInResultDto result)
        {
            if (result.Success)
            {
                // Thẻ lên xe hợp lệ - Màu xanh lá thành công
                pnlCardStatus.BackColor = Color.FromArgb(16, 185, 129);
                lblCardStatusTitle.ForeColor = Color.White;
                lblCardStatusTitle.Text = "✅ ĐÃ LÊN XE THÀNH CÔNG";

                lblCardTicketCode.Text = result.TicketCode;
                lblCardCustomerName.Text = result.CustomerName;
                lblCardCustomerPhone.Text = result.CustomerPhone;
                lblCardSeat.Text = result.SeatNumber;
                lblCardSeat.ForeColor = Color.FromArgb(16, 185, 129);
                lblCardRoute.Text = result.RouteName;
                lblCardVehicle.Text = result.VehiclePlate;
                lblCardDeparture.Text = result.DepartureTime.ToString("HH:mm dd/MM/yyyy");
                lblCardCheckInTime.Text = result.CheckInTime.ToString("HH:mm:ss dd/MM/yyyy");

                try { SystemSounds.Asterisk.Play(); } catch { }
            }
            else
            {
                if (result.Status == CheckInStatus.AlreadyBoarded)
                {
                    // Cảnh báo vé đã lên xe - Màu cam hổ phách
                    pnlCardStatus.BackColor = Color.FromArgb(245, 158, 11);
                    lblCardStatusTitle.ForeColor = Color.White;
                    lblCardStatusTitle.Text = "⚠️ VÉ ĐÃ CHECK-IN LÊN XE TRƯỚC ĐÓ";

                    lblCardTicketCode.Text = result.TicketCode;
                    lblCardCustomerName.Text = result.CustomerName;
                    lblCardCustomerPhone.Text = result.CustomerPhone;
                    lblCardSeat.Text = result.SeatNumber;
                    lblCardSeat.ForeColor = Color.FromArgb(245, 158, 11);
                    lblCardRoute.Text = result.RouteName;
                    lblCardVehicle.Text = result.VehiclePlate;
                    lblCardDeparture.Text = result.DepartureTime.ToString("HH:mm dd/MM/yyyy");
                    lblCardCheckInTime.Text = result.CheckInTime.ToString("HH:mm:ss dd/MM/yyyy");

                    try { SystemSounds.Exclamation.Play(); } catch { }
                }
                else
                {
                    // Lỗi nghiêm trọng: Vé hủy / Chưa thanh toán / Không tồn tại - Màu đỏ nguy hiểm
                    pnlCardStatus.BackColor = Color.FromArgb(239, 68, 68);
                    lblCardStatusTitle.ForeColor = Color.White;
                    lblCardStatusTitle.Text = $"⛔ {result.Status switch { CheckInStatus.Cancelled => "VÉ ĐÃ BỊ HỦY", CheckInStatus.Unpaid => "VÉ CHƯA THANH TOÁN", CheckInStatus.NotFound => "KHÔNG TÌM THẤY VÉ", _ => "MÃ QR KHÔNG HỢP LỆ" }}";

                    lblCardTicketCode.Text = !string.IsNullOrEmpty(result.TicketCode) ? result.TicketCode : "—";
                    lblCardCustomerName.Text = !string.IsNullOrEmpty(result.CustomerName) ? result.CustomerName : "—";
                    lblCardCustomerPhone.Text = !string.IsNullOrEmpty(result.CustomerPhone) ? result.CustomerPhone : "—";
                    lblCardSeat.Text = !string.IsNullOrEmpty(result.SeatNumber) ? result.SeatNumber : "—";
                    lblCardSeat.ForeColor = Color.FromArgb(239, 68, 68);
                    lblCardRoute.Text = !string.IsNullOrEmpty(result.RouteName) ? result.RouteName : "—";
                    lblCardVehicle.Text = !string.IsNullOrEmpty(result.VehiclePlate) ? result.VehiclePlate : "—";
                    lblCardDeparture.Text = result.DepartureTime != default ? result.DepartureTime.ToString("HH:mm dd/MM/yyyy") : "—";
                    lblCardCheckInTime.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                    try { SystemSounds.Hand.Play(); } catch { }
                }
            }
        }

        private void ResetCardStatus()
        {
            pnlCardStatus.BackColor = Color.FromArgb(241, 245, 249);
            lblCardStatusTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblCardStatusTitle.Text = "CHỜ QUÉT MÃ QR...";

            lblCardTicketCode.Text = "—";
            lblCardCustomerName.Text = "—";
            lblCardCustomerPhone.Text = "—";
            lblCardSeat.Text = "—";
            lblCardSeat.ForeColor = Color.FromArgb(14, 165, 233);
            lblCardRoute.Text = "—";
            lblCardVehicle.Text = "—";
            lblCardDeparture.Text = "—";
            lblCardCheckInTime.Text = "—";
        }

        private async Task LoadCheckedInListAsync()
        {
            try
            {
                var list = await _checkInService.GetTodayCheckedInTicketsAsync();
                dgvCheckedIn.DataSource = list;
                lblCountBadge.Text = $"{list.Count} hành khách";
            }
            catch (Exception ex)
            {
                lblFooterStatus.Text = $"Lỗi tải danh sách: {ex.Message}";
            }
        }
    }
}
