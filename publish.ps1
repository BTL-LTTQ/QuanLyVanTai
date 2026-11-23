# =============================================================================
#  publish.ps1 — Đóng gói QLVT thành bản chạy độc lập Windows x64 (self-contained)
# =============================================================================
#  Chạy MỘT LẦN từ thư mục gốc solution:
#      powershell -ExecutionPolicy Bypass -File .\publish.ps1
#
#  Kết quả:
#      QuanLyVanTai.UI\bin\Release\net10.0-windows\publish\win-x64\
#          QuanLyVanTai.UI.exe        <- bản single-file, chạy ngay
#          appsettings.json           <- chuỗi kết nối + cấu hình (sửa theo máy đích)
#          logs\                      <- Serilog tự tạo khi chạy
#
#  Máy đích KHÔNG cần cài .NET 10 SDK. Chỉ cần:
#      • SQL Server cung cấp QuanLyVanTaiDb (xem README) và
#      • WebView2 Evergreen Runtime (có sẵn trên Windows 11 / Edge cập nhật).
# =============================================================================

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$project = Join-Path $root 'QuanLyVanTai.UI\QuanLyVanTai.UI.csproj'
$profile = 'WinX64SelfContained'

Write-Host '=========================================================' -ForegroundColor Cyan
Write-Host ' QLVT - Publish self-contained (win-x64, single-file)'   -ForegroundColor Cyan
Write-Host '=========================================================' -ForegroundColor Cyan

Write-Host "`n[1/2] dotnet publish -c Release -p:PublishProfile=$profile ..." -ForegroundColor Yellow
dotnet publish $project -c Release "-p:PublishProfile=$profile" --nologo
if ($LASTEXITCODE -ne 0) {
    throw "Publish thất bại (exit code $LASTEXITCODE). Xem log phía trên."
}

$outDir = Join-Path $root 'QuanLyVanTai.UI\bin\Release\net10.0-windows\publish\win-x64'
$exePath = Join-Path $outDir 'QuanLyVanTai.UI.exe'

Write-Host "`n[2/2] Kiểm tra kết quả ..." -ForegroundColor Yellow
if (-not (Test-Path $exePath)) {
    throw "Không tìm thấy $exePath. Kiểm tra lại PublishDir trong profile."
}

$sizeMb = [math]::Round((Get-Item $exePath).Length / 1MB, 1)
Write-Host "  OK: $exePath ($sizeMb MB)" -ForegroundColor Green

$configPath = Join-Path $outDir 'appsettings.json'
if (Test-Path $configPath) {
    Write-Host "  OK: appsettings.json đã được copy kèm." -ForegroundColor Green
} else {
    Write-Warning "Thiếu appsettings.json trong thư mục publish — ứng dụng sẽ không đọc được cấu hình."
}

Write-Host "`nHoàn tất. Chép cả thư mục sau sang máy đích:" -ForegroundColor Cyan
Write-Host "  $outDir" -ForegroundColor White
