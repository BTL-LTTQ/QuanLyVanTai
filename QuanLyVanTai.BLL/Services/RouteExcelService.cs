using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    // =========================================================================
    // RouteExcelService — Import / Export Tuyến xe bằng file Excel (.xlsx)
    // =========================================================================
    // Trách nhiệm:
    //   1. Export toàn bộ tuyến xe hiện có ra file .xlsx bằng ClosedXML.
    //   2. Import dữ liệu tuyến xe từ file .xlsx với các ràng buộc chỉ nhập
    //      các trường cốt lõi của Route (mã, tên, cự ly, thời gian, giá, trạng thái).
    //   3. Chống đơ UI: các thao tác đọc/ghi file nặng được bọc trong Task.Run,
    //      và cập nhật tiến độ qua IProgress<int> (0..100) cho ProgressBar.
    //   4. Import an toàn: đọc Excel + lưu DbContext trong MỘT Database Transaction.
    //      Nếu chỉ cần 1 dòng lỗi (sai định dạng, thiếu cột, duplicate,...) thì
    //      Rollback TOÀN BỘ và ném ExcelImportException kèm danh sách dòng lỗi.
    // =========================================================================

    public class RouteExcelService
    {
        // Tiêu đề cột chuẩn khi Export; Import sẽ đối chiếu theo thứ tự này.
        private static readonly string[] ExcelColumns =
        {
            "Mã Tuyến", "Tên Lộ Trình", "Cự Ly (km)",
            "Thời Gian (giờ)", "Giá Cước (VNĐ)", "Trạng Thái"
        };

        private static readonly string[] ValidStatuses = { "Active", "Suspended" };

        // ── EXPORT ────────────────────────────────────────────────────────────

        /// <summary>
        /// Export danh sách tuyến xe ra file Excel (.xlsx) theo đường dẫn cho trước.
        /// Thao tác ghi file nặng chạy trong Task.Run để UI không bị đơ.
        /// </summary>
        /// <param name="filePath">Đường dẫn file đích (.xlsx).</param>
        /// <param name="progress">Báo tiến độ 0..100 cho ProgressBar ở UI.</param>
        /// <param name="cancellationToken">Cho phép hủy giữa chừng.</param>
        /// <returns>Số dòng đã ghi (không tính header).</returns>
        public async Task<int> ExportRoutesAsync(
            string filePath,
            IProgress<int>? progress = null,
            CancellationToken cancellationToken = default)
        {
            // ── Đọc dữ liệu (async EF) trên luồng hiện tại ────────────────────
            var routes = await LoadRoutesAsync(cancellationToken);

            if (routes.Count == 0)
                throw new BusinessRuleViolationException(
                    "Route", "Không có tuyến xe nào để xuất Excel.");

            // ── Snapshot dữ liệu để ghi file trong Task.Run ───────────────────
            var rows = routes
                .Select(r => new object?[]
                {
                    r.RouteCode,
                    r.RouteName,
                    r.DistanceKm,
                    r.EstimatedHours,
                    r.BasePrice,
                    TranslateStatus(r.Status)
                })
                .ToList();

            progress?.Report(20);

            // ── Ghi file nặng trên Background Thread ──────────────────────────
            var total = rows.Count;
            await Task.Run(() =>
            {
                using var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("DanhSachTuyenXe");

                // Header
                for (int c = 0; c < ExcelColumns.Length; c++)
                {
                    var cell = ws.Cell(1, c + 1);
                    cell.Value = ExcelColumns[c];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#2563EB");
                    cell.Style.Font.FontColor = XLColor.White;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                }
                ws.Row(1).Height = 24;

                // Data + báo tiến độ theo phần trăm dòng đã ghi
                for (int i = 0; i < rows.Count; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var values = rows[i];
                    for (int c = 0; c < values.Length; c++)
                        SetCellValue(ws.Cell(i + 2, c + 1), values[c]);

                    // 20% (sau header) → 100%
                    int pct = 20 + (int)((double)(i + 1) / total * 80);
                    progress?.Report(pct);
                }

                ws.Columns(1, ExcelColumns.Length).AdjustToContents();

                wb.SaveAs(filePath);
            }, cancellationToken);

            progress?.Report(100);
            return total;
        }

        // ── IMPORT ────────────────────────────────────────────────────────────

        /// <summary>
        /// Import tuyến xe từ file Excel (.xlsx).
        /// Toàn bộ quá trình đọc + lưu được bọc trong MỘT transaction;
        /// nếu bất kỳ dòng nào lỗi → Rollback toàn bộ và ném ExcelImportException.
        /// Các tuyến có mã trùng với dữ liệu ĐANG CÓ trong DB sẽ bị bỏ qua
        /// (đếm vào SkippedDuplicateRows), không tính là lỗi.
        /// </summary>
        /// <param name="filePath">Đường dẫn file Excel nguồn.</param>
        /// <param name="progress">Báo tiến độ 0..100 cho ProgressBar ở UI.</param>
        /// <param name="cancellationToken">Cho phép hủy giữa chừng.</param>
        /// <exception cref="ExcelImportException">
        /// Nếu file không đúng template hoặc có từ 1 dòng lỗi trở lên (đã Rollback).
        /// </exception>
        public async Task<RouteImportResultDto> ImportRoutesAsync(
            string filePath,
            IProgress<int>? progress = null,
            CancellationToken cancellationToken = default)
        {
            // ── Đọc toàn bộ file trên Background Thread trước khi mở transaction ─
            //    (Tránh giữ database connection lâu khi đọc file lớn.)
            var (parsed, errors) = await Task.Run(() => ParseExcelFile(filePath, cancellationToken));

            if (errors.Count > 0)
                throw new ExcelImportException("Route", errors);

            progress?.Report(20);

            var result = new RouteImportResultDto { TotalRows = parsed.Count };

            if (parsed.Count == 0)
            {
                progress?.Report(100);
                return result;
            }

            // ── Lưu vào DB trong transaction ───────────────────────────────────
            try
            {
                using var db = new AppDbContext();
                using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    // Lấy tập mã tuyến đang tồn tại để bỏ qua dòng trùng (không lỗi)
                    var existingCodes = (await db.Routes
                            .AsNoTracking()
                            .Where(r => parsed.Select(p => p.RouteCode).Contains(r.RouteCode))
                            .Select(r => r.RouteCode)
                            .ToListAsync(cancellationToken))
                        .ToHashSet(StringComparer.OrdinalIgnoreCase);

                    var inserted = new List<Route>();
                    int index = 0;
                    foreach (var row in parsed)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        string code = row.RouteCode.Trim().ToUpperInvariant();
                        if (existingCodes.Contains(code))
                        {
                            result.SkippedDuplicateRows++;
                            continue;
                        }

                        inserted.Add(new Route
                        {
                            RouteCode      = code,
                            RouteName      = row.RouteName.Trim(),
                            DistanceKm     = row.DistanceKm,
                            EstimatedHours = row.EstimatedHours,
                            BasePrice      = row.BasePrice,
                            Status         = row.Status == "Suspended" ? "Suspended" : "Active"
                        });

                        // 20% (sau đọc file) → 90%
                        index++;
                        progress?.Report(20 + (int)((double)index / parsed.Count * 70));
                    }

                    if (inserted.Count > 0)
                    {
                        db.Routes.AddRange(inserted);
                        await db.SaveChangesAsync(cancellationToken);
                    }

                    await transaction.CommitAsync(cancellationToken);
                    result.SuccessRows = inserted.Count;

                    progress?.Report(100);
                    return result;
                }
                catch
                {
                    // Bất kỳ lỗi nào → Rollback toàn bộ dữ liệu đã đọc
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
            catch (ExcelImportException)
            {
                throw;
            }
            catch (BusStationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Route", "ImportRoutes", ex);
            }
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static async Task<List<Route>> LoadRoutesAsync(CancellationToken ct)
        {
            try
            {
                using var db = new AppDbContext();
                return await db.Routes
                    .AsNoTracking()
                    .OrderBy(r => r.RouteCode)
                    .ToListAsync(ct);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Route", "ExportRoutes", ex);
            }
        }

        private static string TranslateStatus(string status)
            => status == "Active" ? "Đang hoạt động" : "Tạm ngừng";

        /// <summary>
        /// Ghi giá trị vào ô Excel theo kiểu động của giá trị nguồn
        /// để giữ đúng kiểu dữ liệu (số giữ là số, chuỗi giữ là chuỗi).
        /// </summary>
        private static void SetCellValue(IXLCell cell, object? value)
        {
            switch (value)
            {
                case decimal d: cell.SetValue(d); break;
                case double dbl: cell.SetValue(dbl); break;
                case int i: cell.SetValue(i); break;
                case bool b: cell.SetValue(b); break;
                case DateTime dt: cell.SetValue(dt); break;
                case string s: cell.SetValue(s); break;
                default: cell.SetValue(value?.ToString() ?? string.Empty); break;
            }
        }

        /// <summary>
        /// Đọc file Excel và map từng dòng thành RouteExcelRowDto.
        /// Trả về danh sách lỗi chi tiết (số dòng + lý do). Không giữ DB mở lúc đọc.
        /// </summary>
        private static (List<RouteExcelRowDto> Rows, List<string> Errors) ParseExcelFile(
            string filePath, CancellationToken ct)
        {
            var rows = new List<RouteExcelRowDto>();
            var errors = new List<string>();

            using var wb = new XLWorkbook(filePath);
            var ws = wb.Worksheets.First();

            // ── Kiểm tra template: đủ 6 cột, đúng tiêu đề ─────────────────────
            for (int c = 0; c < ExcelColumns.Length; c++)
            {
                var header = ws.Cell(1, c + 1).GetString()?.Trim();
                if (!string.Equals(header, ExcelColumns[c], StringComparison.OrdinalIgnoreCase))
                {
                    errors.Add(
                        $"Dòng tiêu đề: thiếu hoặc sai cột [{ExcelColumns[c]}] " +
                        $"ở vị trí cột {c + 1}. Vui lòng dùng đúng template Export.");
                    return (rows, errors);
                }
            }

            int lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;

            for (int r = 2; r <= lastRow; r++)
            {
                ct.ThrowIfCancellationRequested();

                // Bỏ qua dòng hoàn toàn trống
                if (ws.Row(r).IsEmpty())
                    continue;

                var dto = new RouteExcelRowDto();
                bool rowHasError = false;
                var reasons = new List<string>();

                // Mã tuyến
                dto.RouteCode = ws.Cell(r, 1).GetString()?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(dto.RouteCode))
                {
                    reasons.Add("Mã tuyến để trống");
                    rowHasError = true;
                }
                else if (dto.RouteCode.Length > 20)
                {
                    reasons.Add("Mã tuyến vượt quá 20 ký tự");
                    rowHasError = true;
                }

                // Tên tuyến
                dto.RouteName = ws.Cell(r, 2).GetString()?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(dto.RouteName))
                {
                    reasons.Add("Tên lộ trình để trống");
                    rowHasError = true;
                }
                else if (dto.RouteName.Length > 150)
                {
                    reasons.Add("Tên lộ trình vượt quá 150 ký tự");
                    rowHasError = true;
                }

                // Cự ly (km)
                if (!TryParseDecimal(ws.Cell(r, 3), out decimal distance) || distance <= 0)
                {
                    reasons.Add("Cự ly (km) phải là số lớn hơn 0");
                    rowHasError = true;
                }
                else
                {
                    dto.DistanceKm = distance;
                }

                // Thời gian chạy dự kiến (giờ)
                if (!TryParseDecimal(ws.Cell(r, 4), out decimal hours) || hours <= 0)
                {
                    reasons.Add("Thời gian (giờ) phải là số lớn hơn 0");
                    rowHasError = true;
                }
                else
                {
                    dto.EstimatedHours = hours;
                }

                // Giá cước
                if (!TryParseDecimal(ws.Cell(r, 5), out decimal price) || price < 0)
                {
                    reasons.Add("Giá cước phải là số không âm");
                    rowHasError = true;
                }
                else
                {
                    dto.BasePrice = price;
                }

                // Trạng thái
                dto.Status = ws.Cell(r, 6).GetString()?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(dto.Status))
                {
                    reasons.Add("Trạng thái để trống (chấp nhận: Active / Suspended)");
                    rowHasError = true;
                }
                else if (!ValidStatuses.Contains(dto.Status, StringComparer.OrdinalIgnoreCase))
                {
                    reasons.Add($"Trạng thái '{dto.Status}' không hợp lệ (chấp nhận: {string.Join("/", ValidStatuses)})");
                    rowHasError = true;
                }

                if (rowHasError)
                    errors.Add($"Dòng {r}: {string.Join("; ", reasons)}");
                else
                    rows.Add(dto);
            }

            return (rows, errors);
        }

        /// <summary>
        /// Đọc giá trị số từ ô Excel. Hỗ trợ ô nhập là Text, Số hoặc đã có định dạng dàn phẩy.
        /// </summary>
        private static bool TryParseDecimal(IXLCell cell, out decimal value)
        {
            value = 0;

            var text = cell.GetString()?.Trim();
            if (string.IsNullOrWhiteSpace(text))
                return false;

            // Bỏ dấu phân cách hàng nghìn (chấm hoặc phẩy tùy vùng) trước khi parse
            if (double.TryParse(text, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture, out var dbl))
            {
                value = (decimal)dbl;
                return true;
            }

            // Fallback: thử parse trực tiếp
            if (decimal.TryParse(text, out value))
                return true;

            return false;
        }
    }
}