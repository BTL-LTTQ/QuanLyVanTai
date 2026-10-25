using Core.DTOs;
using Microsoft.Extensions.Configuration;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Repositories;

namespace QuanLyVanTai.UI.UserControls
{
    /// <summary>
    /// UserControl "Báo cáo doanh thu (ADO.NET)" — minh họa truy vấn bằng
    /// <b>ADO.NET thuần</b> (SqlCommand / SqlDataReader / SqlDataAdapter)
    /// để đối chiếu với EF Core theo yêu cầu Chương 7.
    /// </summary>
    public partial class UcAdoReport : UserControl
    {
        private readonly IReportAdoService _reportService;
        private CancellationTokenSource _cts = new();

        // ─────────────────────────────────────────────────────────────────────
        //  Constructor — lấy connection string từ appsettings.json
        // ─────────────────────────────────────────────────────────────────────
        public UcAdoReport()
        {
            InitializeComponent();

            // Designer guard
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
                return;

            // Đọc connection string giống Program.cs
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            string connStr = config.GetConnectionString("DefaultConnection")
                ?? "Server=.;Database=QuanLyVanTaiDb;Trusted_Connection=True;TrustServerCertificate=True;";

            // Khởi tạo theo dependency chain: Repository → Service
            var repo    = new ReportAdoRepository(connStr);
            _reportService = new ReportAdoService(repo);

            // Preset: 12 tháng gần nhất
            dtpFrom.Value = DateTime.Today.AddMonths(-11).AddDays(1 - DateTime.Today.Day); // đầu tháng -11
            dtpTo.Value   = DateTime.Today;

            _ = LoadReportAsync();
        }

        // ─────────────────────────────────────────────────────────────────────
        //  Sự kiện bộ lọc
        // ─────────────────────────────────────────────────────────────────────
        private async void BtnLoad_Click(object sender, EventArgs e)
            => await LoadReportAsync();

        // ─────────────────────────────────────────────────────────────────────
        //  Nạp dữ liệu — async, có ProgressBar, không đóng băng UI
        // ─────────────────────────────────────────────────────────────────────
        private async Task LoadReportAsync()
        {
            // Hủy request cũ nếu đang chạy
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            var ct = _cts.Token;

            SetLoadingState(true);

            try
            {
                var from = dtpFrom.Value.Date;
                var to   = dtpTo.Value.Date;

                if (from > to)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.",
                        "Lỗi bộ lọc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi 3 phương thức ADO.NET song song
                var monthlyTask = _reportService.GetMonthlyRevenueAsync(from, to, ct);
                var routeTask   = _reportService.GetRevenueByRouteAsync(from, to, ct);
                var totalTask   = _reportService.GetTotalRevenueAsync(from, to, ct);

                await Task.WhenAll(monthlyTask, routeTask, totalTask);

                if (ct.IsCancellationRequested) return;

                var monthly = await monthlyTask;
                var routes  = await routeTask;
                decimal total = await totalTask;

                // ── Binding vào DataGridView ──────────────────────────────────────

                // Bảng 1: doanh thu theo tháng
                dgvMonthly.DataSource = monthly.Select(m => new
                {
                    m.MonthLabel,
                    DoanhThu  = m.Revenue.ToString("N0") + " ₫",
                    SoVe      = m.TicketCount,
                    TrungBinh = m.TicketCount > 0
                                ? (m.Revenue / m.TicketCount).ToString("N0") + " ₫"
                                : "—"
                }).ToList();

                // Bảng 2: doanh thu theo tuyến
                dgvRoutes.DataSource = routes.Select(r => new
                {
                    r.RouteCode,
                    r.RouteName,
                    DoanhThu = r.Revenue.ToString("N0") + " ₫",
                    SoVe     = r.TicketCount
                }).ToList();

                // Tổng
                lblTotal.Text = $"Tổng doanh thu (ADO.NET): {total:N0} ₫";

                // Ghi chú kỹ thuật
                lblNote.Text = $"✅  Truy vấn bằng SqlCommand + SqlDataReader (doanh thu tháng) "
                             + $"và SqlDataAdapter + DataTable (theo tuyến). "
                             + $"Khoảng: {from:dd/MM/yyyy} → {to:dd/MM/yyyy}. "
                             + $"Cập nhật lúc {DateTime.Now:HH:mm:ss}.";
            }
            catch (OperationCanceledException)
            {
                // Bị hủy — bình thường, bỏ qua
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn ADO.NET:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblNote.Text = $"❌ Lỗi: {ex.Message}";
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void SetLoadingState(bool loading)
        {
            progressBar.Visible = loading;
            btnLoad.Enabled     = !loading;
            dtpFrom.Enabled     = !loading;
            dtpTo.Enabled       = !loading;
        }
    }
}
