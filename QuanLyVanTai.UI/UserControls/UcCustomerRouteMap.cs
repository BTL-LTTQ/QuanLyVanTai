using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using Core.Configs;
using Core.Helpers;
using Core.Logging;
using Microsoft.Web.WebView2.Core;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcCustomerRouteMap : UserControl
    {
        private readonly RouteMapService _routeMapService = new();
        private List<RouteListDto> _routes = new();
        private RouteMapDto? _currentRouteMap;
        private bool _isWebViewReady = false;
        private int? _targetRouteId;

        public event Action<string, bool>? OnStatusChanged;
        public event Action<int>? OnNavigateToBooking;

        public UcCustomerRouteMap() : this(null)
        {
        }

        public UcCustomerRouteMap(int? initialRouteId)
        {
            InitializeComponent();
            _targetRouteId = initialRouteId;

            // Chống crash Visual Studio Designer theo chuẩn LTTQ
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            SetupUi();
            WireEvents();
        }

        private void SetupUi()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.BackgroundColor;

            // Nút bấm theo chuẩn ThemeConfig
            ThemeConfig.StyleSuccessButton(btnDatVeTuyenNay, FontAwesome.Sharp.IconChar.Ticket);
            ThemeConfig.StyleSecondaryButton(btnFitBounds, FontAwesome.Sharp.IconChar.Expand);
            ThemeConfig.StyleSecondaryButton(btnMoGoogleMaps, FontAwesome.Sharp.IconChar.ArrowUpRightFromSquare);
            ThemeConfig.StyleSecondaryButton(btnLamMoi, FontAwesome.Sharp.IconChar.Rotate);

            StyleLayerButton(btnLayerStreet, true);
            StyleLayerButton(btnLayerSatellite, false);
            StyleLayerButton(btnLayerVoyager, false);
        }

        private void StyleLayerButton(FontAwesome.Sharp.IconButton btn, bool isActive)
        {
            if (isActive)
            {
                btn.BackColor = Color.FromArgb(238, 242, 255);
                btn.ForeColor = Color.FromArgb(67, 56, 202);
                btn.FlatAppearance.BorderColor = Color.FromArgb(199, 210, 254);
                btn.IconColor = Color.FromArgb(67, 56, 202);
            }
            else
            {
                btn.BackColor = Color.FromArgb(241, 245, 249);
                btn.ForeColor = Color.FromArgb(71, 85, 105);
                btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
                btn.IconColor = Color.FromArgb(71, 85, 105);
            }
        }

        private void WireEvents()
        {
            this.Load += async (s, e) =>
            {
                await InitializeWebViewAsync();
                await LoadRoutesAsync();
            };

            cboTuyenXe.SelectedIndexChanged += async (s, e) =>
            {
                if (cboTuyenXe.SelectedValue is int routeId)
                {
                    await LoadSelectedRouteMapAsync(routeId);
                }
            };

            btnLamMoi.Click += async (s, e) =>
            {
                await LoadRoutesAsync();
            };

            dgvStations.SelectionChanged += (s, e) =>
            {
                OnStationRowSelected();
            };

            btnFitBounds.Click += async (s, e) =>
            {
                if (_isWebViewReady)
                {
                    try
                    {
                        await webViewMap.ExecuteScriptAsync("fitRouteBounds();");
                        lblMapStatus.Text = "🔍 Đã căn chỉnh hiển thị toàn bộ lộ trình";
                    }
                    catch { }
                }
            };

            btnDatVeTuyenNay.Click += (s, e) =>
            {
                if (_currentRouteMap != null)
                {
                    OnNavigateToBooking?.Invoke(_currentRouteMap.RouteId);
                }
            };

            btnMoGoogleMaps.Click += (s, e) =>
            {
                OpenGoogleMapsInBrowser();
            };

            // Chuyển đổi lớp bản đồ
            btnLayerStreet.Click += async (s, e) =>
            {
                StyleLayerButton(btnLayerStreet, true);
                StyleLayerButton(btnLayerSatellite, false);
                StyleLayerButton(btnLayerVoyager, false);
                if (_isWebViewReady)
                {
                    await webViewMap.ExecuteScriptAsync("setMapLayer('street');");
                }
            };

            btnLayerSatellite.Click += async (s, e) =>
            {
                StyleLayerButton(btnLayerStreet, false);
                StyleLayerButton(btnLayerSatellite, true);
                StyleLayerButton(btnLayerVoyager, false);
                if (_isWebViewReady)
                {
                    await webViewMap.ExecuteScriptAsync("setMapLayer('satellite');");
                }
            };

            btnLayerVoyager.Click += async (s, e) =>
            {
                StyleLayerButton(btnLayerStreet, false);
                StyleLayerButton(btnLayerSatellite, false);
                StyleLayerButton(btnLayerVoyager, true);
                if (_isWebViewReady)
                {
                    await webViewMap.ExecuteScriptAsync("setMapLayer('street');");
                }
            };
        }

        private async Task InitializeWebViewAsync()
        {
            try
            {
                lblLoading.Visible = true;
                lblLoading.Text = "⏳ Đang khởi tạo môi trường Bản đồ số WebView2...";

                // Lưu UserData ở LocalApplicationData để tránh lỗi phân quyền thư mục
                string userDataFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "QuanLyVanTai",
                    "WebView2_UserData");

                Directory.CreateDirectory(userDataFolder);

                var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
                await webViewMap.EnsureCoreWebView2Async(env);

                webViewMap.CoreWebView2.Settings.IsStatusBarEnabled = false;
                webViewMap.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                webViewMap.CoreWebView2.Settings.IsZoomControlEnabled = true;

                // Lắng nghe thông điệp gửi ngược từ JavaScript trong WebView về WinForms
                webViewMap.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

                _isWebViewReady = true;
                lblLoading.Visible = false;
            }
            catch (Exception ex)
            {
                LoggerService.Error(ex, "Lỗi khi khởi tạo Microsoft WebView2");
                lblLoading.Visible = true;
                lblLoading.Text = "⚠️ Không thể tải WebView2. Vui lòng đảm bảo máy tính đã cài đặt Microsoft Edge WebView2 Runtime.";
            }
        }

        private void CoreWebView2_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                string rawJson = e.TryGetWebMessageAsString();
                if (string.IsNullOrWhiteSpace(rawJson)) return;

                using var doc = JsonDocument.Parse(rawJson);
                var root = doc.RootElement;

                if (root.TryGetProperty("type", out var typeProp) && typeProp.GetString() == "stationSelected")
                {
                    if (root.TryGetProperty("stationId", out var idProp))
                    {
                        int stId = idProp.GetInt32();
                        SelectStationInGrid(stId);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerService.Error(ex, "Lỗi phân tích WebMessage từ WebView2");
            }
        }

        private void SelectStationInGrid(int stationId)
        {
            foreach (DataGridViewRow row in dgvStations.Rows)
            {
                if (row.Tag is RouteStationMapPointDto point && point.StationId == stationId)
                {
                    row.Selected = true;
                    dgvStations.CurrentCell = row.Cells[1];
                    lblMapStatus.Text = $"📍 Đã chọn trạm: {point.StationName} ({point.City})";
                    break;
                }
            }
        }

        public async Task LoadRoutesAsync()
        {
            try
            {
                OnStatusChanged?.Invoke("Đang nạp danh sách tuyến xe...", true);
                _routes = await _routeMapService.GetActiveRoutesWithStationsAsync();

                cboTuyenXe.DataSource = null;
                cboTuyenXe.DisplayMember = "RouteName";
                cboTuyenXe.ValueMember = "Id";
                cboTuyenXe.DataSource = _routes;

                if (_routes.Any())
                {
                    if (_targetRouteId.HasValue && _routes.Any(r => r.Id == _targetRouteId.Value))
                    {
                        cboTuyenXe.SelectedValue = _targetRouteId.Value;
                    }
                    else
                    {
                        cboTuyenXe.SelectedIndex = 0;
                    }

                    if (cboTuyenXe.SelectedValue is int selectedId)
                    {
                        await LoadSelectedRouteMapAsync(selectedId);
                    }
                }
                else
                {
                    lblRouteNameVal.Text = "Chưa có dữ liệu tuyến xe";
                    dgvStations.Rows.Clear();
                }

                OnStatusChanged?.Invoke("Đã tải xong dữ liệu tuyến và bản đồ lộ trình.", false);
            }
            catch (Exception ex)
            {
                LoggerService.Error(ex, "Lỗi khi tải danh sách tuyến xe cho bản đồ");
                OnStatusChanged?.Invoke("Lỗi khi tải tuyến xe.", false);
                MessageBox.Show($"Không thể tải dữ liệu tuyến xe:\n{ex.Message}", "Lỗi Bản Đồ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task LoadSelectedRouteMapAsync(int routeId)
        {
            try
            {
                OnStatusChanged?.Invoke("Đang tải dữ liệu tọa độ lộ trình...", true);
                _currentRouteMap = await _routeMapService.GetRouteMapDataAsync(routeId);

                if (_currentRouteMap == null)
                {
                    lblRouteNameVal.Text = "Không tìm thấy thông tin tuyến";
                    dgvStations.Rows.Clear();
                    return;
                }

                // Cập nhật thông số lộ trình
                lblRouteNameVal.Text = $"{_currentRouteMap.RouteCode} - {_currentRouteMap.RouteName}";
                lblDistanceTimeVal.Text = $"{_currentRouteMap.DistanceKm:N0} km  (~{_currentRouteMap.EstimatedHours:N1} giờ)";
                lblBasePriceVal.Text = $"{_currentRouteMap.BasePrice:N0} VNĐ";
                lblStationCountVal.Text = $"{_currentRouteMap.Stations.Count} trạm đón/trả";

                // Đổ dữ liệu vào bảng trạm dừng
                dgvStations.Rows.Clear();
                foreach (var st in _currentRouteMap.Stations)
                {
                    string roleStr = st.IsDeparture ? "🟢 Điểm khởi hành" :
                                     st.IsDestination ? "🔴 Bến đến" : "🔵 Trạm dừng";

                    int rowIdx = dgvStations.Rows.Add(
                        st.StopOrder,
                        st.StationName,
                        st.City,
                        roleStr
                    );

                    dgvStations.Rows[rowIdx].Tag = st;

                    if (st.IsDeparture)
                    {
                        dgvStations.Rows[rowIdx].DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
                        dgvStations.Rows[rowIdx].DefaultCellStyle.ForeColor = Color.FromArgb(5, 150, 105);
                    }
                    else if (st.IsDestination)
                    {
                        dgvStations.Rows[rowIdx].DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
                        dgvStations.Rows[rowIdx].DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    }
                }

                // Hiển thị trên bản đồ tương tác WebView2
                if (_isWebViewReady)
                {
                    string html = _routeMapService.GenerateInteractiveMapHtml(_currentRouteMap);
                    webViewMap.NavigateToString(html);
                    lblMapStatus.Text = $"🗺️ Lộ trình: {_currentRouteMap.Stations.Count} trạm dừng. Click để xem chi tiết.";
                }

                OnStatusChanged?.Invoke($"Đang xem bản đồ tuyến {_currentRouteMap.RouteCode}", false);
            }
            catch (Exception ex)
            {
                LoggerService.Error(ex, "Lỗi khi nạp dữ liệu lộ trình lên bản đồ");
                OnStatusChanged?.Invoke("Lỗi nạp bản đồ lộ trình.", false);
            }
        }

        private async void OnStationRowSelected()
        {
            if (dgvStations.CurrentRow?.Tag is RouteStationMapPointDto point && _isWebViewReady)
            {
                try
                {
                    await webViewMap.ExecuteScriptAsync($"focusStation({point.StationId});");
                    lblMapStatus.Text = $"📍 Đã định vị: {point.StationName} ({point.Address}, {point.City})";
                }
                catch { }
            }
        }

        private void OpenGoogleMapsInBrowser()
        {
            if (_currentRouteMap == null || !_currentRouteMap.Stations.Any())
            {
                MessageBox.Show("Chưa có dữ liệu trạm dừng trên tuyến này để mở Google Maps.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string url = _routeMapService.GetGoogleMapsDirectionsUrl(_currentRouteMap);
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                LoggerService.Error(ex, "Lỗi khi mở Google Maps qua trình duyệt ngoài");
                MessageBox.Show($"Không thể mở liên kết trình duyệt:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
