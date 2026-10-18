using Core.Configs;
using FontAwesome.Sharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcDashboard : UserControl
    {
        private readonly IDashboardService _dashboardService = new DashboardService();
        private bool _isGdiMode = false;
        private List<RevenueByDateDto> _dateRevenueData = new();
        private List<RevenueByRouteDto> _routeRevenueData = new();
        private System.Windows.Forms.Timer? _clockTimer;

        public UcDashboard()
        {
            InitializeComponent();

            // 🛡️ BẢO VỆ: Chặn Visual Studio Designer thực thi kết nối CSDL hoặc gọi Service
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || this.DesignMode)
            {
                return;
            }

            SetupRuntimeUi();
        }

        private void SetupRuntimeUi()
        {
            this.Font = ThemeConfig.MainFont;
            this.BackColor = Color.FromArgb(248, 250, 252);

            // Bật DoubleBuffered chống nhấp nháy cho các canvas GDI+
            EnableDoubleBuffering(pnlTimeChartGdi);
            EnableDoubleBuffering(pnlRouteChartGdi);

            // Đăng ký sự kiện vẽ GDI+
            pnlTimeChartGdi.Paint += PnlTimeChartGdi_Paint;
            pnlTimeChartGdi.Resize += (_, _) => pnlTimeChartGdi.Invalidate();

            pnlRouteChartGdi.Paint += PnlRouteChartGdi_Paint;
            pnlRouteChartGdi.Resize += (_, _) => pnlRouteChartGdi.Invalidate();

            // Cấu hình sự kiện bộ lọc
            cboDateFilter.SelectedIndex = 0; // "7 ngày gần nhất"
            cboDateFilter.SelectedIndexChanged += CboDateFilter_SelectedIndexChanged;
            btnApplyFilter.Click += async (_, _) => await LoadDashboardDataAsync();
            btnRefresh.Click += async (_, _) => await LoadDashboardDataAsync();
            btnToggleMode.Click += BtnToggleMode_Click;

            // Timer đồng hồ thời gian thực ở footer
            _clockTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _clockTimer.Tick += (_, _) => lblFooterTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            _clockTimer.Start();

            // Nạp dữ liệu lần đầu
            ApplySelectedDatePreset();
            _ = LoadDashboardDataAsync();
        }

        private static void EnableDoubleBuffering(Control control)
        {
            var prop = typeof(Control).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            prop?.SetValue(control, true, null);
        }

        private void CboDateFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplySelectedDatePreset();
        }

        private void ApplySelectedDatePreset()
        {
            var today = DateTime.Today;
            switch (cboDateFilter.SelectedIndex)
            {
                case 0: // 7 ngày gần nhất
                    dtpFromDate.Value = today.AddDays(-6);
                    dtpToDate.Value = today;
                    dtpFromDate.Enabled = false;
                    dtpToDate.Enabled = false;
                    break;
                case 1: // 30 ngày gần nhất
                    dtpFromDate.Value = today.AddDays(-29);
                    dtpToDate.Value = today;
                    dtpFromDate.Enabled = false;
                    dtpToDate.Enabled = false;
                    break;
                case 2: // Tháng này
                    dtpFromDate.Value = new DateTime(today.Year, today.Month, 1);
                    dtpToDate.Value = today;
                    dtpFromDate.Enabled = false;
                    dtpToDate.Enabled = false;
                    break;
                case 3: // Tùy chọn ngày
                    dtpFromDate.Enabled = true;
                    dtpToDate.Enabled = true;
                    break;
            }
        }

        private void BtnToggleMode_Click(object? sender, EventArgs e)
        {
            _isGdiMode = !_isGdiMode;
            if (_isGdiMode)
            {
                cartesianChartTime.Visible = false;
                pnlTimeChartGdi.Visible = true;

                pieChartRoutes.Visible = false;
                pnlRouteChartGdi.Visible = true;

                btnToggleMode.Text = " Chế độ: GDI+ Canvas (Trực tiếp)";
                btnToggleMode.IconChar = IconChar.PaintBrush;
                btnToggleMode.IconColor = Color.FromArgb(59, 130, 246);

                pnlTimeChartGdi.Invalidate();
                pnlRouteChartGdi.Invalidate();
            }
            else
            {
                pnlTimeChartGdi.Visible = false;
                cartesianChartTime.Visible = true;

                pnlRouteChartGdi.Visible = false;
                pieChartRoutes.Visible = true;

                btnToggleMode.Text = " Chế độ: LiveCharts2 (Hiện đại)";
                btnToggleMode.IconChar = IconChar.ChartLine;
                btnToggleMode.IconColor = Color.FromArgb(16, 185, 129);

                RenderLiveChartsTime(_dateRevenueData);
                RenderLiveChartsRoutes(_routeRevenueData);
            }
        }

        // =========================================================================
        //  DATA LOADING & BINDING (ASYNC - PHẢN ÁNH DỮ LIỆU THẬT CSDL)
        // =========================================================================
        public async Task LoadDashboardDataAsync()
        {
            try
            {
                lblFooterStatus.Text = "⏳ Đang truy vấn và tính toán thống kê giao dịch vé...";
                lblFooterStatus.ForeColor = Color.FromArgb(59, 130, 246);
                btnRefresh.Enabled = false;
                btnApplyFilter.Enabled = false;

                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date;

                // Tác vụ nền truy vấn đồng thời qua DashboardService (BLL)
                var summaryTask = _dashboardService.GetSummaryAsync(fromDate, toDate);
                var dateRevenueTask = _dashboardService.GetRevenueByDateAsync(fromDate, toDate);
                var routeRevenueTask = _dashboardService.GetRevenueByRouteAsync(fromDate, toDate);
                var recentTask = _dashboardService.GetRecentTransactionsAsync(15);

                await Task.WhenAll(summaryTask, dateRevenueTask, routeRevenueTask, recentTask);

                var summary = await summaryTask;
                _dateRevenueData = await dateRevenueTask;
                _routeRevenueData = await routeRevenueTask;
                var recentTransactions = await recentTask;

                // 1. Cập nhật Stat Cards
                lblStatRevenue.Text = $"{summary.TotalRevenue:N0} VNĐ";
                lblStatTickets.Text = $"{summary.TotalTickets:N0} vé";
                lblStatRoutes.Text = $"{summary.ActiveRoutes} tuyến";
                lblStatVehicles.Text = $"{summary.ActiveVehicles} xe";

                lblCardRevenueSub.Text = $"Hôm nay: {summary.TodayRevenue:N0} VNĐ ({summary.TodayTickets} vé)";

                // 2. Cập nhật Biểu đồ (LiveCharts2 hoặc GDI+)
                if (!_isGdiMode)
                {
                    RenderLiveChartsTime(_dateRevenueData);
                    RenderLiveChartsRoutes(_routeRevenueData);
                }
                else
                {
                    pnlTimeChartGdi.Invalidate();
                    pnlRouteChartGdi.Invalidate();
                }

                // 3. Cập nhật Bảng giao dịch vé gần đây
                dgvRecentTransactions.DataSource = recentTransactions;

                // 4. Cập nhật Footer
                lblFooterStatus.Text = "● Đã đồng bộ dữ liệu giao dịch vé thực tế từ CSDL";
                lblFooterStatus.ForeColor = Color.FromArgb(16, 185, 129);
                lblFooterRecords.Text = $"Tổng ghi nhận: {summary.TotalTickets:N0} vé bán thành công | Giá vé TB: {summary.AverageTicketPrice:N0} VNĐ";
                lblFooterTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch (Exception ex)
            {
                lblFooterStatus.Text = $"⚠️ Lỗi nạp số liệu: {ex.Message}";
                lblFooterStatus.ForeColor = Color.FromArgb(239, 68, 68);
            }
            finally
            {
                btnRefresh.Enabled = true;
                btnApplyFilter.Enabled = true;
            }
        }

        // =========================================================================
        //  LIVECHARTS2 RENDERING (BIỂU ĐỒ HIỆN ĐẠI ANIMATION & HOVER TOOLTIP)
        // =========================================================================
        private void RenderLiveChartsTime(List<RevenueByDateDto> data)
        {
            if (data == null || data.Count == 0)
            {
                cartesianChartTime.Series = Array.Empty<ISeries>();
                return;
            }

            var revenues = data.Select(d => (double)d.Revenue).ToArray();
            var ticketCounts = data.Select(d => (double)d.TicketCount).ToArray();
            var labels = data.Select(d => d.DateLabel).ToArray();

            cartesianChartTime.Series = new ISeries[]
            {
                new ColumnSeries<double>
                {
                    Name = "Doanh thu (VNĐ)",
                    Values = revenues,
                    Fill = new SolidColorPaint(new SKColor(16, 185, 129)), // Emerald
                    Stroke = new SolidColorPaint(new SKColor(5, 150, 105), 1.5f),
                    MaxBarWidth = 36,
                    Rx = 4,
                    Ry = 4,
                    DataLabelsPaint = new SolidColorPaint(new SKColor(30, 41, 59)),
                    DataLabelsSize = 11,
                    DataLabelsFormatter = p => p.Model >= 1_000_000
                        ? $"{p.Model / 1_000_000:0.##} tr"
                        : (p.Model > 0 ? $"{p.Model / 1_000:0} k" : "0"),
                    ScalesYAt = 0
                },
                new LineSeries<double>
                {
                    Name = "Số lượng vé",
                    Values = ticketCounts,
                    Stroke = new SolidColorPaint(new SKColor(59, 130, 246), 2.5f), // Blue
                    GeometryStroke = new SolidColorPaint(new SKColor(59, 130, 246), 2f),
                    GeometryFill = new SolidColorPaint(SKColors.White),
                    GeometrySize = 7,
                    Fill = null,
                    DataLabelsPaint = new SolidColorPaint(new SKColor(59, 130, 246)),
                    DataLabelsSize = 11,
                    DataLabelsFormatter = p => p.Model > 0 ? $"{p.Model} vé" : "",
                    ScalesYAt = 1
                }
            };

            cartesianChartTime.XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = labels,
                    LabelsRotation = 15,
                    TextSize = 11,
                    LabelsPaint = new SolidColorPaint(new SKColor(100, 116, 139))
                }
            };

            cartesianChartTime.YAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Doanh thu (VNĐ)",
                    NamePaint = new SolidColorPaint(new SKColor(16, 185, 129)),
                    NameTextSize = 11,
                    Labeler = value => value >= 1_000_000 ? $"{value / 1_000_000:0.#} tr" : $"{value:N0}",
                    LabelsPaint = new SolidColorPaint(new SKColor(100, 116, 139)),
                    TextSize = 11,
                    MinLimit = 0
                },
                new Axis
                {
                    Name = "Số vé",
                    NamePaint = new SolidColorPaint(new SKColor(59, 130, 246)),
                    NameTextSize = 11,
                    Labeler = value => $"{value:N0}",
                    LabelsPaint = new SolidColorPaint(new SKColor(59, 130, 246)),
                    TextSize = 11,
                    Position = LiveChartsCore.Measure.AxisPosition.End,
                    ShowSeparatorLines = false,
                    MinLimit = 0
                }
            };

            cartesianChartTime.LegendPosition = LiveChartsCore.Measure.LegendPosition.Top;
            cartesianChartTime.TooltipPosition = LiveChartsCore.Measure.TooltipPosition.Top;
        }

        private void RenderLiveChartsRoutes(List<RevenueByRouteDto> data)
        {
            if (data == null || data.Count == 0)
            {
                pieChartRoutes.Series = Array.Empty<ISeries>();
                return;
            }

            var palette = new SKColor[]
            {
                new(59, 130, 246),   // Blue
                new(16, 185, 129),   // Emerald
                new(245, 158, 11),   // Amber
                new(139, 92, 246),   // Purple
                new(239, 68, 68),    // Red
                new(14, 165, 233),   // Sky
                new(236, 72, 153),   // Pink
            };

            var seriesList = new List<ISeries>();
            for (int i = 0; i < data.Count; i++)
            {
                var item = data[i];
                var color = palette[i % palette.Length];
                seriesList.Add(new PieSeries<double>
                {
                    Name = item.RouteName,
                    Values = new double[] { (double)item.Revenue },
                    Fill = new SolidColorPaint(color),
                    Stroke = new SolidColorPaint(SKColors.White, 2),
                    DataLabelsPaint = new SolidColorPaint(SKColors.White),
                    DataLabelsSize = 11,
                    DataLabelsFormatter = p => item.Percentage > 5 ? $"{item.Percentage}%" : "",
                    ToolTipLabelFormatter = point => $"{item.RouteCode} ({item.RouteName}): {point.Model:N0} VNĐ ({item.Percentage}%)"
                });
            }

            pieChartRoutes.Series = seriesList.ToArray();
            pieChartRoutes.InitialRotation = 45;
            pieChartRoutes.LegendPosition = LiveChartsCore.Measure.LegendPosition.Bottom;
            pieChartRoutes.TooltipPosition = LiveChartsCore.Measure.TooltipPosition.Top;
        }

        // =========================================================================
        //  GDI+ RENDERING (DOUBLE BUFFERED CANVAS TỰ VẼ - CHỐNG RUNG GIẬT FLICKER)
        // =========================================================================
        private void PnlTimeChartGdi_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Panel canvas || _dateRevenueData == null || _dateRevenueData.Count == 0) return;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int W = canvas.Width;
            int H = canvas.Height;
            const int PL = 55, PR = 40, PT = 24, PB = 35;
            int cw = W - PL - PR;
            int ch = H - PT - PB;

            if (cw <= 10 || ch <= 10) return;

            decimal maxRev = _dateRevenueData.Max(d => d.Revenue);
            if (maxRev <= 0) maxRev = 1_000_000m;

            int count = _dateRevenueData.Count;
            float step = cw / (float)Math.Max(1, count);
            float barWidth = Math.Min(34f, step * 0.6f);

            // 1. Grid ngang & nhãn Y
            using (var gridPen = new Pen(Color.FromArgb(241, 245, 249), 1))
            using (var axisFont = new Font("Segoe UI", 7.8F))
            using (var labelBrush = new SolidBrush(Color.FromArgb(148, 163, 184)))
            {
                for (int i = 0; i <= 4; i++)
                {
                    int gy = PT + ch * i / 4;
                    g.DrawLine(gridPen, PL, gy, W - PR, gy);

                    decimal val = maxRev * (4 - i) / 4m;
                    string yLabel = val >= 1_000_000m ? $"{val / 1_000_000m:0.#}tr" : $"{val:N0}";
                    g.DrawString(yLabel, axisFont, labelBrush, 6, gy - 7);
                }
            }

            // 2. Cột Doanh Thu (Bars)
            for (int i = 0; i < count; i++)
            {
                var d = _dateRevenueData[i];
                float cx = PL + step * i + step / 2f;
                float barHeight = (float)(d.Revenue / maxRev) * ch;
                float bx = cx - barWidth / 2f;
                float by = PT + ch - barHeight;

                if (barHeight > 0)
                {
                    var rect = new RectangleF(bx, by, barWidth, barHeight);
                    using var br = new LinearGradientBrush(rect, Color.FromArgb(16, 185, 129), Color.FromArgb(5, 150, 105), LinearGradientMode.Vertical);
                    g.FillRectangle(br, rect);

                    // Nhãn giá trị trên đầu cột
                    using var valFont = new Font("Segoe UI", 7.2F, FontStyle.Bold);
                    using var valBrush = new SolidBrush(Color.FromArgb(30, 41, 59));
                    string topLabel = d.Revenue >= 1_000_000m ? $"{d.Revenue / 1_000_000m:0.#}tr" : (d.Revenue > 0 ? $"{d.Revenue / 1_000m:0}k" : "0");
                    var sz = g.MeasureString(topLabel, valFont);
                    g.DrawString(topLabel, valFont, valBrush, cx - sz.Width / 2f, by - sz.Height - 1);
                }

                // Nhãn X (Ngày)
                using var xFont = new Font("Segoe UI", 7.8F);
                using var xBrush = new SolidBrush(Color.FromArgb(100, 116, 139));
                var xsz = g.MeasureString(d.DateLabel, xFont);
                g.DrawString(d.DateLabel, xFont, xBrush, cx - xsz.Width / 2f, H - PB + 6);
            }

            // 3. Đường Xu Hướng Số Lượng Vé (Line overlay)
            int maxTickets = _dateRevenueData.Max(d => d.TicketCount);
            if (maxTickets <= 0) maxTickets = 1;

            var points = new List<PointF>();
            for (int i = 0; i < count; i++)
            {
                var d = _dateRevenueData[i];
                float cx = PL + step * i + step / 2f;
                float py = PT + ch - ((float)d.TicketCount / maxTickets) * ch * 0.85f;
                points.Add(new PointF(cx, py));
            }

            if (points.Count > 1)
            {
                using var linePen = new Pen(Color.FromArgb(59, 130, 246), 2.5f);
                g.DrawLines(linePen, points.ToArray());

                using var nodeFill = new SolidBrush(Color.White);
                using var nodeBorder = new Pen(Color.FromArgb(59, 130, 246), 2f);
                foreach (var pt in points)
                {
                    g.FillEllipse(nodeFill, pt.X - 3.5f, pt.Y - 3.5f, 7, 7);
                    g.DrawEllipse(nodeBorder, pt.X - 3.5f, pt.Y - 3.5f, 7, 7);
                }
            }
        }

        private void PnlRouteChartGdi_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Panel canvas || _routeRevenueData == null || _routeRevenueData.Count == 0) return;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int W = canvas.Width;
            int H = canvas.Height;

            var colors = new Color[]
            {
                Color.FromArgb(59, 130, 246),   // Blue
                Color.FromArgb(16, 185, 129),   // Emerald
                Color.FromArgb(245, 158, 11),   // Amber
                Color.FromArgb(139, 92, 246),   // Purple
                Color.FromArgb(239, 68, 68),    // Red
                Color.FromArgb(14, 165, 233),   // Sky
            };

            int diameter = Math.Min((int)(W * 0.48), (int)(H * 0.78));
            if (diameter <= 30) return;

            int pieX = 20;
            int pieY = (H - diameter) / 2;
            var pieRect = new Rectangle(pieX, pieY, diameter, diameter);

            decimal total = _routeRevenueData.Sum(r => r.Revenue);
            float startAngle = 0;

            for (int i = 0; i < _routeRevenueData.Count; i++)
            {
                var r = _routeRevenueData[i];
                float sweepAngle = total > 0 ? (float)(r.Revenue / total) * 360f : 360f / _routeRevenueData.Count;
                var col = colors[i % colors.Length];

                using (var br = new SolidBrush(col))
                {
                    g.FillPie(br, pieRect, startAngle, sweepAngle);
                }
                using (var pen = new Pen(Color.White, 2))
                {
                    g.DrawPie(pen, pieRect, startAngle, sweepAngle);
                }

                startAngle += sweepAngle;
            }

            // Khoét lỗ tâm tạo hiệu ứng Donut Chart
            int holeDiameter = (int)(diameter * 0.52);
            int holeX = pieX + (diameter - holeDiameter) / 2;
            int holeY = pieY + (diameter - holeDiameter) / 2;
            using (var bgBr = new SolidBrush(Color.White))
            {
                g.FillEllipse(bgBr, holeX, holeY, holeDiameter, holeDiameter);
            }

            // Text tổng doanh thu ở tâm Donut
            using (var tf1 = new Font("Segoe UI", 7.5F, FontStyle.Bold))
            using (var tf2 = new Font("Segoe UI", 8.8F, FontStyle.Bold))
            using (var tb1 = new SolidBrush(Color.FromArgb(100, 116, 139)))
            using (var tb2 = new SolidBrush(Color.FromArgb(15, 23, 42)))
            {
                var s1 = "TỔNG CỘNG";
                string s2 = total >= 1_000_000m ? $"{total / 1_000_000m:0.#} tr" : $"{total:N0}";
                var sz1 = g.MeasureString(s1, tf1);
                var sz2 = g.MeasureString(s2, tf2);

                g.DrawString(s1, tf1, tb1, holeX + (holeDiameter - sz1.Width) / 2f, holeY + holeDiameter / 2f - sz1.Height);
                g.DrawString(s2, tf2, tb2, holeX + (holeDiameter - sz2.Width) / 2f, holeY + holeDiameter / 2f + 1);
            }

            // Legend bên phải
            int legX = pieX + diameter + 20;
            int legY = 16;
            using var nameFont = new Font("Segoe UI", 8.2F, FontStyle.Bold);
            using var subFont = new Font("Segoe UI", 7.8F);
            using var nameBrush = new SolidBrush(Color.FromArgb(30, 41, 59));
            using var subBrush = new SolidBrush(Color.FromArgb(100, 116, 139));

            for (int i = 0; i < Math.Min(6, _routeRevenueData.Count); i++)
            {
                var r = _routeRevenueData[i];
                var col = colors[i % colors.Length];

                using (var sb = new SolidBrush(col))
                {
                    g.FillEllipse(sb, legX, legY + 3, 10, 10);
                }

                string line1 = $"{r.RouteCode} ({r.Percentage:0.#}%)";
                string line2 = $"{r.Revenue:N0} VNĐ ({r.TicketCount} vé)";

                g.DrawString(line1, nameFont, nameBrush, legX + 16, legY);
                g.DrawString(line2, subFont, subBrush, legX + 16, legY + 16);

                legY += 36;
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            _clockTimer?.Stop();
            _clockTimer?.Dispose();
            base.OnHandleDestroyed(e);
        }
    }
}
