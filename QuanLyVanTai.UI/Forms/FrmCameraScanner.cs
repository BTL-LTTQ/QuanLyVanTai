using System.ComponentModel;
using Core.Configs;
using FlashCap;
using FontAwesome.Sharp;
using QuanLyVanTai.BLL.Services;
using Serilog;

namespace QuanLyVanTai.UI.Forms
{
    public partial class FrmCameraScanner : Form
    {
        private readonly PassengerCheckInService _decoder = new();
        private readonly object _frameLock = new();
        private CaptureDevice? _device;
        private Task _cameraOperation = Task.CompletedTask;
        private Bitmap? _pendingFrame;
        private string? _pendingPayload;
        private string? _frameError;
        private int _processingFrame;
        private int _capturing;
        private long _lastFrameAt;
        private bool _closing;
        private bool _allowClose;

        public string? QrPayload { get; private set; }

        public FrmCameraScanner()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            ThemeConfig.StylePrimaryButton(btnStart, IconChar.Camera);
            ThemeConfig.StyleSecondaryButton(btnStop, IconChar.Stop);
            ThemeConfig.StyleSecondaryButton(btnClose, IconChar.Xmark);
            Shown += async (_, _) => await LoadCamerasAsync();
            btnStart.Click += async (_, _) => await (_cameraOperation = StartCameraAsync());
            btnStop.Click += async (_, _) => await (_cameraOperation = StopCameraAsync());
            previewTimer.Tick += PreviewTimer_Tick;
            FormClosing += CameraScanner_FormClosing;
            previewTimer.Start();
        }

        private async Task LoadCamerasAsync()
        {
            lblStatus.Text = "Đang tìm camera kết nối...";
            try
            {
                var cameras = await Task.Run(() => new CaptureDevices().EnumerateDescriptors()
                    .Where(d => d.DeviceType == DeviceTypes.DirectShow && d.Characteristics.Any(c =>
                        c.PixelFormat is PixelFormats.JPEG or PixelFormats.RGB24 or PixelFormats.RGB32))
                    .ToArray());
                if (_closing || IsDisposed) return;
                cboCamera.DataSource = cameras;
                cboCamera.Enabled = cameras.Length > 0;
                btnStart.Enabled = cameras.Length > 0;
                lblStatus.Text = cameras.Length > 0
                    ? "Chọn camera rồi bấm Bật camera. Đưa mã QR trên điện thoại hoặc vé giấy trước camera.\r\nĐọc được mã sẽ tự đóng cửa sổ và xử lý check-in một lần."
                    : "Không tìm thấy camera. Hãy kết nối webcam, đóng cửa sổ rồi mở lại.\r\nBạn vẫn có thể dùng máy quét USB hoặc nhập mã vé.";
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Cannot enumerate QR scanner cameras");
                if (!_closing && !IsDisposed)
                    lblStatus.Text = "Không thể tìm camera. Kiểm tra kết nối và quyền camera trong Windows Settings > Privacy & security > Camera.";
            }
        }

        private async Task StartCameraAsync()
        {
            if (cboCamera.SelectedItem is not CaptureDeviceDescriptor descriptor || _device != null || _closing)
                return;

            cboCamera.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = false;
            lblStatus.Text = "Đang mở camera...";
            CaptureDevice? opened = null;
            try
            {
                var mode = descriptor.Characteristics
                    .Where(c => c.PixelFormat is PixelFormats.JPEG or PixelFormats.RGB24 or PixelFormats.RGB32)
                    .OrderBy(c => Math.Abs(c.Width - 640) + Math.Abs(c.Height - 480))
                    .ThenBy(c => c.PixelFormat != PixelFormats.JPEG)
                    .ThenByDescending(c => c.FramesPerSecond)
                    .First();
                opened = await descriptor.OpenAsync(mode, OnFrameArrived);
                if (_closing) return;
                _lastFrameAt = Environment.TickCount64 - 200;
                Volatile.Write(ref _capturing, 1);
                await opened.StartAsync();
                if (_closing) return;
                _device = opened;
                opened = null;
                lblStatus.Text = "Đưa mã QR vào trước camera, giữ vé rõ nét và đủ sáng.\r\nĐọc được mã sẽ tự đóng cửa sổ và xử lý check-in một lần.";
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Cannot start QR scanner camera");
                if (!_closing)
                    lblStatus.Text = "Không mở được camera. Hãy đóng ứng dụng đang dùng camera và cho phép desktop apps truy cập camera trong Windows Settings > Privacy & security > Camera.";
            }
            finally
            {
                if (opened != null)
                {
                    Volatile.Write(ref _capturing, 0);
                    await opened.DisposeAsync();
                    ClearFrames();
                }
                if (!_closing)
                {
                    cboCamera.Enabled = _device == null;
                    btnStart.Enabled = _device == null;
                    btnStop.Enabled = _device != null;
                }
            }
        }

        private void OnFrameArrived(PixelBufferScope scope)
        {
            if (Volatile.Read(ref _capturing) == 0 || Interlocked.CompareExchange(ref _processingFrame, 1, 0) != 0)
                return;
            try
            {
                if (Environment.TickCount64 - Interlocked.Read(ref _lastFrameAt) < 200) return;
                var bytes = scope.Buffer.ReferImage().ToArray();
                scope.ReleaseNow();
                ProcessFrame(bytes);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Cannot read QR scanner frame");
                lock (_frameLock)
                    _frameError = "Không đọc được hình từ camera. Hãy dừng rồi bật lại camera hoặc chọn camera khác.";
            }
            finally
            {
                Interlocked.Exchange(ref _processingFrame, 0);
            }
        }

        private void ProcessFrame(byte[] bytes)
        {
            using var stream = new MemoryStream(bytes);
            using var image = Image.FromStream(stream);
            var frame = new Bitmap(image);
            string? payload = _decoder.DecodeQrFromBitmap(frame);
            lock (_frameLock)
            {
                if (_closing || Volatile.Read(ref _capturing) == 0)
                {
                    frame.Dispose();
                    return;
                }
                _pendingFrame?.Dispose();
                _pendingFrame = frame;
                if (!string.IsNullOrWhiteSpace(payload))
                    _pendingPayload ??= payload;
                Interlocked.Exchange(ref _lastFrameAt, Environment.TickCount64);
            }
        }

        private async void PreviewTimer_Tick(object? sender, EventArgs e)
        {
            if (_closing || !_cameraOperation.IsCompleted) return;
            Bitmap? frame;
            string? payload;
            string? error;
            lock (_frameLock)
            {
                frame = _pendingFrame;
                payload = _pendingPayload;
                error = _frameError;
                _pendingFrame = null;
                _pendingPayload = null;
                _frameError = null;
            }
            if (frame != null)
            {
                var previous = picPreview.Image;
                picPreview.Image = frame;
                previous?.Dispose();
            }
            if (payload != null)
            {
                QrPayload = payload;
                Close();
                return;
            }
            if (error != null || (_device != null &&
                Environment.TickCount64 - Interlocked.Read(ref _lastFrameAt) > 10000))
            {
                await (_cameraOperation = StopCameraAsync());
                if (!_closing)
                    lblStatus.Text = error ?? "Camera không gửi hình. Kiểm tra kết nối, quyền camera hoặc chọn camera khác rồi bật lại.";
            }
        }

        private async Task StopCameraAsync()
        {
            btnStop.Enabled = false;
            Volatile.Write(ref _capturing, 0);
            var device = _device;
            _device = null;
            try
            {
                if (device != null) await device.StopAsync();
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Cannot stop QR scanner camera");
            }
            finally
            {
                if (device != null) await device.DisposeAsync();
                ClearFrames();
                if (!_closing)
                {
                    cboCamera.Enabled = cboCamera.Items.Count > 0;
                    btnStart.Enabled = cboCamera.Items.Count > 0;
                    lblStatus.Text = "Camera đã dừng. Chọn camera rồi bấm Bật camera để quét tiếp.";
                }
            }
        }

        private async void CameraScanner_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_allowClose) return;
            e.Cancel = true;
            if (_closing) return;
            _closing = true;
            previewTimer.Stop();
            btnStart.Enabled = btnStop.Enabled = btnClose.Enabled = false;
            await Task.Yield();
            await _cameraOperation;
            await StopCameraAsync();
            _allowClose = true;
            DialogResult = QrPayload == null ? DialogResult.Cancel : DialogResult.OK;
            Close();
        }

        private void ClearFrames()
        {
            lock (_frameLock)
            {
                _pendingFrame?.Dispose();
                _pendingFrame = null;
                _pendingPayload = null;
                _frameError = null;
            }
            var image = picPreview.Image;
            picPreview.Image = null;
            image?.Dispose();
        }

        private void ReleaseCamera()
        {
            Volatile.Write(ref _capturing, 0);
            _device?.Dispose();
            _device = null;
            ClearFrames();
        }
    }
}
