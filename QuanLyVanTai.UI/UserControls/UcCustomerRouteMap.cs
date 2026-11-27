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
        private bool _suppressRouteSelectionChange;
        private bool _suppressStationSelectionChange;
        private int _routeLoadVersion;

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
            StyleLayerButton(btnLayerTopo, false);
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
                if (_suppressRouteSelectionChange) return;
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
                if (_suppressStationSelectionChange) return;
                OnStationRowSelected();
            };

            btnFitBounds.Click += async (s, e) =>
            {
                await RunMapScriptAsync("fitRouteBounds();");
                lblMapStatus.ForeColor = Color.FromArgb(100, 116, 139);
                lblMapStatus.Text = UiTranslator.Translate("Đã căn chỉnh hiển thị toàn bộ lộ trình");
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
            btnLayerStreet.Click += async (s, e) => await SwitchLayerAsync(btnLayerStreet, "street");
            btnLayerSatellite.Click += async (s, e) => await SwitchLayerAsync(btnLayerSatellite, "satellite");
            btnLayerTopo.Click += async (s, e) => await SwitchLayerAsync(btnLayerTopo, "topo");
        }

        private async Task SwitchLayerAsync(FontAwesome.Sharp.IconButton activeButton, string layerName)
        {
            StyleLayerButton(btnLayerStreet, activeButton == btnLayerStreet);
            StyleLayerButton(btnLayerSatellite, activeButton == btnLayerSatellite);
            StyleLayerButton(btnLayerTopo, activeButton == btnLayerTopo);
            await RunMapScriptAsync($"setMapLayer('{layerName}');");
        }

        private async Task RunMapScriptAsync(string script)
        {
            if (!_isWebViewReady) return;

            try
            {
                await webViewMap.ExecuteScriptAsync(script);
            }
            catch (Exception ex)
            {
                LoggerService.Error(ex, "Lỗi thực thi JavaScript trên bản đồ");
            }
        }

        private async Task InitializeWebViewAsync()
        {
            try
            {
                lblLoading.Visible = true;
                lblLoading.Text = UiTranslator.Translate("Đang khởi tạo môi trường Bản đồ số WebView2...");

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
                lblLoading.Text = UiTranslator.Translate("Không thể tải WebView2. Vui lòng đảm bảo máy tính đã cài đặt Microsoft Edge WebView2 Runtime.");
            }
        }

        private void CoreWebView2_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                // JS gửi object JSON; TryGetWebMessageAsString() sẽ ném lỗi với message dạng JSON
                string rawJson = e.WebMessageAsJson;
                if (string.IsNullOrWhiteSpace(rawJson)) return;

                using var doc = JsonDocument.Parse(rawJson);
                var root = doc.RootElement;

                if (!root.TryGetProperty("type", out var typeProp)) return;

                switch (typeProp.GetString())
                {
                    case "stationSelected" when root.TryGetProperty("stationId", out var idProp):
                        SelectStationInGrid(idProp.GetInt32());
                        break;

                    case "mapError":
                        string detail = root.TryGetProperty("message", out var msgProp)
                            ? msgProp.GetString() ?? string.Empty
                            : string.Empty;
                        lblMapStatus.ForeColor = Color.FromArgb(220, 38, 38);
                        lblMapStatus.Text = string.IsNullOrWhiteSpace(detail)
                            ? UiTranslator.Translate("Không tải được bản đồ. Vui lòng kiểm tra kết nối Internet.")
                            : detail;
                        break;
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
                    // Chặn SelectionChanged tạm thời: đổi Selected rồi CurrentCell sẽ bắn 2 sự kiện
                    // và lần ghi sau có thể lấy nhầm CurrentRow cũ (bản đồ nhảy về trạm đầu).
                    _suppressStationSelectionChange = true;
                    try
                    {
                        row.Selected = true;
                        dgvStations.CurrentCell = row.Cells[1];
                    }
                    finally
                    {
                        _suppressStationSelectionChange = false;
                    }

                    FocusStationOnMap(point);
                    break;
                }
            }
        }

        private async void FocusStationOnMap(RouteStationMapPointDto point)
        {
            if (!_isWebViewReady) return;

            await RunMapScriptAsync($"focusStation({point.StationId});");
            lblMapStatus.ForeColor = Color.FromArgb(100, 116, 139);
            lblMapStatus.Text = string.Format(UiTranslator.Translate("Đã định vị: {0} ({1}, {2})"), point.StationName, point.Address, point.City);
        }

        public async Task LoadRoutesAsync()
        {
            try
            {
                OnStatusChanged?.Invoke("Đang nạp danh sách tuyến xe...", true);
                _routes = await _routeMapService.GetActiveRoutesWithStationsAsync();

                // Ưu tiên tuyến được yêu cầu mở, sau đó giữ tuyến đang chọn khi làm mới
                int? preferredId = _targetRouteId ?? cboTuyenXe.SelectedValue as int?;
                _targetRouteId = null;

                _suppressRouteSelectionChange = true;
                try
                {
                    cboTuyenXe.DataSource = null;
                    cboTuyenXe.DisplayMember = "RouteName";
                    cboTuyenXe.ValueMember = "Id";
                    cboTuyenXe.DataSource = _routes;

                    if (_routes.Any())
                    {
                        cboTuyenXe.SelectedValue = preferredId.HasValue && _routes.Any(r => r.Id == preferredId.Value)
                            ? preferredId.Value
                            : _routes[0].Id;
                    }
                }
                finally
                {
                    _suppressRouteSelectionChange = false;
                }

                if (cboTuyenXe.SelectedValue is int selectedId)
                {
                    await LoadSelectedRouteMapAsync(selectedId);
                }
                else
                {
                    lblRouteNameVal.Text = UiTranslator.Translate("Chưa có dữ liệu tuyến xe");
                    dgvStations.Rows.Clear();
                }

                OnStatusChanged?.Invoke(UiTranslator.Translate("Đã tải xong dữ liệu tuyến và bản đồ lộ trình."), false);
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
                int loadVersion = ++_routeLoadVersion;
                OnStatusChanged?.Invoke("Đang tải dữ liệu tọa độ lộ trình...", true);
                var routeMap = await _routeMapService.GetRouteMapDataAsync(routeId);

                // Người dùng đã chọn tuyến khác trong lúc chờ dữ liệu
                if (loadVersion != _routeLoadVersion) return;

                _currentRouteMap = routeMap;

                if (_currentRouteMap == null)
                {
                    lblRouteNameVal.Text = UiTranslator.Translate("Không tìm thấy thông tin tuyến");
                    dgvStations.Rows.Clear();
                    return;
                }

                // Cập nhật thông số lộ trình
                lblRouteNameVal.Text = $"{_currentRouteMap.RouteCode} - {_currentRouteMap.RouteName}";
                lblDistanceTimeVal.Text = string.Format(UiTranslator.Translate("{0:N0} km  (~{1:N1} giờ)"), _currentRouteMap.DistanceKm, _currentRouteMap.EstimatedHours);
                lblBasePriceVal.Text = string.Format(UiTranslator.Translate("{0:N0} VNĐ"), _currentRouteMap.BasePrice);
                lblStationCountVal.Text = _currentRouteMap.MissingCoordinateCount > 0
                    ? string.Format(UiTranslator.Translate("{0} trạm đón/trả ({1} trạm chưa có GPS)"), _currentRouteMap.Stations.Count, _currentRouteMap.MissingCoordinateCount)
                    : string.Format(UiTranslator.Translate("{0} trạm đón/trả"), _currentRouteMap.Stations.Count);

                // Đổ dữ liệu vào bảng trạm dừng
                dgvStations.Rows.Clear();
                foreach (var st in _currentRouteMap.Stations)
                {
                    string roleStr = st.IsDeparture ? UiTranslator.Translate("Điểm khởi hành") :
                                     st.IsDestination ? UiTranslator.Translate("Bến đến") : UiTranslator.Translate("Trạm dừng");

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

                    lblMapStatus.ForeColor = Color.FromArgb(100, 116, 139);
                    lblMapStatus.Text = _currentRouteMap.MissingCoordinateCount > 0
                        ? string.Format(UiTranslator.Translate("Lộ trình: {0} trạm dừng. {1} trạm chưa có GPS nên không hiển thị."), _currentRouteMap.Stations.Count, _currentRouteMap.MissingCoordinateCount)
                        : string.Format(UiTranslator.Translate("Lộ trình: {0} trạm dừng. Click vào marker để xem chi tiết."), _currentRouteMap.Stations.Count);
                }

                OnStatusChanged?.Invoke(string.Format(UiTranslator.Translate("Đang xem bản đồ tuyến {0}"), _currentRouteMap.RouteCode), false);
            }
            catch (Exception ex)
            {
                LoggerService.Error(ex, "Lỗi khi nạp dữ liệu lộ trình lên bản đồ");
                OnStatusChanged?.Invoke("Lỗi nạp bản đồ lộ trình.", false);
            }
        }

        private void OnStationRowSelected()
        {
            if (dgvStations.CurrentRow?.Tag is RouteStationMapPointDto point)
            {
                FocusStationOnMap(point);
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
